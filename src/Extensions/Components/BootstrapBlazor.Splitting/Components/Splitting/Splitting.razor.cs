// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Loader
/// </summary>
public partial class Splitting
{
    /// <summary>
    /// 获得/设置 文本内容
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 是否显示加载文本 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowLoadingText { get; set; } = true;

    /// <summary>
    /// 获得/设置 重复播放动画次数 默认为 -1 无限循环
    /// </summary>
    [Parameter]
    public int Repeat { get; set; } = -1;

    /// <summary>
    /// 获得/设置 数据数量 默认 10
    /// </summary>
    [Parameter]
    public int Columns { get; set; } = 10;

    /// <summary>
    /// 获得/设置 进度条颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    [Inject]
    [NotNull]
    private IStringLocalizer<Splitting>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("splitting")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? FlipClassString => CssBuilder.Default("splitting-flip")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"bg-primary", Color == Color.None)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= Localizer[nameof(Text)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id);
        }
    }
}
