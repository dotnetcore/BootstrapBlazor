// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// JsonEnumConverter is an enum converter that allows customization of enum serialization.
/// </summary>
/// <param name="camelCase">Optional naming policy for writing enum values.</param>
/// <param name="allowIntegerValues">True to allow undefined enum values. When true, if an enum value isn't defined it will output as a number rather than a string.</param>
public class JsonEnumConverter(bool camelCase = false, bool allowIntegerValues = true) : JsonConverterAttribute
{
    /// <summary>
    /// Creates a JsonConverter for the specified type.
    /// </summary>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <returns>A JsonConverter for the specified type.</returns>
    public override JsonConverter? CreateConverter(Type typeToConvert)
    {
        var converter = camelCase
                ? new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues)
                : new JsonStringEnumConverter(null, allowIntegerValues);
        return converter;
    }
}
