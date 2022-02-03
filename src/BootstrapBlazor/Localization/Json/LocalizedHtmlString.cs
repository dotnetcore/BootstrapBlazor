// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// An <see cref="T:Microsoft.AspNetCore.Html.IHtmlContent" /> with localized content.
/// </summary>
public class LocalizedHtmlString
{
    private readonly object[] _arguments;

    /// <summary>
    /// The name of the string resource.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The original resource string, prior to formatting with any constructor arguments.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets a flag that indicates if the resource is not found.
    /// </summary>
    public bool IsResourceNotFound { get; }

    /// <summary>
    /// Creates an instance of <see cref="T:Microsoft.AspNetCore.Mvc.Localization.LocalizedHtmlString" />.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <param name="value">The string resource.</param>
    public LocalizedHtmlString(string name, string value)
        : this(name, value, isResourceNotFound: false, Array.Empty<object>())
    {

    }

    /// <summary>
    /// Creates an instance of <see cref="T:Microsoft.AspNetCore.Mvc.Localization.LocalizedHtmlString" />.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <param name="value">The string resource.</param>
    /// <param name="isResourceNotFound">A flag that indicates if the resource is not found.</param>
    public LocalizedHtmlString(string name, string value, bool isResourceNotFound)
        : this(name, value, isResourceNotFound, Array.Empty<object>())
    {

    }

    /// <summary>
    /// Creates an instance of <see cref="T:Microsoft.AspNetCore.Mvc.Localization.LocalizedHtmlString" />.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <param name="value">The string resource.</param>
    /// <param name="isResourceNotFound">A flag that indicates if the resource is not found.</param>
    /// <param name="arguments">The values to format the <paramref name="value" /> with.</param>
    public LocalizedHtmlString(string name, string value, bool isResourceNotFound, params object[] arguments)
    {
        Name = name;
        Value = value;
        IsResourceNotFound = isResourceNotFound;
        _arguments = arguments;
    }

    /// <summary>
    /// Implicitly converts the <see cref="T:Microsoft.Extensions.Localization.LocalizedString" /> to a <see cref="T:System.String" />.
    /// </summary>
    /// <param name="localizedString">The string to be implicitly converted.</param>
    public static implicit operator string(LocalizedHtmlString localizedString)
    {
        return localizedString.Value;
    }
}
