// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// QRCode 组件
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor.BarCode/qrcode.bundle.min.js", ModuleName = "BlazorQRCode", Relative = false)]
public partial class QRCode : IAsyncDisposable
{
    private ElementReference QRCodeElement { get; set; }

    private string? ClassString => CssBuilder.Default("qrcode")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ImageStyleString => $"width: {Width}px; height: {Width}px;";

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
    /// 获得/设置 清除按钮图标
    /// </summary>
    [Parameter]
    public string? ClearButtonIcon { get; set; } = "fa-solid fa-xmark";

    /// <summary>
    /// 获得/设置 生成按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? GenerateButtonText { get; set; }

    /// <summary>
    /// 获得/设置 二维码白色区域颜色 默认 #ffffff
    /// </summary>
    [Parameter]
    public string? LightColor { get; set; } = "#ffffff";

    /// <summary>
    /// 获得/设置 二维码黑色区域颜色 默认 #000000
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DarkColor { get; set; } = "#000000";

    /// <summary>
    /// 获得/设置 生成按钮图标
    /// </summary>
    [Parameter]
    public string? GenerateButtonIcon { get; set; } = "fa-solid fa-qrcode";

    /// <summary>
    /// 获得/设置 二维码内容信息
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 二维码内容宽度 高度与宽度一致 最小值为 40 默认值 128
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 128;

    private string? MethodName { get; set; } = "generate";

    [Inject]
    [NotNull]
    private IStringLocalizer<QRCode>? Localizer { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Width = Math.Max(40, Width);
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        GenerateButtonText ??= Localizer[nameof(GenerateButtonText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task ModuleInitAsync()
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync($"{ModuleName}.init", QRCodeElement, MethodName, Content, nameof(Generated));
        }
        MethodName = null;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task ModuleExecuteAsync()
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync($"{ModuleName}.execute", QRCodeElement, MethodName, Content);
        }
        MethodName = null;
    }

    private void Clear() => MethodName = "clear";

    private void Generate() => MethodName = string.IsNullOrEmpty(Content) ? "clear" : "generate";

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task Generated()
    {
        if (OnGenerated != null)
        {
            await OnGenerated();
        }
    }
}
