// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoComplete 组件
/// </summary>
public partial class AutoComplete
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected virtual string? ClassString => CssBuilder.Default("auto-complete")
        .Build();

    /// <summary>
    /// 获得/设置 通过输入字符串获得匹配数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<string>? Items { get; set; }

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
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// 获得/设置 OnFocus 时是否过滤选择 默认 false
    /// </summary>
    [Parameter]
    public bool OnFocusFilter { get; set; }

    /// <summary>
    /// 获得/设置 匹配时是否忽略大小写，默认为 true
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// 获得/设置 自定义集合过滤规则 默认 null
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<string>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// 获得/设置 下拉菜单选择回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnSelectedItemChanged { get; set; }

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
    /// 获得/设置 滚动行为 默认 <see cref="ScrollIntoViewBehavior.Smooth"/>
    /// </summary>
    [Parameter]
    public ScrollIntoViewBehavior ScrollIntoViewBehavior { get; set; } = ScrollIntoViewBehavior.Smooth;

    /// <summary>
    /// 获得/设置 候选项模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<string>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 加载图标
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// IStringLocalizer 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    private string CurrentSelectedItem { get; set; } = "";

    private List<string>? _items;

    private string? SkipEscString => SkipEsc ? "true" : null;

    private string? SkipEnterString => SkipEnter ? "true" : null;

    private string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        SkipRegisterEnterEscJSInvoke = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoCompleteIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);

        _items = Items?.ToList();
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
    /// <param name="key"></param>
    /// <returns></returns>
    [JSInvokable]
    public virtual async Task OnKeyUp(string key)
    {
        await Task.Delay(0);

        //var source = FilterItems;
        //if (source.Count > 0)
        //{
        //    // 键盘向上选择
        //    if (key == "ArrowUp")
        //    {
        //        var index = source.IndexOf(CurrentSelectedItem) - 1;
        //        if (index < 0)
        //        {
        //            index = source.Count - 1;
        //        }
        //        CurrentSelectedItem = source[index];
        //        CurrentItemIndex = index;
        //    }
        //    else if (key == "ArrowDown")
        //    {
        //        var index = source.IndexOf(CurrentSelectedItem) + 1;
        //        if (index > source.Count - 1)
        //        {
        //            index = 0;
        //        }
        //        CurrentSelectedItem = source[index];
        //        CurrentItemIndex = index;
        //    }
        //    else if (key == "Escape")
        //    {
        //        await OnBlur();
        //        if (!SkipEsc && OnEscAsync != null)
        //        {
        //            await OnEscAsync(Value);
        //        }
        //    }
        //    else if (IsEnterKey(key))
        //    {
        //        if (!string.IsNullOrEmpty(CurrentSelectedItem))
        //        {
        //            CurrentValueAsString = CurrentSelectedItem;
        //            if (OnSelectedItemChanged != null)
        //            {
        //                await OnSelectedItemChanged(CurrentSelectedItem);
        //            }
        //        }

        //        await OnBlur();
        //        if (!SkipEnter && OnEnterAsync != null)
        //        {
        //            await OnEnterAsync(Value);
        //        }
        //    }
        //}
        //await CustomKeyUp(key);
        //StateHasChanged();
    }

    /// <summary>
    /// 自定义按键处理方法
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected virtual Task CustomKeyUp(string key) => Task.CompletedTask;

    /// <summary>
    /// TriggerOnChange 方法
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public async Task TriggerOnChange(string val)
    {
        if (OnCustomFilter != null)
        {
            var items = await OnCustomFilter(val);
            _items = items.ToList();
        }
        else
        {
            var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            var items = IsLikeMatch
                ? Items.Where(s => s.Contains(val, comparison))
                : Items.Where(s => s.StartsWith(val, comparison));
            _items = items.ToList();
        }

        if (DisplayCount != null)
        {
            _items = _items.Take(DisplayCount.Value).ToList();
        }

        CurrentValue = val;
        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 出发 OnBlur 回调方法 由 Javascript 触发
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerBlur()
    {
        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }
}
