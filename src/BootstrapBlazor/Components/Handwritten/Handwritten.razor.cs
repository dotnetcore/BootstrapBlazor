// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Handwritten 手写签名
/// </summary>
[Obsolete("已弃用，请使用 BootstrapBlazor.SignaturePad 代替；Deprecated, use BootstrapBlazor.SignaturePad instead")]
[ExcludeFromCodeCoverage]
public partial class Handwritten
{
    /// <summary>
    /// 清除按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 保存按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Handwritten>? Localizer { get; set; }

    /// <summary>
    /// 手写结果回调方法
    /// </summary>
    [Parameter]
    public EventCallback<string> HandwrittenBase64 { get; set; }

    /// <summary>
    /// 手写签名imgBase64字符串
    /// </summary>
    [Parameter]
    public string? Result { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnValueChanged));

    /// <summary>
    /// 保存结果
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnValueChanged(string val)
    {
        Result = val;
        StateHasChanged();
        await HandwrittenBase64.InvokeAsync(val);
    }
}
