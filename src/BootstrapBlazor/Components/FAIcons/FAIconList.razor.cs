// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class FAIconList
{
    private ElementReference IconListElement { get; set; }

    private string? ClassString => CssBuilder.Default("icon-list")
        .AddClass("is-catalog", ShowCatalog)
        .AddClass("is-dialog", ShowCopyDialog)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 点击时是否显示高级拷贝弹窗 默认 false 直接拷贝到粘贴板
    /// </summary>
    [Parameter]
    public bool ShowCopyDialog { get; set; }

    /// <summary>
    /// 获得/设置 是否显示目录 默认 false 开启后需要自行增加样式
    /// </summary>
    [Parameter]
    public bool ShowCatalog { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    private JSInterop<FAIconList>? Interop { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            Interop ??= new(JSRuntime);
            await Interop.InvokeVoidAsync(this, IconListElement, "bb_iconList", nameof(ShowDialog));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task ShowDialog(string text) => DialogService.ShowCloseDialog<IconDialog>("请选择图标", parameters =>
    {
        parameters.Add(nameof(IconDialog.IconName), text);
    });
}
