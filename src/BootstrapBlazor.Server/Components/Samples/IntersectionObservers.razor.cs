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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _images = Enumerable.Range(1, 100).Select(i => "../../images/default.jpeg").ToList();
    }

    private Task OnIntersectingAsync(int index)
    {
        _images[index] = GetImageUrl(index);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static string GetImageUrl(int index) => $"https://picsum.photos/160/160?random={index}";
}
