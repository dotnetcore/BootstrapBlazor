// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Handwritten 手写签名</para>
/// <para lang="en">Handwritten Signature</para>
/// </summary>
[Obsolete("已弃用，请使用 BootstrapBlazor.SignaturePad 代替；Deprecated, use BootstrapBlazor.SignaturePad instead")]
[ExcludeFromCodeCoverage]
public partial class Handwritten
{
    /// <summary>
    /// <para lang="zh">清除按钮文本</para>
    /// <para lang="en">Clear Button Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">保存按钮文本</para>
    /// <para lang="en">Save Button Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Handwritten>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">手写结果回调方法</para>
    /// <para lang="en">Handwritten Result Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<string> HandwrittenBase64 { get; set; }

    /// <summary>
    /// <para lang="zh">手写签名imgBase64字符串</para>
    /// <para lang="en">Handwritten Signature imgBase64 String</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Result { get; set; }

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized Method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnValueChanged));

    /// <summary>
    /// <para lang="zh">保存结果</para>
    /// <para lang="en">Save Result</para>
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
