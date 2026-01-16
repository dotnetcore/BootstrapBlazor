// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IStringLocalizer 实现类</para>
/// <para lang="en">IStringLocalizer implementation class</para>
/// </summary>
internal class StringLocalizer : IStringLocalizer
{
    private readonly IStringLocalizer _localizer;

    public StringLocalizer(IStringLocalizerFactory factory, IOptions<JsonLocalizationOptions> options)
    {
        if (options.Value.ResourceManagerStringLocalizerType == null)
        {
            throw new InvalidOperationException();
        }
        _localizer = factory.Create(options.Value.ResourceManagerStringLocalizerType);
    }

    public LocalizedString this[string name] => _localizer[name];

    public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);
}
