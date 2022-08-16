// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class InputGroups
{
    private string BindValue { get; set; } = string.Empty;

    private string StringAt { get; set; } = "@";

    private string StringMailServer { get; set; } = "163.com";

    private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
    {
        new SelectedItem ("", "请选择 ..."),
        new SelectedItem ("Beijing", "北京"),
        new SelectedItem ("Shanghai", "上海")
    };

    [NotNull]
    private Foo? Model { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    private string? GroupFormClassString => CssBuilder.Default("row g-3")
        .AddClass("form-inline", FormRowType == RowType.Inline)
        .Build();

    private RowType FormRowType { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Model = Foo.Generate(LocalizerFoo);
    }
}
