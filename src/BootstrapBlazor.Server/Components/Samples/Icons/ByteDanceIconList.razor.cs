// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Samples.Icons;

/// <summary>
/// FAIconList 组件
/// </summary>
[JSModuleAutoLoader("Samples/Icons/ByteDanceIconList.razor.js", JSObjectReference = true)]
public partial class ByteDanceIconList : IAsyncDisposable
{
    private string? ClassString => CssBuilder.Default("icon-list")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

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
    private IStringLocalizer<IconDialog>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(UpdateIcon), IsCopy);

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
}
