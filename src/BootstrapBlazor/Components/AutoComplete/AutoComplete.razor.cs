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

    private List<string> _items = [];

    /// <summary>
    /// 获得 是否跳过 ESC 按键字符串
    /// </summary>
    protected string? SkipEscString => SkipEsc ? "true" : null;

    /// <summary>
    /// 获得 是否跳过 Enter 按键字符串
    /// </summary>
    protected string? SkipEnterString => SkipEnter ? "true" : null;

    /// <summary>
    /// 获得 滚动行为字符串
    /// </summary>
    protected string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

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

        _items = Items?.ToList() ?? [];
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
