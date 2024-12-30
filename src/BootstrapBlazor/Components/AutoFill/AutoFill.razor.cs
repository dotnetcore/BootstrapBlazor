﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoFill 组件
/// </summary>
public partial class AutoFill<TValue>
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("auto-complete auto-fill")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 最终候选数据源
    /// </summary>
    [NotNull]
    private List<TValue>? FilterItems { get; set; }

    /// <summary>
    /// 获得/设置 组件数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// 获得/设置 匹配数据时显示的数量 默认 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// 获得/设置 是否开启模糊查询，默认为 false
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// 获得/设置 匹配时是否忽略大小写，默认为 true
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// 获得/设置 获得焦点时是否展开下拉候选菜单 默认 true
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// 获得/设置 候选项模板
    /// </summary>
    [Parameter]
    public RenderFragment<TValue>? Template { get; set; }

    /// <summary>
    /// 获得/设置 通过模型获得显示文本方法 默认使用 ToString 重载方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 加载图标
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    /// <summary>
    /// 获得 获得焦点自动显示下拉框设置字符串
    /// </summary>
    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private string? _displayText;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoFillIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);

        OnGetDisplayText ??= v => v?.ToString();
        _displayText = OnGetDisplayText(Value);

        FilterItems ??= Items?.ToList() ?? [];
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }
}
