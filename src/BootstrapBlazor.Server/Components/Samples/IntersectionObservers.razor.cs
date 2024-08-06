// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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

    private Task OnIntersectingAsync(bool intersectioning, int index)
    {
        if (intersectioning)
        {
            _images[index] = GetImageUrl(index);
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private async Task OnLoadMoreAsync(bool intersectioning, int index)
    {
        if (intersectioning)
        {
            await Task.Delay(1000);
            _items.AddRange(Enumerable.Range(_items.Count + 1, 20).Select(i => $"https://picsum.photos/160/160?random={i}"));
            StateHasChanged();
        }
    }

    private async Task OnVisibleChanged(bool intersectioning, int index)
    {
        if (intersectioning)
        {
            await _video.Play();
        }
        else
        {
            await _video.Pause();
        }
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
            Name = "AutoUnobserve",
            Description = Localizer["AttributeAutoUnobserve"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
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
