// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// QRCode 组件
/// </summary>
public partial class QRCode
{
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
    /// 获得/设置 二维码清除后回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnCleared { get; set; }

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

    [Inject]
    [NotNull]
    private IStringLocalizer<QRCode>? Localizer { get; set; }

    private string? _content;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _content = Content;
    }

    /// <summary>
    /// <inheritdoc/>
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
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_content != Content)
        {
            _content = Content;
            await InvokeVoidAsync("update", Id, Content);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Content, nameof(Generated));

    private async Task OnClickClear()
    {
        Content = "";
        if (OnCleared != null)
        {
            await OnCleared();
        }
    }

    private Task OnClickGenerate()
    {
        return Task.CompletedTask;
    }

    private Task OnValueChanged(string? v)
    {
        Content = v;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 二维码生成后回调方法由 JavaScript 调用
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
