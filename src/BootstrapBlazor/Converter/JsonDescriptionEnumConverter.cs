// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Enum type converter that serializes the [Description] attribute of enum values to strings.
/// It is recommended to use <see cref="JsonEnumConverter"/> instead.
/// </summary>
public class JsonDescriptionEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    /// <summary>
    /// Reads and converts the JSON to the specified enum type.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The converted enum value.</returns>
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        T ret = default;
        if (reader.TokenType == JsonTokenType.String)
        {
            var enumStringValue = reader.GetString();
            var v = Enum.GetNames<T>().FirstOrDefault(i => typeof(T).ToDescriptionString(i) == enumStringValue);
            if (Enum.TryParse<T>(v, true, out T val))
            {
                ret = val;
            }
        }
        return ret;
    }

    /// <summary>
    /// Writes the specified enum value as a string using its [Description] attribute.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToDescriptionString());
    }
}
