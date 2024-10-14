// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Core.Converter;

/// <summary>
/// JsonEnumConverter 枚举转换器
/// </summary>
/// <param name="camelCase">Optional naming policy for writing enum values.</param>
/// <param name="allowIntegerValues">True to allow undefined enum values. When true, if an enum value isn't defined it will output as a number rather than a string.</param>
public class JsonEnumConverter(bool camelCase = false, bool allowIntegerValues = true) : JsonConverterAttribute
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override JsonConverter? CreateConverter(Type typeToConvert)
    {
        var converter = camelCase
                ? new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues)
                : new JsonStringEnumConverter(null, allowIntegerValues);
        return converter;
    }
}
