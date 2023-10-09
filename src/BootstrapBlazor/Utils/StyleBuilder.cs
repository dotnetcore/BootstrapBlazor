// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Utils;

/// <summary>
/// 
/// </summary>
public readonly struct StyleBuilder
{
    private readonly HashSet<string> _styles;
    private readonly string? _userStyles;

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleBuilder"/> class.
    /// </summary>
    public StyleBuilder()
    {
        _styles = new HashSet<string>();
        _userStyles = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleBuilder"/> class.
    /// </summary>
    /// <param name="userStyles">The user styles to include at the end.</param>
    public StyleBuilder(string? userStyles)
    {
        _styles = new HashSet<string>();
        _userStyles = string.IsNullOrWhiteSpace(userStyles)
                    ? null
                    : string.Join("; ", userStyles.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                                  .Where(i => !string.IsNullOrWhiteSpace(i)));
    }

    /// <summary>
    /// Adds a conditional in-line style to the builder with space separator and closing semicolon.
    /// </summary>
    /// <param name="style"></param>
    public StyleBuilder AddStyle(string? style) => AddRaw(style);

    /// <summary>
    /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
    /// </summary>
    /// <param name="prop"></param>
    /// <param name="value">Style to add</param>
    /// <returns>StyleBuilder</returns>
    public StyleBuilder AddStyle(string prop, string? value) => AddRaw($"{prop}: {value}");

    /// <summary>
    /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
    /// </summary>
    /// <param name="prop"></param>
    /// <param name="value">Style to conditionally add.</param>
    /// <param name="when">Condition in which the style is added.</param>
    /// <returns>StyleBuilder</returns>
    public StyleBuilder AddStyle(string prop, string? value, bool when = true) => when ? this.AddStyle(prop, value) : this;

    /// <summary>
    /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
    /// </summary>
    /// <param name="prop"></param>
    /// <param name="value">Style to conditionally add.</param>
    /// <param name="when">Condition in which the style is added.</param>
    /// <returns>StyleBuilder</returns>
    public StyleBuilder AddStyle(string prop, string? value, Func<bool> when) => this.AddStyle(prop, value, when != null && when());

    /// <summary>
    /// Finalize the completed Style as a string.
    /// </summary>
    /// <returns>string</returns>
    public string? Build()
    {
        var allStyles = string.IsNullOrWhiteSpace(_userStyles)
                      ? _styles
                      : _styles.Union(new[] { _userStyles });

        if (!allStyles.Any())
        {
            return null;
        }

        return string.Concat(allStyles.Select(s => $"{s}; ")).TrimEnd();
    }

    /// <summary>
    /// ToString should only and always call Build to finalize the rendered string.
    /// </summary>
    /// <returns></returns>
    public override string? ToString() => Build();

    /// <summary>
    /// Adds a raw string to the builder that will be concatenated with the next style or value added to the builder.
    /// </summary>
    /// <param name="style"></param>
    /// <returns>StyleBuilder</returns>
    private StyleBuilder AddRaw(string? style)
    {
        if (!string.IsNullOrWhiteSpace(style))
        {
            _styles.Add(style.Trim().TrimEnd(';'));
        }

        return this;
    }
}
