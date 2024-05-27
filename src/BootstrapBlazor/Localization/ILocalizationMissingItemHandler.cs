// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Localization;

/// <summary>
/// The handler to handle the items that cannot be localized by any localizer or LocalizationResolver <seealso cref="ILocalizationResolve"/> 
/// </summary>
public interface ILocalizationMissingItemHandler
{
    /// <summary>
    /// Handle the item that cannot be localized by any localizer or <seealso cref="ILocalizationResolve"/> 
    /// </summary>
    /// <param name="name">the item name for localization</param>
    /// <param name="typeName">the type name that uses the item</param>
    /// <param name="cultureName">the culture name that is missing localization</param>
    void HandleMissingItem(string name, string typeName, string cultureName);
}
