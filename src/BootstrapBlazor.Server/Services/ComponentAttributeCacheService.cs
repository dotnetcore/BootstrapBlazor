// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// 组件属性缓存服务
/// </summary>
public static class ComponentAttributeCacheService
{
    private static readonly ConcurrentDictionary<string, List<AttributeItem>> _cache = new();

    /// <summary>
    /// 通过组件类型获取组件的 AttributeItem 列表
    /// </summary>
    public static List<AttributeItem> GetAttributes(Type componentType)
    {
#if DEBUG
        return GetAttributeCore(componentType);
#else
        var currentLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var key = $"{componentType.FullName}_{currentLanguage}";
        return _cache.GetOrAdd(key, _ => GetAttributeCore(componentType));
#endif
    }

    private static List<AttributeItem> GetAttributeCore(Type type)
    {
        var attributes = new List<AttributeItem>();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<ParameterAttribute>() != null);

        var xmlDoc = GetXmlDocumentation(type.Assembly);
        foreach (var property in properties)
        {
            var item = new AttributeItem
            {
                Name = property.Name,
                Type = GetFriendlyTypeName(property.PropertyType),
                Description = GetSummary(xmlDoc, property) ?? "",
                Version = GetVersion(xmlDoc, property) ?? "10.0.0"
            };
            attributes.Add(item);
        }
        return attributes.OrderBy(i => i.Name).ToList();
    }

    /// <summary>
    /// 从 XML 注释获取 summary（支持多语言）
    /// </summary>
    private static string? GetSummary(XDocument? xmlDoc, PropertyInfo property)
    {
        if (xmlDoc == null) return null;

        var memberName = $"P:{property.DeclaringType?.FullName}.{property.Name}";
        var memberElement = xmlDoc.Descendants("member")
            .FirstOrDefault(x => x.Attribute("name")?.Value == memberName);

        if (memberElement == null) return null;

        var summaryElement = memberElement.Element("summary");
        if (summaryElement == null) return null;

        // 获取当前语言（zh-CN -> zh, en-US -> en）
        var currentLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        // 查找匹配当前语言的 para 元素
        var langPara = summaryElement.Elements("para")
            .FirstOrDefault(p => p.Attribute("lang")?.Value == currentLanguage);

        if (langPara != null)
        {
            return langPara.Value.Trim();
        }

        // 如果找不到当前语言，回退到第一个有 lang 属性的 para（通常是 zh）
        var firstLangPara = summaryElement.Elements("para")
            .FirstOrDefault(p => p.Attribute("lang") != null);

        if (firstLangPara != null)
        {
            return firstLangPara.Value.Trim();
        }

        // 如果都没有，返回整个 summary 的文本（向后兼容旧格式）
        return summaryElement.Value.Trim();
    }

    /// <summary>
    /// 从 XML 注释的 para version 节点获取版本信息
    /// </summary>
    private static string? GetVersion(XDocument? xmlDoc, PropertyInfo property)
    {
        if (xmlDoc == null) return null;

        var memberName = $"P:{property.DeclaringType?.FullName}.{property.Name}";
        var memberElement = xmlDoc.Descendants("member")
            .FirstOrDefault(x => x.Attribute("name")?.Value == memberName);

        if (memberElement == null) return null;

        // 在 summary 节点下查找包含 version 的 para 节点
        // XML 格式: <summary><para><version>10.2.2</version></para></summary>
        var summaryElement = memberElement.Element("summary");
        if (summaryElement == null) return null;

        // 查找第一个包含 version 元素的 para
        // 直接在循环中返回，避免创建中间变量
        return summaryElement.Elements("para")
            .Select(p => p.Element("version"))
            .FirstOrDefault(v => v != null)
            ?.Value.Trim();
    }

    /// <summary>
    /// 获取友好的类型名称
    /// </summary>
    private static string GetFriendlyTypeName(Type type)
    {
        if (type.IsGenericType)
        {
            var genericTypeName = type.GetGenericTypeDefinition().Name;
            var backtickIndex = genericTypeName.IndexOf('`');
            if (backtickIndex > 0)
            {
                genericTypeName = genericTypeName.Substring(0, backtickIndex);
            }
            var genericArgs = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyTypeName));
            return $"{genericTypeName}<{genericArgs}>";
        }

        return type.Name switch
        {
            "Int32" => "int",
            "String" => "string",
            "Boolean" => "bool",
            "Double" => "double",
            "Decimal" => "decimal",
            _ => type.Name
        };
    }

    /// <summary>
    /// 获取 XML 文档
    /// </summary>
    private static XDocument? GetXmlDocumentation(Assembly? assembly)
    {
        if (assembly == null) return null;

        try
        {
            var assemblyLocation = assembly.Location;
            if (string.IsNullOrEmpty(assemblyLocation))
            {
                return null;
            }

            var xmlPath = Path.Combine(
                Path.GetDirectoryName(assemblyLocation) ?? "",
                Path.GetFileNameWithoutExtension(assemblyLocation) + ".xml"
            );

            if (File.Exists(xmlPath))
            {
                // 使用安全的 XML 读取设置防止 XXE 攻击
                var settings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                    XmlResolver = null
                };

                using var reader = XmlReader.Create(xmlPath, settings);
                return XDocument.Load(reader);
            }
        }
        catch (FileNotFoundException)
        {
            // XML 文档文件不存在,忽略
        }
        catch (XmlException)
        {
            // XML 文档格式错误,忽略
        }
        catch (UnauthorizedAccessException)
        {
            // 没有访问权限,忽略
        }

        return null;
    }

    /// <summary>
    /// 清除缓存
    /// </summary>
    public static void ClearCache()
    {
        _cache.Clear();
    }
}
