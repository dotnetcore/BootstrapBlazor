// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Localization;

internal class NullLocalizationResolve : ILocalizationResolve
{
    public IEnumerable<LocalizedString> GetAllStringsByCulture(bool includeParentCultures) => [];
}
