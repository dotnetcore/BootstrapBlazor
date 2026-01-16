// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">handler to handle the items that cannot be localized by any localizer or LocalizationResolver <seealso cref="ILocalizationResolve"/></para>
/// <para lang="en">The handler to handle the items that cannot be localized by any localizer or LocalizationResolver <seealso cref="ILocalizationResolve"/></para>
/// </summary>
public interface ILocalizationMissingItemHandler
{
    /// <summary>
    /// <para lang="zh">Handle the item that cannot be localized by any localizer or <seealso cref="ILocalizationResolve"/></para>
    /// <para lang="en">Handle the item that cannot be localized by any localizer or <seealso cref="ILocalizationResolve"/></para>
    /// </summary>
    /// <param name="name">the item name for localization</param>
    /// <param name="typeName">the type name that uses the item</param>
    /// <param name="cultureName">the culture name that is missing localization</param>
    void HandleMissingItem(string name, string typeName, string cultureName);
}
