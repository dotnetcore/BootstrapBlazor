// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Mask 组件
/// </summary>
public partial class Mask
{
    [Inject]
    [NotNull]
    private MaskService? MaskService { get; set; }

    private string? ClassString => CssBuilder.Default("bb-mask fade")
        .AddClass("show", IsMasking)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-mask-zindex: {_options.ZIndex};", _options.ZIndex != null)
        .AddClass($"--bb-mask-bg: {_options.BackgroupColor};", _options.BackgroupColor != null)
        .AddClass($"--bb-mask-opacity: {_options.Opacity};", _options.Opacity != null)
        .Build();

    private bool IsMasking { get; set; }

    [NotNull]
    private MaskOption? _options = default;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        MaskService.Register(this, Show);
    }

    private Task Show(MaskOption? option)
    {
        _options = option;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task Close()
    {
        _options.ChildContent = null;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
