// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Loader
/// </summary>
public partial class Loader
{
    /// <summary>
    /// 文本内容
    /// </summary>
    [Parameter]
    public string Text { get; set; } = "Loading ...";

    /// <summary>
    /// 数据数量
    /// </summary>
    [Parameter]
    public int Columns { get; set; } = 10;

    private string? ClassString { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ClassString = CssBuilder.Default("loader-box").AddClassFromAttributes(AdditionalAttributes).Build();
    }

    /// <inheritdoc/>
    protected override async Task InvokeInitAsync() => await InvokeVoidAsync("init", Id);
}
