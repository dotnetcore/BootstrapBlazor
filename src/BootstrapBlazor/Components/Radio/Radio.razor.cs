// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Radio 单选框组件</para>
/// <para lang="en">Radio Component</para>
/// </summary>
[JSModuleNotInherited]
public partial class Radio<TValue> : Checkbox<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 点击回调方法</para>
    /// <para lang="en">Get/Set Click Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Radio 组名称一般来讲需要设置 默认为 null 未设置</para>
    /// <para lang="en">Get/Set Radio Group Name. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? GroupName { get; set; }

    private string? ClassString => CssBuilder.Default("form-check")
        .AddClass("is-checked", State == CheckboxState.Checked)
        .AddClass($"form-check-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass($"form-check-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnClickHandler()
    {
        if (OnClick != null)
        {
            await OnClick(Value);
        }
    }
}
