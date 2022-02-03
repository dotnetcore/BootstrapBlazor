// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoComplete 组件基类
/// </summary>
public partial class AutoFill<TValue>
{
    private bool _isLoading;
    private bool _isShown;

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected virtual string? ClassString => CssBuilder.Default("auto-complete")
        .AddClass("is-loading", _isLoading)
        .AddClass("is-complete", _isShown)
        .Build();

    /// <summary>
    /// 获得 最终候选数据源
    /// </summary>
    [NotNull]
    protected List<TValue>? FilterItems { get; private set; }

    /// <summary>
    /// 获得/设置 组件数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// 获得/设置 无匹配数据时显示提示信息 默认提示"无匹配数据"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NoDataTip { get; set; }

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
    /// 获得/设置 自定义集合过滤规则
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnCustomFilter { get; set; }

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
    public Func<TValue, string>? OnGetDisplayText { get; set; }

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
    /// 获得/设置 选项改变回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    private string InputString { get; set; } = "";

    private TValue? ActiveSelectedItem { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected ElementReference AutoFillElement { get; set; }

    /// <summary>
    /// 
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
        Items ??= Enumerable.Empty<TValue>();
        FilterItems ??= new List<TValue>();
        OnGetDisplayText ??= v => v?.ToString() ?? "";
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
            await JSRuntime.InvokeVoidAsync(AutoFillElement, "bb_autoScrollItem", CurrentItemIndex.Value);
        }
    }

    /// <summary>
    /// OnBlur 方法
    /// </summary>
    protected async Task OnBlur()
    {
        _isShown = false;
        if (OnSelectedItemChanged != null && ActiveSelectedItem != null)
        {
            await OnSelectedItemChanged(ActiveSelectedItem);
            ActiveSelectedItem = default;
        }
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    protected virtual async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        InputString = OnGetDisplayText(val);
        ActiveSelectedItem = default;
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
        if (!_isLoading)
        {
            _isLoading = true;
            if (OnCustomFilter != null)
            {
                var items = await OnCustomFilter(InputString);
                FilterItems = items.ToList();
            }
            else
            {
                var items = FindItem();
                FilterItems = DisplayCount == null ? items.ToList() : items.Take(DisplayCount.Value).ToList();
            }
            _isLoading = false;
        }

        var source = FilterItems;
        if (source.Any())
        {
            _isShown = true;
            // 键盘向上选择
            if (args.Key == "ArrowUp")
            {
                var index = 0;
                if (ActiveSelectedItem != null)
                {
                    index = source.IndexOf(ActiveSelectedItem) - 1;
                    if (index < 0)
                    {
                        index = source.Count - 1;
                    }
                }
                ActiveSelectedItem = source[index];
                CurrentItemIndex = index;
            }
            else if (args.Key == "ArrowDown")
            {
                var index = 0;
                if (ActiveSelectedItem != null)
                {
                    index = source.IndexOf(ActiveSelectedItem) + 1;
                    if (index > source.Count - 1)
                    {
                        index = 0;
                    }
                }
                ActiveSelectedItem = source[index];
                CurrentItemIndex = index;
            }
            else if (args.Key == "Escape")
            {
                await OnBlur();
                if (!SkipEsc && OnEscAsync != null)
                {
                    await OnEscAsync(Value);
                }
            }
            else if (args.Key == "Enter")
            {
                if (ActiveSelectedItem == null)
                {
                    ActiveSelectedItem = FindItem().FirstOrDefault();
                }
                if (ActiveSelectedItem != null)
                {
                    InputString = OnGetDisplayText(ActiveSelectedItem);
                }
                await OnBlur();
                if (!SkipEnter && OnEnterAsync != null)
                {
                    await OnEnterAsync(Value);
                }
            }
        }

        IEnumerable<TValue> FindItem()
        {
            var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return IsLikeMatch ?
                Items.Where(s => OnGetDisplayText(s).Contains(InputString, comparison)) :
                Items.Where(s => OnGetDisplayText(s).StartsWith(InputString, comparison));
        }
    }
}
