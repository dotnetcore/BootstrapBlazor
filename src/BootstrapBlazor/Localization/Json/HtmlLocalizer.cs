// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// An <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" /> that uses the provided <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> to do HTML-aware
/// localization of content.
/// </summary>
public class HtmlLocalizer : IHtmlLocalizer
{
    /// <summary>
    /// The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> to read strings from.
    /// </summary>
    protected readonly IStringLocalizer _localizer;

    /// <summary>
    /// Gets the string resource with the given name.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <returns>The string resource as a <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" />.</returns>
    public virtual LocalizedHtmlString this[string name] => ToHtmlString(_localizer[name]);

    /// <summary>
    /// Gets the string resource with the given name and formatted with the supplied arguments. The arguments will
    /// be HTML encoded.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <param name="arguments">The values to format the string with.</param>
    /// <returns>The formatted string resource as a <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" />.</returns>
    public virtual LocalizedHtmlString this[string name, params object[] arguments] => ToHtmlString(_localizer[name], arguments);

    /// <summary>
    /// Creates a new <see cref="T:BootstrapBlazor.Localization.Json.HtmlLocalizer" />.
    /// </summary>
    /// <param name="localizer">The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> to read strings from.</param>
    public HtmlLocalizer(IStringLocalizer localizer) => _localizer = localizer;

    /// <summary>
    /// Gets all string resources.
    /// </summary>
    /// <param name="includeParentCultures">
    /// A <see cref="T:System.Boolean" /> indicating whether to include strings from parent cultures.
    /// </param>
    /// <returns>The strings.</returns>
    public virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);

    /// <summary>
    /// Creates a new <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" /> for a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
    /// </summary>
    /// <param name="result">The <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.</param>
    protected virtual LocalizedHtmlString ToHtmlString(LocalizedString result) => new LocalizedHtmlString(result.Name, result.Value, result.ResourceNotFound);

    /// <summary>
    /// Creates a new <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" /> for a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
    /// </summary>
    /// <param name="result">The <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.</param>
    /// <param name="arguments">The values to format the <paramref name="arguments" /> with.</param>
    protected virtual LocalizedHtmlString ToHtmlString(LocalizedString result, object[] arguments) => new LocalizedHtmlString(result.Name, result.Value, result.ResourceNotFound, arguments);
}

/// <summary>
/// An <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" /> implementation that provides localized HTML content for the specified type
/// <typeparamref name="TResource" />.
/// </summary>
/// <typeparam name="TResource">The <see cref="T:System.Type" /> to scope the resource names.</typeparam>
public class HtmlLocalizer<TResource> : IHtmlLocalizer<TResource>, IHtmlLocalizer
{
    private readonly IHtmlLocalizer _localizer;

    /// <summary>
    /// Gets the string resource with the given name.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <returns>The string resource as a <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" />.</returns>
    public virtual LocalizedHtmlString this[string name] => _localizer[name];

    /// <summary>
    /// Gets the string resource with the given name and formatted with the supplied arguments. The arguments will
    /// be HTML encoded.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <param name="arguments">The values to format the string with.</param>
    /// <returns>The formatted string resource as a <see cref="T:BootstrapBlazor.Localization.Json.LocalizedHtmlString" />.</returns>
    public virtual LocalizedHtmlString this[string name, params object[] arguments] => _localizer[name, arguments];

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.AspNetCore.Mvc.Localization.HtmlLocalizer`1" />.
    /// </summary>
    /// <param name="factory">The <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizerFactory" />.</param>
    public HtmlLocalizer(IHtmlLocalizerFactory factory) => _localizer = factory.Create(typeof(TResource));

    /// <summary>
    /// Gets all string resources.
    /// </summary>
    /// <param name="includeParentCultures">
    /// A <see cref="T:System.Boolean" /> indicating whether to include strings from parent cultures.
    /// </param>
    /// <returns>The strings.</returns>
    public virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);
}
