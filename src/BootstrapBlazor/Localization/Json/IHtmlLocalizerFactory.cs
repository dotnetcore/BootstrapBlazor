// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// A factory that creates <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" /> instances.
/// </summary>
public interface IHtmlLocalizerFactory
{
    /// <summary>
    /// Creates an <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" /> using the <see cref="T:System.Reflection.Assembly" /> and
    /// <see cref="P:System.Type.FullName" /> of the specified <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="resourceSource">The <see cref="T:System.Type" />.</param>
    /// <returns>The <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" />.</returns>
    IHtmlLocalizer Create(Type resourceSource);

    /// <summary>
    /// Creates an <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" />.
    /// </summary>
    /// <param name="baseName">The base name of the resource to load strings from.</param>
    /// <param name="location">The location to load resources from.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Localization.IHtmlLocalizer" />.</returns>
    IHtmlLocalizer Create(string baseName, string location);
}
