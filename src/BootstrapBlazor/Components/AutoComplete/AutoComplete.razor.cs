// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoComplete 组件基类
/// </summary>
public partial class AutoComplete
{
    private bool IsLoading { get; set; }

    private bool IsShown { get; set; }

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected virtual string? ClassString => CssBuilder.Default("auto-complete")
        .AddClass("is-loading", IsLoading)
        .AddClass("is-complete", IsShown)
        .Build();

    /// <summary>
    /// 获得 最终候选数据源
    /// </summary>
    [NotNull]
    protected List<string>? FilterItems { get; private set; }

    /// <summary>
    /// 获得/设置 通过输入字符串获得匹配数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<string>? Items { get; set; }

    /// <summary>
    /// 获得/设置 无匹配数据时显示提示信息 默认提示"无匹配数据"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NoDataTip { get; set; }

    /// <summary>
    /// 获得/设置 匹配数据时显示的数量
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// 获得/设置 是否开启模糊查询，默认为 false
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; } = false;

    /// <summary>
    /// 获得/设置 匹配时是否忽略大小写，默认为 true
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// 获得/设置 自定义集合过滤规则
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<string>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// 获得/设置 下拉菜单选择回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// 获得/设置 防抖时间 默认为 0 即不开启
    /// </summary>
    [Parameter]
    public int Debounce { get; set; }

    /// <summary>
    /// 获得/设置 是否跳过 Enter 按键处理 默认 false
    /// </summary>
    [Parameter]
    public bool SkipEnter { get; set; }

    /// <summary>
    /// 获得/设置 是否跳过 Esc 按键处理 默认 false
    /// </summary>
    [Parameter]
    public bool SkipEsc { get; set; }

    /// <summary>
    /// IStringLocalizer 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    private string CurrentSelectedItem { get; set; } = "";

    /// <summary>
    /// ElementReference 实例
    /// </summary>
    protected ElementReference AutoCompleteElement { get; set; }

    /// <summary>
    /// CurrentItemIndex 当前选中项索引
    /// </summary>
    protected int? CurrentItemIndex { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Items ??= Enumerable.Empty<string>();
        FilterItems ??= new List<string>();

        SkipRegisterEnterEscJSInvoke = true;
    }

    /// <summary>
    /// firstRender
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (CurrentItemIndex.HasValue)
        {
            await JSRuntime.InvokeVoidAsync(AutoCompleteElement, "bb_autoScrollItem", CurrentItemIndex.Value);
        }

        if (firstRender)
        {
            if (Debounce > 0)
            {
                await JSRuntime.InvokeVoidAsync(FocusElement, "bb_setDebounce", Debounce);
            }
        }
    }

    /// <summary>
    /// OnBlur 方法
    /// </summary>
    protected void OnBlur()
    {
        CurrentSelectedItem = "";
        IsShown = false;
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    protected virtual async Task OnClickItem(string val)
    {
        CurrentValue = val;
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    /// <summary>
    /// OnKeyUp 方法
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected virtual async Task OnKeyUp(KeyboardEventArgs args)
    {
        if (!IsLoading)
        {
            IsLoading = true;
            if (OnCustomFilter != null)
            {
                var items = await OnCustomFilter(CurrentValueAsString);
                FilterItems = items.ToList();
            }
            else
            {
                var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                var items = IsLikeMatch ?
                    Items.Where(s => s.Contains(CurrentValueAsString, comparison)) :
                    Items.Where(s => s.StartsWith(CurrentValueAsString, comparison));
                FilterItems = DisplayCount == null ? items.ToList() : items.Take(DisplayCount.Value).ToList();
            }
            IsLoading = false;
        }

        IsShown = true;

        var source = FilterItems;
        if (source.Any())
        {
            // 键盘向上选择
            if (args.Key == "ArrowUp")
            {
                var index = source.IndexOf(CurrentSelectedItem) - 1;
                if (index < 0)
                {
                    index = source.Count - 1;
                }
                CurrentSelectedItem = source[index];
                CurrentItemIndex = index;
            }
            else if (args.Key == "ArrowDown")
            {
                var index = source.IndexOf(CurrentSelectedItem) + 1;
                if (index > source.Count - 1)
                {
                    index = 0;
                }
                CurrentSelectedItem = source[index];
                CurrentItemIndex = index;
            }
            else if (args.Key == "Escape")
            {
                OnBlur();
                if (!SkipEsc && OnEscAsync != null)
                {
                    await OnEscAsync(Value);
                }
            }
            else if (args.Key == "Enter")
            {
                if (!string.IsNullOrEmpty(CurrentSelectedItem))
                {
                    CurrentValueAsString = CurrentSelectedItem;
                    if (OnSelectedItemChanged != null)
                    {
                        await OnSelectedItemChanged(CurrentSelectedItem);
                    }
                }

                OnBlur();
                if (!SkipEnter && OnEnterAsync != null)
                {
                    await OnEnterAsync(Value);
                }
            }
        }
    }
}
