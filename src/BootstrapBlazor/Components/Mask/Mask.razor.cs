// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
            await InvokeVoidAsync("update", Id, new
            {
                Show = _options != null,
                _options?.ContainerId,
                _options?.Selector
            });
        }
    }

    private Task Show(MaskOption? option)
    {
        _options = option;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
