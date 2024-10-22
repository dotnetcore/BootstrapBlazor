// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

internal class NullLocalizationResolve : ILocalizationResolve
{
    public IEnumerable<LocalizedString> GetAllStringsByCulture(bool includeParentCultures) => [];
}
