// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// IStringLocalizer 实现类
/// </summary>
internal class StringLocalizer : IStringLocalizer
{
    private readonly IStringLocalizer _localizer;

    public StringLocalizer(IStringLocalizerFactory factory, IOptions<JsonLocalizationOptions> options)
    {
        _localizer = factory.Create(options.Value.ResourceManagerStringLocalizerType!);
    }

    public LocalizedString this[string name] => _localizer[name];

    public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);
}
