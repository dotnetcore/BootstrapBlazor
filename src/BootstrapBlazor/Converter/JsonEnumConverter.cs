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
    /// <param name="camelCase">Optional naming policy for writing enum values.</param>
    /// <param name="allowIntegerValues">True to allow undefined enum values. When true, if an enum value isn't defined it will output as a number rather than a string.</param>
    public JsonEnumConverter(bool camelCase = false, bool allowIntegerValues = true)
    {
        _camelCase = camelCase;
        _allowIntegerValues = allowIntegerValues;
    }

    /// <summary>
    /// naming policy for writing enum values
    /// </summary>
    private readonly bool _camelCase;

    /// <summary>
    /// True to allow undefined enum values. When true, if an enum value isn't defined it will output as a number rather than a string
    /// </summary>
    private readonly bool _allowIntegerValues;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override JsonConverter? CreateConverter(Type typeToConvert)
    {
        var converter = _camelCase
                ? new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, _allowIntegerValues)
                : new JsonStringEnumConverter(null, _allowIntegerValues);
        return converter;
    }
}
