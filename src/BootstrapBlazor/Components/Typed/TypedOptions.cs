// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Typed 组件配置类</para>
/// <para lang="en">Typed Component Configuration Class</para>
/// </summary>
public class TypedOptions : IEquatable<TypedOptions>
{
    /// <summary>
    /// <para lang="zh">获得/设置 要打字的字符串数组</para>
    /// <para lang="en">Gets or sets the array of strings to be typed</para>
    /// </summary>
    [JsonPropertyName("strings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 打字速度，默认 null 未设置，单位毫秒</para>
    /// <para lang="en">Gets or sets the typing speed. Default is null, in milliseconds.</para>
    /// </summary>
    [JsonPropertyName("typeSpeed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TypeSpeed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 退格速度，默认 null 未设置，单位毫秒</para>
    /// <para lang="en">Gets or sets the backspace speed. Default is null, in milliseconds.</para>
    /// </summary>
    [JsonPropertyName("backSpeed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BackSpeed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 smartBackspace，仅退格与前一个字符串不匹配的内容，默认 true</para>
    /// <para lang="en">Gets or sets smartBackspace. Only backspace what doesn't match the previous string. Default is true.</para>
    /// </summary>
    [JsonPropertyName("smartBackspace")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SmartBackspace { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否打乱字符串顺序，默认 false</para>
    /// <para lang="en">Gets or sets whether to shuffle the strings. Default is false.</para>
    /// </summary>
    [JsonPropertyName("shuffle")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Shuffle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 退格前的延迟时间，默认 700 毫秒</para>
    /// <para lang="en">Gets or sets the delay time before backspacing in milliseconds. Default is 700.</para>
    /// </summary>
    [JsonPropertyName("backDelay")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BackDelay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否循环打字，默认 false</para>
    /// <para lang="en">Gets or sets whether to loop typing. Default is false.</para>
    /// </summary>
    [JsonPropertyName("loop")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Loop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 循环次数，默认无限</para>
    /// <para lang="en">Gets or sets the amount of loops. Default is Infinity.</para>
    /// </summary>
    [JsonPropertyName("loopCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? LoopCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示光标，默认 true</para>
    /// <para lang="en">Gets or sets whether to show the cursor. Default is true.</para>
    /// </summary>
    [JsonPropertyName("showCursor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowCursor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 光标字符，默认 |</para>
    /// <para lang="en">Gets or sets the cursor character. Default is |.</para>
    /// </summary>
    [JsonPropertyName("cursorChar")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CursorChar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容类型，'html' 或 'null' 表示纯文本，默认 html</para>
    /// <para lang="en">Gets or sets the content type. 'html' or 'null' for plaintext. Default is html.</para>
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
