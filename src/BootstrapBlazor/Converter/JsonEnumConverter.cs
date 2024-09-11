// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Core.Converter;

/// <summary>
/// JsonEnumConverter 枚举转换器
/// </summary>
public class JsonEnumConverter : JsonConverterAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public JsonEnumConverter() : base()
    {

    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="camelCase">Optional naming policy for writing enum values.</param>
    /// <param name="allowIntegerValues">True to allow undefined enum values. When true, if an enum value isn't defined it will output as a number rather than a string.</param>
    public JsonEnumConverter(bool camelCase, bool allowIntegerValues = true) : this()
    {
        CamelCase = camelCase;
        AllowIntegerValues = allowIntegerValues;
    }

    /// <summary>
    /// naming policy for writing enum values
    /// </summary>
    public bool CamelCase { get; private set; }

    /// <summary>
    /// True to allow undefined enum values. When true, if an enum value isn't defined it will output as a number rather than a string
    /// </summary>
    public bool AllowIntegerValues { get; private set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override JsonConverter? CreateConverter(Type typeToConvert)
    {
        var converter = CamelCase
                ? new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, AllowIntegerValues)
                : new JsonStringEnumConverter(null, AllowIntegerValues);
        return converter;
    }
}
