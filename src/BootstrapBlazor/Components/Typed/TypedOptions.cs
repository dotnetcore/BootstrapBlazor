// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TypedJs 组件配置类
/// </summary>
public class TypedOptions : IEquatable<TypedOptions>
{
    /// <summary>
    /// 获得/设置 要打字的字符串数组
    /// </summary>
    [JsonPropertyName("strings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Text { get; set; }

    /// <summary>
    /// 获得/设置 打字速度 默认 null 未设置 单位毫秒
    /// </summary>
    [JsonPropertyName("typeSpeed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TypeSpeed { get; set; }

    /// <summary>
    /// 获得/设置 退格速度 默认 null 未设置 单位毫秒
    /// </summary>
    [JsonPropertyName("backSpeed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BackSpeed { get; set; }

    /// <summary>
    /// 获得/设置 smartBackspace only backspace what doesn't match the previous string default true
    /// </summary>
    [JsonPropertyName("smartBackspace")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SmartBackspace { get; set; }

    /// <summary>
    /// 获得/设置 shuffle the strings default false
    /// </summary>
    [JsonPropertyName("shuffle")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Shuffle { get; set; }

    /// <summary>
    /// 获得/设置 backDelay time before backspacing in milliseconds default 700
    /// </summary>
    [JsonPropertyName("backDelay")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BackDelay { get; set; }

    /// <summary>
    /// 获得/设置 loop loop strings default false
    /// </summary>
    [JsonPropertyName("loop")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Loop { get; set; }

    /// <summary>
    /// 获得/设置 loopCount amount of loops default Infinity
    /// </summary>
    [JsonPropertyName("loopCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? LoopCount { get; set; }

    /// <summary>
    /// 获得/设置 showCursor show cursor default true
    /// </summary>
    [JsonPropertyName("showCursor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowCursor { get; set; }

    /// <summary>
    /// 获得/设置 cursorChar character for cursor default |
    /// </summary>
    [JsonPropertyName("cursorChar")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CursorChar { get; set; }

    /// <summary>
    /// 获得/设置 contentType 'html' or 'null' for plaintext default html
    /// </summary>
    [JsonPropertyName("contentType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContentType { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public bool Equals(TypedOptions? option)
    {
        if (option == null)
        {
            return false;
        }

        return EqualText(option.Text) &&
               TypeSpeed == option.TypeSpeed &&
               BackSpeed == option.BackSpeed &&
               SmartBackspace == option.SmartBackspace &&
               Shuffle == option.Shuffle &&
               BackDelay == option.BackDelay &&
               Loop == option.Loop &&
               LoopCount == option.LoopCount &&
               ShowCursor == option.ShowCursor &&
               CursorChar == option.CursorChar &&
               ContentType == option.ContentType;
    }

    private bool EqualText(List<string>? text)
    {
        if (Text == null && text == null)
        {
            return true;
        }
        if (Text == null || text == null)
        {
            return false;
        }
        if (Text.Count != text.Count)
        {
            return false;
        }
        return Text.SequenceEqual(text);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is TypedOptions option)
        {
            return Equals(option);
        }

        return false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => base.GetHashCode();
}
