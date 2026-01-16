// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using BootstrapBlazor.Server.Data;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// 组件属性缓存服务
/// </summary>
public static class ComponentAttributeCacheService
{
    private static readonly ConcurrentDictionary<Type, AttributeItem[]> _cache = new();

    /// <summary>
    /// 获取组件的 AttributeItem 列表(带缓存)
    /// </summary>
    public static AttributeItem[] GetAttributes(Type componentType)
    {
        return _cache.GetOrAdd(componentType, type =>
        {
            var attributes = new List<AttributeItem>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<ParameterAttribute>() != null);

            foreach (var property in properties)
            {
                var item = new AttributeItem
                {
                    Name = property.Name,
                    Type = GetFriendlyTypeName(property.PropertyType),
                    Description = GetSummary(property) ?? "",
                    DefaultValue = GetDefaultValue(property) ?? "",
                    ValueList = GetValueList(property) ?? "",
                    Version = GetVersion(property) ?? ""
                };
                attributes.Add(item);
            }

            return attributes.ToArray();
        });
    }

    /// <summary>
    /// 从 XML 注释获取 summary
    /// </summary>
    private static string? GetSummary(PropertyInfo property)
    {
        var xmlDoc = GetXmlDocumentation(property.DeclaringType?.Assembly);
        if (xmlDoc == null) return null;

        var memberName = $"P:{property.DeclaringType?.FullName}.{property.Name}";
        var element = xmlDoc.Descendants("member")
            .FirstOrDefault(x => x.Attribute("name")?.Value == memberName);

        return element?.Element("summary")?.Value.Trim();
    }

    /// <summary>
    /// 从 XML 注释的 para version 节点获取版本信息
    /// </summary>
    private static string? GetVersion(PropertyInfo property)
    {
        var xmlDoc = GetXmlDocumentation(property.DeclaringType?.Assembly);
        if (xmlDoc == null) return null;

        var memberName = $"P:{property.DeclaringType?.FullName}.{property.Name}";
        var element = xmlDoc.Descendants("member")
            .FirstOrDefault(x => x.Attribute("name")?.Value == memberName);

        // 查找 <para><version>10.0.0</version></para>
        var versionElement = element?.Descendants("para")
            .SelectMany(p => p.Elements("version"))
            .FirstOrDefault();

        return versionElement?.Value.Trim();
    }

    /// <summary>
    /// 获取默认值
    /// </summary>
    private static string? GetDefaultValue(PropertyInfo property)
    {
        var defaultValueAttr = property.GetCustomAttribute<DefaultValueAttribute>();
        if (defaultValueAttr != null)
        {
            return defaultValueAttr.Value?.ToString() ?? "";
        }

        // 从 XML 注释中提取 DefaultValue
        var xmlDoc = GetXmlDocumentation(property.DeclaringType?.Assembly);
        if (xmlDoc == null) return null;

        var memberName = $"P:{property.DeclaringType?.FullName}.{property.Name}";
        var element = xmlDoc.Descendants("member")
            .FirstOrDefault(x => x.Attribute("name")?.Value == memberName);

        var defaultElement = element?.Element("value");
        return defaultElement?.Value.Trim();
    }

    /// <summary>
    /// 获取可选值列表
    /// </summary>
    private static string? GetValueList(PropertyInfo property)
    {
        // 如果是枚举类型,返回枚举值
        if (property.PropertyType.IsEnum)
        {
            return string.Join(" / ", Enum.GetNames(property.PropertyType));
        }

        return null;
    }

    /// <summary>
    /// 获取友好的类型名称
    /// </summary>
    private static string GetFriendlyTypeName(Type type)
    {
        if (type.IsGenericType)
        {
            var genericTypeName = type.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
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
