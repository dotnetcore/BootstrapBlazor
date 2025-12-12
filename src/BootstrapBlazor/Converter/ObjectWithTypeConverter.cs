// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

internal class ObjectWithTypeConverter : JsonConverter<object>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);

        if (!doc.RootElement.TryGetProperty("$type", out var typeProp))
            return doc.RootElement.Clone();  // 无类型信息

        var type = Type.GetType(typeProp.GetString()!)!;

        var valueElement = doc.RootElement.GetProperty("value");
        return JsonSerializer.Deserialize(valueElement.GetRawText(), type, options);
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("$type", value.GetType().AssemblyQualifiedName);
        writer.WritePropertyName("value");
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
        writer.WriteEndObject();
    }
}
