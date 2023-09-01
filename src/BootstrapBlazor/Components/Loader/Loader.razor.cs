// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Loader
/// </summary>
public partial class Loader
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
    /// 获得/设置 是否重复播放动画 默认为 true 重复
    /// </summary>
    [Parameter]
    public bool IsRepeat { get; set; } = true;

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
    private IStringLocalizer<Loader>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("loader")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? FlipClassString => CssBuilder.Default("loader-flip")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"bg-primary", Color == Color.None)
        .Build();

    private int _columns;
    private bool _isRepeat;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task InvokeInitAsync()
    {
        await InvokeVoidAsync("init", Id, Columns, IsRepeat);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        Text ??= Localizer[nameof(Text)];

        if (Columns != _columns || IsRepeat != _isRepeat)
        {
            await InvokeVoidAsync("update", Id, Columns, IsRepeat);

            _columns = Columns;
            _isRepeat = IsRepeat;
        }
    }
}
