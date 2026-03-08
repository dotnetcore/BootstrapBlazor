// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Css 生成操作类</para>
/// <para lang="en">Css builder class</para>
/// </summary>
public class CssBuilder
{
    private readonly StringBuilder _builder = new();
    private bool _hasContent;

    /// <summary>
    /// <para lang="zh">创建一个 CssBuilder 实例，用于定义组件中使用的条件 CSS 类。调用 Build() 返回完整的 CSS 类字符串</para>
    /// <para lang="en">Creates a CssBuilder used to define conditional CSS classes used in a component. Call Build() to return the completed CSS Classes as a string</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">初始的 CSS 类字符串</para>
    ///  <para lang="en">Initial CSS class string</para>
    /// </param>
    public static CssBuilder Default(string? value = null) => new(value);

    /// <summary>
    /// <para lang="zh">创建一个 CssBuilder 实例，用于定义组件中使用的条件 CSS 类。调用 Build() 返回完整的 CSS 类字符串</para>
    /// <para lang="en">Creates a CssBuilder used to define conditional CSS classes used in a component. Call Build() to return the completed CSS Classes as a string</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">初始的 CSS 类字符串</para>
    ///  <para lang="en">Initial CSS class string</para>
    /// </param>
    protected CssBuilder(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _builder.Append(value);
            _hasContent = true;
        }
    }

    /// <summary>
    /// <para lang="zh">向构建器添加一个原始字符串，该字符串将与下一个添加到构建器的类或值连接</para>
    /// <para lang="en">Adds a raw string to the builder that will be concatenated with the next class or value added to the builder</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">要添加的 CSS 类字符串</para>
    ///  <para lang="en">CSS class string to add</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClass(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            if (_hasContent)
            {
                _builder.Append(' ');
            }
            else
            {
                _hasContent = true;
            }
            _builder.Append(value);
        }
        return this;
    }

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件 CSS 类，该类将在满足条件时添加，并使用空格分隔</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">要添加的 CSS 类字符串</para>
    ///  <para lang="en">CSS Class to conditionally add</para>
    /// </param>
    /// <param name="when">
    ///  <para lang="zh">添加 CSS 类的条件</para>
    ///  <para lang="en">Condition in which the CSS Class is added</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClass(string? value, bool when = true) => when ? AddClass(value) : this;

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件 CSS 类，该类将在满足条件时添加，并使用空格分隔</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">要添加的 CSS 类字符串</para>
    ///  <para lang="en">CSS Class to conditionally add</para>
    /// </param>
    /// <param name="when">
    ///  <para lang="zh">添加 CSS 类的条件</para>
    ///  <para lang="en">Condition in which the CSS Class is added</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClass(string? value, Func<bool> when) => AddClass(value, when());

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件 CSS 类，该类将在满足条件时添加，并使用空格分隔</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">返回要条件性添加的 CSS 类的函数</para>
    ///  <para lang="en">Function that returns a CSS Class to conditionally add</para>
    /// </param>
    /// <param name="when">
    ///  <para lang="zh">添加 CSS 类的条件</para>
    ///  <para lang="en">Condition in which the CSS Class is added</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClass(Func<string?> value, bool when = true) => when ? AddClass(value()) : this;

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件 CSS 类，该类将在满足条件时添加，并使用空格分隔</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">返回要条件性添加的 CSS 类的函数</para>
    ///  <para lang="en">Function that returns a CSS Class to conditionally add</para>
    /// </param>
    /// <param name="when">
    ///  <para lang="zh">添加 CSS 类的条件</para>
    ///  <para lang="en">Condition in which the CSS Class is added</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClass(Func<string?> value, Func<bool> when) => AddClass(value, when());

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件嵌套 CssBuilder，该类将在满足条件时添加，并使用空格分隔</para>
    /// <para lang="en">Adds a conditional nested CssBuilder to the builder with space separator</para>
    /// </summary>
    /// <param name="builder">
    ///  <para lang="zh">要条件性添加的嵌套 CssBuilder 实例</para>
    ///  <para lang="en">Nested CssBuilder instance to conditionally add</para>
    /// </param>
    /// <param name="when">
    ///  <para lang="zh">添加 CSS 类的条件</para>
    ///  <para lang="en">Condition in which the CSS Class is added</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClass(CssBuilder builder, bool when = true) => when ? AddClass(builder.Build()) : this;

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件 CSS 类，该类将在满足条件时添加，并使用空格分隔</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator</para>
    /// </summary>
    /// <param name="builder">
    ///  <para lang="zh">要条件性添加的嵌套 CssBuilder 实例</para>
    ///  <para lang="en">Nested CssBuilder instance to conditionally add</para>
    /// </param>
    /// <param name="when">
    ///  <para lang="zh">添加 CSS 类的条件</para>
    ///  <para lang="en">Condition in which the CSS Class is added</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClass(CssBuilder builder, Func<bool> when) => AddClass(builder, when());

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件 CSS 类，当它存在于字典中时添加，并使用空格分隔。空安全操作</para>
    /// <para lang="en">Adds a conditional CSS Class when it exists in a dictionary to the builder with space separator. Null safe operation</para>
    /// </summary>
    /// <param name="additionalAttributes">
    ///  <para lang="zh">要条件性添加的附加属性字典</para>
    ///  <para lang="en">Dictionary of additional attributes to conditionally add</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddClassFromAttributes(IDictionary<string, object>? additionalAttributes)
    {
        if (additionalAttributes != null && additionalAttributes.TryGetValue("class", out var c))
        {
            var classString = Convert.ToString(c, CultureInfo.InvariantCulture);
            AddClass(classString);
        }
        return this;
    }

    /// <summary>
    /// <para lang="zh">向构建器添加一个条件 CSS 样式，当它存在于字典中时添加，并使用空格分隔。空安全操作</para>
    /// <para lang="en">Adds a conditional CSS Style when it exists in a dictionary to the builder with space separator. Null safe operation</para>
    /// </summary>
    /// <param name="additionalAttributes">
    ///  <para lang="zh">要条件性添加的附加属性字典</para>
    ///  <para lang="en">Dictionary of additional attributes to conditionally add</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">CssBuilder 实例</para>
    ///  <para lang="en">CssBuilder instance</para>
    /// </returns>
    public CssBuilder AddStyleFromAttributes(IDictionary<string, object>? additionalAttributes)
    {
        if (additionalAttributes != null && additionalAttributes.TryGetValue("style", out var c))
        {
            var styleList = Convert.ToString(c, CultureInfo.InvariantCulture);
            AddClass(styleList);
        }
        return this;
    }

    /// <summary>
    /// <para lang="zh">将完成的 CSS 类作为字符串返回</para>
    /// <para lang="en">Finalize the completed CSS Classes as a string</para>
    /// </summary>
    /// <returns>
    ///  <para lang="zh">完成的 CSS 类字符串</para>
    ///  <para lang="en">Completed CSS Classes as a string</para>
    /// </returns>
    public string? Build() => _hasContent ? _builder.ToString() : null;
}
