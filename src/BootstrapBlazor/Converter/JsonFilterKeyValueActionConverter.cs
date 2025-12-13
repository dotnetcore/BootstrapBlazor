// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="FilterKeyValueAction"/> 转换器
/// </summary>
public sealed class JsonFilterKeyValueActionConverter : JsonConverter<FilterKeyValueAction>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override FilterKeyValueAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var action = new FilterKeyValueAction();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            Type? fieldValueType = null;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "fieldKey":
                            action.FieldKey = reader.GetString();
                            break;
                        case "fieldValueType":
                            var typeName = reader.GetString();
                            if (!string.IsNullOrEmpty(typeName))
                            {
                                fieldValueType = Type.GetType(typeName);
                            }
                            break;
                        case "fieldValue":
                            if (fieldValueType != null)
                            {
                                action.FieldValue = JsonSerializer.Deserialize(ref reader, fieldValueType, options);
                            }
                            else
                            {
                                action.FieldValue = reader.GetString();
                            }
                            break;
                        case "filterAction":
                            action.FilterAction = JsonSerializer.Deserialize<FilterAction>(ref reader, options);
                            break;
                        case "filterLogic":
                            action.FilterLogic = JsonSerializer.Deserialize<FilterLogic>(ref reader, options);
                            break;
                        case "filters":
                            var filters = JsonSerializer.Deserialize<List<FilterKeyValueAction>>(ref reader, options);
                            if (filters != null)
                            {
                                action.Filters.AddRange(filters);
                            }
                            break;
                    }
                }
            }
        }
        return action;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, FilterKeyValueAction value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("fieldKey", value.FieldKey);

        writer.WriteString("fieldValueType", value.FieldValue?.GetType().FullName);

        writer.WritePropertyName("fieldValue");
        writer.WriteRawValue(JsonSerializer.Serialize(value.FieldValue, options));

        writer.WritePropertyName("filterAction");
        writer.WriteRawValue(JsonSerializer.Serialize(value.FilterAction, options));

        writer.WritePropertyName("filterLogic");
        writer.WriteRawValue(JsonSerializer.Serialize(value.FilterLogic, options));

        writer.WriteStartArray("filters");
        foreach (var filter in value.Filters)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(filter, options));
        }
        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
