// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapLabel 组件
/// </summary>
public partial class BootstrapLabel : IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 组件值 显示文本 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 false 始终不显示
    /// </summary>
    [Parameter]
    [NotNull]
    public bool? ShowLabelTooltip { get; set; }

    private ElementReference LabelElement { get; set; }

    private bool Init { get; set; }

    private string? ClassString => CssBuilder.Default("form-label")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// OnParametersSetAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        ShowLabelTooltip ??= false;
        Value ??= "";

        if (Init)
        {
            var method = ShouldInvokeTooltip() ? "init" : "dispose";
            await InvokeTooltip(method);
        }
    }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Init = true;
            if (ShouldInvokeTooltip())
            {
                await InvokeTooltip("init");
            }
        }
    }

    private bool ShouldInvokeTooltip() => ShowLabelTooltip.Value && !string.IsNullOrEmpty(Value);

    private ValueTask InvokeTooltip(string method) => JSRuntime.InvokeVoidAsync(LabelElement, "bb_showLabelTooltip", method, Value);

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (Init)
            {
                await InvokeTooltip("dispose");
            }
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
