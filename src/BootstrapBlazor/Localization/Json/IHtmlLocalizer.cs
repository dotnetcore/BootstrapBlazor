// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// Represents a type that does HTML-aware localization of strings, by HTML encoding arguments that are formatted in the resource string.
/// </summary>
public interface IHtmlLocalizer
{
    /// <summary>
    /// Gets the string resource with the given name.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <returns>The string resource as a <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" />.</returns>
    LocalizedHtmlString this[string name] { get; }

    /// <summary>
    /// Gets the string resource with the given name and formatted with the supplied arguments. The arguments will
    /// be HTML encoded.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <param name="arguments">The values to format the string with.</param>
    /// <returns>The formatted string resource as a <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" />.</returns>
    LocalizedHtmlString this[string name, params object[] arguments] { get; }

    /// <summary>
    /// Gets all string resources.
    /// </summary>
    /// <param name="includeParentCultures">
    /// A <see cref="T:System.Boolean" /> indicating whether to include strings from parent cultures.
    /// </param>
    /// <returns>The strings.</returns>
    IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures);
}

/// <summary>
/// An <see cref="T:Microsoft.Extensions.Localization.IHtmlLocalizer" /> that provides localized HTML content.
/// </summary>
/// <typeparam name="TResource">The <see cref="T:System.Type" /> to scope the resource names.</typeparam>
public interface IHtmlLocalizer<out TResource> : IHtmlLocalizer
{

}
