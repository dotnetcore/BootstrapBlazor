// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// InputGroup 组件示例
/// </summary>
public partial class InputGroups
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Foo? Model { get; set; }

    private string BindValue { get; set; } = string.Empty;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Model = Foo.Generate(LocalizerFoo);
    }

    private string StringAt { get; set; } = "@";

    private string StringMailServer { get; set; } = "163.com";

    private readonly IEnumerable<SelectedItem> Items = new SelectedItem[]
    {
        new SelectedItem("", "请选择 ..."),
        new SelectedItem("Beijing", "北京"),
        new SelectedItem("Shanghai", "上海")
    };
    private string? GroupFormClassString => CssBuilder.Default("row g-3").AddClass("form-inline", FormRowType == RowType.Inline).Build();

    private RowType FormRowType { get; set; }
}
