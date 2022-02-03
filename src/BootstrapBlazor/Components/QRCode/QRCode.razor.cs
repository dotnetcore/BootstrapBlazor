// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// QRCode 组件
/// </summary>
public partial class QRCode : IDisposable
{
    private ElementReference QRCodeElement { get; set; }

    [NotNull]
    private JSInterop<QRCode>? Interop { get; set; }

    /// <summary>
    /// 获得/设置 二维码生成后回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnGenerated { get; set; }

    /// <summary>
    /// 获得/设置 PlaceHolder 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 是否显示工具按钮 默认 false
    /// </summary>
    [Parameter]
    public bool ShowButtons { get; set; }

    /// <summary>
    /// 获得/设置 清除按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 获得/设置 生成按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? GenerateButtonText { get; set; }

    /// <summary>
    /// 获得/设置 二维码内容信息
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<QRCode>? Localizer { get; set; }

    private string? MethodName { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        GenerateButtonText ??= Localizer[nameof(GenerateButtonText)];

        Interop = new JSInterop<QRCode>(JSRuntime);
    }

    /// <summary>
    /// OnParametersSetAsync 方法
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await Generate();
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!string.IsNullOrEmpty(MethodName))
        {
            await Interop.InvokeVoidAsync(this, QRCodeElement, "bb_qrcode", MethodName, Content ?? "");
            MethodName = null;
        }
    }

    private Task Clear()
    {
        MethodName = "clear";
        return Task.CompletedTask;
    }

    private Task Generate()
    {
        MethodName = string.IsNullOrEmpty(Content) ? "clear" : "generate";
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task Generated()
    {
        if (OnGenerated != null)
        {
            await OnGenerated.Invoke();
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Interop.Dispose();
            Interop = null;
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
