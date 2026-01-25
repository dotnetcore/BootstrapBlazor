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
    private bool _hasConent;

    /// <summary>
    /// <para lang="zh">Creates a CssBuilder used to define conditional CSS classes used in a component. Call Build() to return the completed CSS Classes as a string.</para>
    /// <para lang="en">Creates a CssBuilder used to define conditional CSS classes used in a component. Call Build() to return the completed CSS Classes as a string.</para>
    /// </summary>
    /// <param name="value"></param>
    public static CssBuilder Default(string? value = null) => new(value);

    /// <summary>
    /// <para lang="zh">Creates a CssBuilder used to define conditional CSS classes used in a component. Call Build() to return the completed CSS Classes as a string.</para>
    /// <para lang="en">Creates a CssBuilder used to define conditional CSS classes used in a component. Call Build() to return the completed CSS Classes as a string.</para>
    /// </summary>
    /// <param name="value"></param>
    protected CssBuilder(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _builder.Append(value);
            _hasConent = true;
        }
    }

    /// <summary>
    /// <para lang="zh">Adds a raw string to the builder that will be concatenated with the next class or value added to the builder.</para>
    /// <para lang="en">Adds a raw string to the builder that will be concatenated with the next class or value added to the builder.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns>CssBuilder</returns>
    public CssBuilder AddClass(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            if (_hasConent)
            {
                _builder.Append(' ');
            }
            else
            {
                _hasConent = true;
            }
            _builder.Append(value);
        }
        return this;
    }

    /// <summary>
    /// <para lang="zh">Adds a conditional CSS Class to the builder with space separator.</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator.</para>
    /// </summary>
    /// <param name="value">CSS Class to conditionally add.</param>
    /// <param name="when">Condition in which the CSS Class is added.</param>
    /// <returns>CssBuilder</returns>
    public CssBuilder AddClass(string? value, bool when = true) => when ? AddClass(value) : this;

    /// <summary>
    /// <para lang="zh">Adds a conditional CSS Class to the builder with space separator.</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator.</para>
    /// </summary>
    /// <param name="value">CSS Class to conditionally add.</param>
    /// <param name="when">Condition in which the CSS Class is added.</param>
    /// <returns>CssBuilder</returns>
    public CssBuilder AddClass(string? value, Func<bool> when) => AddClass(value, when());

    /// <summary>
    /// <para lang="zh">Adds a conditional CSS Class to the builder with space separator.</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator.</para>
    /// </summary>
    /// <param name="value">Function that returns a CSS Class to conditionally add.</param>
    /// <param name="when">Condition in which the CSS Class is added.</param>
    /// <returns>CssBuilder</returns>
    public CssBuilder AddClass(Func<string?> value, bool when = true) => when ? AddClass(value()) : this;

    /// <summary>
    /// <para lang="zh">Adds a conditional CSS Class to the builder with space separator.</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator.</para>
    /// </summary>
    /// <param name="value">Function that returns a CSS Class to conditionally add.</param>
    /// <param name="when">Condition in which the CSS Class is added.</param>
    /// <returns>CssBuilder</returns>
    public CssBuilder AddClass(Func<string?> value, Func<bool> when) => AddClass(value, when());

    /// <summary>
    /// <para lang="zh">Adds a conditional nested CssBuilder to the builder with space separator.</para>
    /// <para lang="en">Adds a conditional nested CssBuilder to the builder with space separator.</para>
    /// </summary>
    /// <param name="builder">CSS Class to conditionally add.</param>
    /// <param name="when">Condition in which the CSS Class is added.</param>
    /// <returns>CssBuilder</returns>
    public CssBuilder AddClass(CssBuilder builder, bool when = true) => when ? AddClass(builder.Build()) : this;

    /// <summary>
    /// <para lang="zh">Adds a conditional CSS Class to the builder with space separator.</para>
    /// <para lang="en">Adds a conditional CSS Class to the builder with space separator.</para>
    /// </summary>
    /// <param name="builder">CSS Class to conditionally add.</param>
    /// <param name="when">Condition in which the CSS Class is added.</param>
    /// <returns>CssBuilder</returns>
    public CssBuilder AddClass(CssBuilder builder, Func<bool> when) => AddClass(builder, when());

    /// <summary>
    /// <para lang="zh">Adds a conditional CSS Class when it exists in a dictionary to the builder with space separator. Null safe operation.</para>
    /// <para lang="en">Adds a conditional CSS Class when it exists in a dictionary to the builder with space separator. Null safe operation.</para>
    /// </summary>
    /// <param name="additionalAttributes">Additional Attribute splat parameters</param>
    /// <returns>CssBuilder</returns>
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
    /// <para lang="zh">Adds a conditional css Style when it exists in a dictionary to the builder with space separator. Null safe operation.</para>
    /// <para lang="en">Adds a conditional css Style when it exists in a dictionary to the builder with space separator. Null safe operation.</para>
    /// </summary>
    /// <param name="additionalAttributes">Additional Attribute splat parameters</param>
    /// <returns>CssBuilder</returns>
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
    /// <para lang="zh">Finalize the completed CSS Classes as a string.</para>
    /// <para lang="en">Finalize the completed CSS Classes as a string.</para>
    /// </summary>
    /// <returns>string</returns>
    public string? Build() => _hasConent ? _builder.ToString() : null;
}
