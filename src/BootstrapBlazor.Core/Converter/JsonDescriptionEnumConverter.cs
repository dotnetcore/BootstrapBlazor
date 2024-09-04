// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Core.Converter;

/// <summary>
/// 枚举类型转换器
/// </summary>
/// <remarks>序列化时把枚举类型序列化成字符串</remarks>
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
