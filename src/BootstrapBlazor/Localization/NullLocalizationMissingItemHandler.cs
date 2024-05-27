// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Localization;

class NullLocalizationMissingItemHandler : ILocalizationMissingItemHandler
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="typeName"></param>
    /// <param name="cultureName"></param>
    public void HandleMissingItem(string name, string typeName, string cultureName) { }
}
