// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 可选择表格组件示例
/// </summary>
public partial class SelectTables
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    private Foo? Foo { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        Items = Foo.GenerateFoo(Localizer);
    }

    private static string? GetTextCallback(Foo? foo) => foo?.Name;
}
