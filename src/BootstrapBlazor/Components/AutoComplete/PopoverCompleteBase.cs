// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗可悬浮组件基类
/// </summary>
[BootstrapModuleAutoLoader("AutoComplete/AutoComplete.razor.js", JSObjectReference = true)]
public abstract class PopoverCompleteBase<TValue> : BootstrapInputBase<TValue>, IPopoverBaseComponent
{
    /// <summary>
    /// 图标主题服务
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// 获得/设置 无匹配数据时显示提示信息 默认提示"无匹配数据"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NoDataTip { get; set; }

    /// <summary>
    /// 获得/设置 是否显示无匹配数据选项 默认 true 显示
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Offset { get; set; }

    /// <summary>
    /// 获得/设置 获得焦点时是否展开下拉候选菜单 默认 true
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// 获得/设置 防抖时间 默认为 0 即不开启
    /// </summary>
    [Parameter]
    public int Debounce { get; set; }

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
    /// 获得/设置 下拉菜单选择回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnSelectedItemChanged { get; set; }

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
    /// 获得/设置 UI 呈现数据集合
    /// </summary>
    [NotNull]
    protected List<string>? FilterItems { get; set; }

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
    /// 防抖时长字符串
    /// </summary>
    protected string? DurationString => Debounce > 0 ? $"{Debounce}" : null;

    /// <summary>
    /// data-bs-toggle 值
    /// </summary>
    protected string? ToggleString => IsPopover ? Constants.DropdownToggleString : null;

    /// <summary>
    /// 偏移量字符串
    /// </summary>
    protected string? OffsetString => IsPopover ? null : Offset;

    /// <summary>
    /// 输入框 Id
    /// </summary>
    protected string InputId => $"{Id}_input";

    /// <summary>
    /// CurrentItemIndex 当前选中项索引
    /// </summary>
    protected int? CurrentItemIndex { get; set; }

    /// <summary>
    /// 弹窗位置字符串
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    /// 获得 获得焦点自动显示下拉框设置字符串
    /// </summary>
    protected string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? null : "false";

    /// <summary>
    /// 获得 CustomClass 字符串
    /// </summary>
    protected virtual string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("shadow", ShowShadow)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Offset ??= "[0, 10]";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (CurrentItemIndex.HasValue)
        {
            await InvokeVoidAsync("autoScroll", Id, CurrentItemIndex.Value);
        }
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    protected async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override string? GetInputId() => InputId;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    /// <summary>
    /// 判断是否为回车键
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected bool IsEnterKey(string key) => key == "Enter" || key == "NumpadEnter";
}
