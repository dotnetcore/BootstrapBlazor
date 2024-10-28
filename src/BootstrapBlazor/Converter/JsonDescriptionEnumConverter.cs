// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 枚举类型转换器 序列化时把枚举类型的 [Description] 标签内容序列化成字符串 推荐使用 <see cref="JsonEnumConverter"/> 转换器
/// </summary>
public class JsonDescriptionEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
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
    /// <inheritdoc/>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToDescriptionString());
    }
}
