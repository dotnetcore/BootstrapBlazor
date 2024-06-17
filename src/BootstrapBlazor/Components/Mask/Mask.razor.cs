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
        .AddClass("show", _options != null)
        .Build();

    private string? GetStyleString()
    {
        string? ret = null;
        if (_options != null)
        {
            ret = CssBuilder.Default()
               .AddClass($"--bb-mask-zindex: {_options.ZIndex};", _options.ZIndex != null)
               .AddClass($"--bb-mask-bg: {_options.BackgroupColor};", _options.BackgroupColor != null)
               .AddClass($"--bb-mask-opacity: {_options.Opacity};", _options.Opacity != null)
               .Build();
        }
        return ret;
    }

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

    private Task Close()
    {
        _options = null;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
