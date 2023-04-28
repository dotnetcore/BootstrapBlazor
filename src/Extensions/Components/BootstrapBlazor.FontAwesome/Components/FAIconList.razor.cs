﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// FAIconList 组件
/// </summary>
public partial class FAIconList : BootstrapComponentBase, IAsyncDisposable
{
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
    /// 获得/设置 是否显示目录 默认 false
    /// </summary>
    [Parameter]
    public bool ShowCatalog { get; set; }

    /// <summary>
    /// 获得/设置 高级弹窗 Header 显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DialogHeaderText { get; set; }

    /// <summary>
    /// 获得/设置 当前选择图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 当前选择图标回调方法
    /// </summary>
    [Parameter]
    public EventCallback<string?> IconChanged { get; set; }

    /// <summary>
    /// 获得/设置 点击图标是否进行拷贝处理 默认 false
    /// </summary>
    [Parameter]
    public bool IsCopy { get; set; }

    /// <summary>
    /// 获得/设置 拷贝成功提示文字
    /// </summary>
    [Parameter]
    public string? CopiedTooltipText { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<IconDialog>? Localizer { get; set; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<FAIconList>? Interop { get; set; }

    /// <summary>
    /// 获得/设置 EChart DOM 元素实例
    /// </summary>
    private ElementReference Element { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DialogHeaderText ??= Localizer[nameof(DialogHeaderText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            // import JavaScript
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.FontAwesome/Components/FAIconList.razor.js");
            Interop = DotNetObjectReference.Create(this);
            await Module.InvokeVoidAsync("init", Element, Interop, nameof(UpdateIcon), nameof(ShowDialog), IsCopy);
        }
    }

    /// <summary>
    /// UpdateIcon 方法由 JS Invoke 调用
    /// </summary>
    /// <param name="icon"></param>
    [JSInvokable]
    public async Task UpdateIcon(string icon)
    {
        Icon = icon;
        if (IconChanged.HasDelegate)
        {
            await IconChanged.InvokeAsync(Icon);
        }
        else
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// ShowDialog 方法由 JS Invoke 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task ShowDialog(string text) => DialogService.ShowCloseDialog<IconDialog>(DialogHeaderText, parameters =>
    {
        parameters.Add(nameof(IconDialog.IconName), text);
    });

    #region Dispose
    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            Interop?.Dispose();

            if (Module != null)
            {
                await Module.InvokeVoidAsync("dispose", Element);
                await Module.DisposeAsync();
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
