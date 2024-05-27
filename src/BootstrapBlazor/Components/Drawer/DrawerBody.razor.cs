// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DrawerBody 组件
/// </summary>
public partial class DrawerBody
{
    /// <summary>
    /// 获得/设置 <see cref="DrawerOption"/> 实例
    /// </summary>
    [Parameter, NotNull]
    public DrawerOption? Option { get; set; }

    [CascadingParameter]
    private Func<DrawerOption, Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// 获得 抽屉 Style 字符串
    /// </summary>
    private string? DrawerStyleString => CssBuilder.Default()
        .AddClass($"--bb-drawer-width: {Option.Width};", !string.IsNullOrEmpty(Option.Width) && Option.Placement != Placement.Top && Option.Placement != Placement.Bottom)
        .AddClass($"--bb-drawer-height: {Option.Height};", !string.IsNullOrEmpty(Option.Height) && (Option.Placement == Placement.Top || Option.Placement == Placement.Bottom))
        .Build();

    /// <summary>
    /// 获得 抽屉样式
    /// </summary>
    private string? DrawerClassString => CssBuilder.Default("drawer-body")
        .AddClass("left", Option.Placement != Placement.Right && Option.Placement != Placement.Top && Option.Placement != Placement.Bottom)
        .AddClass("top", Option.Placement == Placement.Top)
        .AddClass("right", Option.Placement == Placement.Right)
        .AddClass("bottom", Option.Placement == Placement.Bottom)
        .Build();

    /// <summary>
    /// 关闭抽屉方法
    /// </summary>
    /// <returns></returns>
    public async Task Close()
    {
        if (OnCloseAsync != null)
        {
            await OnCloseAsync(Option);
        }
    }
}
