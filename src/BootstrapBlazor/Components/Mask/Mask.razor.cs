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

    private string? ClassString => CssBuilder.Default("bb-mask")
        .AddClass("show", _options is { ChildContent: not null })
        .Build();

    private string? StyleString => _options == null ? null : CssBuilder.Default()
        .AddClass($"--bb-mask-zindex: {_options.ZIndex};", _options.ZIndex != null)
        .AddClass($"--bb-mask-bg: {_options.BackgroundColor};", _options.BackgroundColor != null)
        .AddClass($"--bb-mask-opacity: {_options.Opacity};", _options.Opacity != null)
        .Build();

    private MaskOption? _options;

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
}
