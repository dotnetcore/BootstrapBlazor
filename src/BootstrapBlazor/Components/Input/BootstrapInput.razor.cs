// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInput 组件
/// </summary>
public partial class BootstrapInput<TValue>
{
    /// <summary>
    /// 获得/设置 是否为只读 默认 false
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 用户删除后是否自动更改为默认值 0 默认 false
    /// </summary>
    [Parameter]
    public bool AutoSetDefaultWhenNull { get; set; }

    /// <summary>
    /// 获得/设置 失去焦点回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnBlurAsync { get; set; }

    private string? ReadonlyString => Readonly ? "true" : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        bool ret;
        var v = IsTrim ? value.Trim() : value;
        if (AutoSetDefaultWhenNull && string.IsNullOrEmpty(v))
        {
            result = default!;
            validationErrorMessage = null;
            ret = true;
        }
        else
        {
            ret = base.TryParseValueFromString(v, out result, out validationErrorMessage);
        }
        return ret;
    }

    /// <summary>
    /// OnBlur 方法
    /// </summary>
    protected virtual async Task OnBlur()
    {
        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }
}
