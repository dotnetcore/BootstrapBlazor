// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Interface for theme provider
///</para>
/// <para lang="en">Interface for theme provider
///</para>
/// </summary>
public interface IThemeProvider
{
    /// <summary>
    /// <para lang="zh">设置 the theme asynchronously.
    ///</para>
    /// <para lang="en">Sets the theme asynchronously.
    ///</para>
    /// </summary>
    /// <param name="themeName">The name of the theme to set.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SetThemeAsync(string themeName);

    /// <summary>
    /// <para lang="zh">获得 the current theme asynchronously.
    ///</para>
    /// <para lang="en">Gets the current theme asynchronously.
    ///</para>
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, with the current theme name as the result.</returns>
    ValueTask<string?> GetThemeAsync();

    /// <summary>
    /// <para lang="zh">回调 when theme changed
    ///</para>
    /// <para lang="en">The callback when theme changed
    ///</para>
    /// </summary>
    Func<string, Task>? ThemeChangedAsync { get; set; }

    /// <summary>
    /// <para lang="zh">Trigger the theme changed event
    ///</para>
    /// <para lang="en">Trigger the theme changed event
    ///</para>
    /// </summary>
    /// <param name="themeName">The name of the theme to set.</param>
    void TriggerThemeChanged(string themeName);
}
