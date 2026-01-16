// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ColumnVisibleItem 序列化转化器</para>
///  <para lang="en">ColumnVisibleItem 序列化转化器</para>
/// </summary>
public class ColumnVisibleItemConverter : JsonConverter<ColumnVisibleItem>
{
    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override ColumnVisibleItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? name = null;
        bool visible = false;
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    if (propertyName == "name")
                    {
                        reader.Read();
                        name = reader.GetString();
                    }
                    else if (propertyName == "visible")
                    {
                        reader.Read();
                        visible = reader.GetBoolean();
                    }
                }
            }
        }
        return string.IsNullOrEmpty(name) ? null : new ColumnVisibleItem(name, visible);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, ColumnVisibleItem value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", value.Name);
        writer.WriteBoolean("visible", value.Visible);
        writer.WriteEndObject();
    }
}
