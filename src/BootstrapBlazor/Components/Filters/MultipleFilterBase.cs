// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone


namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class MultipleFilterBase : FilterBase
{
    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 获得/设置 增加过滤条件图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// 获得/设置 减少过滤条件图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示增加减少条件按钮
    /// </summary>
    public bool ShowMoreButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 条件数量
    /// </summary>
    protected int Count { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterPlusIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterMinusIcon);
    }

    /// <summary>
    /// 点击增加按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected void OnClickPlus()
    {
        if (Count == 0)
        {
            Count++;
        }
    }

    /// <summary>
    /// 点击减少按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected void OnClickMinus()
    {
        if (Count == 1)
        {
            Count--;
        }
    }
}
