﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 交叉检测组件示例
/// </summary>
public partial class IntersectionObservers
{
    private List<string> _images = default!;

    private List<string> _items = default!;

    private VideoDemo _video = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _images = Enumerable.Range(1, 100).Select(i => "../../images/default.jpeg").ToList();
        _items = Enumerable.Range(1, 20).Select(i => $"https://picsum.photos/160/160?random={i}").ToList();
    }

    private Task OnIntersectingAsync(IntersectionObserverEntry entry)
    {
        if (entry.IsIntersecting)
        {
            _images[entry.Index] = GetImageUrl(entry.Index);
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private async Task OnLoadMoreAsync(IntersectionObserverEntry entry)
    {
        if (entry.IsIntersecting)
        {
            await Task.Delay(1000);
            _items.AddRange(Enumerable.Range(_items.Count + 1, 20).Select(i => $"https://picsum.photos/160/160?random={i}"));
            StateHasChanged();
        }
    }

    private string? _videoStateString;
    private string? _textColorString = "text-muted";

    private async Task OnVisibleChanged(IntersectionObserverEntry entry)
    {
        if (entry.IsIntersecting)
        {
            _videoStateString = Localizer["IntersectionObserverVisiblePlayLabel"];
            _textColorString = "text-success";
            await _video.Play();
        }
        else
        {
            _videoStateString = Localizer["IntersectionObserverVisiblePauseLabel"];
            _textColorString = "text-danger";
            await _video.Pause();
        }
        StateHasChanged();
    }

    private static string GetImageUrl(int index) => $"https://picsum.photos/160/160?random={index}";

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "UseElementViewport",
            Description = Localizer["AttributeUseElementViewport"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "RootMargin",
            Description = Localizer["AttributeRootMargin"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Threshold",
            Description = Localizer["AttributeThreshold"],
            Type = "string?",
            ValueList = "0.0 — 1.0|[]",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(IntersectionObserver.AutoUnobserveWhenIntersection),
            Description = Localizer["AttributeAutoUnobserveWhenIntersection"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(IntersectionObserver.AutoUnobserveWhenNotIntersection),
            Description = Localizer["AttributeAutoUnobserveWhenNotIntersection"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnIntersecting",
            Description = Localizer["AttributeOnIntersectingAsync"],
            Type = "Func<bool, int, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["AttributeChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
