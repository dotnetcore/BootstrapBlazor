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
            await InvokeVoidAsync("update", Id, new { Show = _options != null, _options?.ContainerId });
        }
    }

    private Task Show(MaskOption? option)
    {
        _options = option;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
