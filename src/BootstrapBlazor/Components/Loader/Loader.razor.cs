// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Loader
/// </summary>
public partial class Loader
{
    /// <summary>
    /// 文本内容
    /// </summary>
    [Parameter]
    public string Text { get; set; } = "Loading ...";

    /// <summary>
    /// 是否重复播放
    /// </summary>
    [Parameter]
    public bool IsRepeat { get; set; } = true;

    /// <summary>
    /// 重复动画之前的延迟(秒)
    /// </summary>
    [Parameter]
    public double RepeatDelay { get; set; } = 0.75;

    /// <summary>
    /// 动画持续时间(秒)
    /// </summary>
    [Parameter]
    public double Duration { get; set; } = 0.25;
    private string? ClassString { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ClassString = CssBuilder.Default("loader-box").AddClassFromAttributes(AdditionalAttributes).Build();
    }

    /// <inheritdoc/>
    protected override async Task InvokeInitAsync() => await InvokeVoidAsync("init", IsRepeat, RepeatDelay, Duration);
}
