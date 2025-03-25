// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Interface for theme provider
/// </summary>
public interface IThemeProvider
{
    /// <summary>
    /// Sets the theme asynchronously.
    /// </summary>
    /// <param name="themeName">The name of the theme to set.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SetThemeAsync(string themeName);

    /// <summary>
    /// Gets the current theme asynchronously.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, with the current theme name as the result.</returns>
    ValueTask<string?> GetThemeAsync();

    /// <summary>
    /// The callback when theme changed
    /// </summary>
    Func<string, Task>? ThemeChangedAsync { get; set; }

    /// <summary>
    /// Trigger the theme changed event
    /// </summary>
    /// <param name="themeName">The name of the theme to set.</param>
    void TriggerThemeChanged(string themeName);
}
