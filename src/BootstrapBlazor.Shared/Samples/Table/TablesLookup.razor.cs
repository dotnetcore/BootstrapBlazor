// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public partial class TablesLookup
{
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private List<SelectedItem>? DataSource { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await Task.Delay(200);

        Items = Foo.GenerateFoo(Localizer);

        DataSource = new List<SelectedItem>
            {
                new SelectedItem{ Value = "true", Text = Localizer["True"].Value },
                new SelectedItem{ Value = "false", Text = Localizer["False"].Value }
            };
    }
}
