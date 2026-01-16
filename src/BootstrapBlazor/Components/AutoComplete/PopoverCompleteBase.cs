// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">弹窗可悬浮组件基类</para>
/// <para lang="en">Base class for popover complete component</para>
/// </summary>
[BootstrapModuleAutoLoader("AutoComplete/AutoComplete.razor.js", JSObjectReference = true)]
public abstract class PopoverCompleteBase<TValue> : BootstrapInputBase<TValue>, IPopoverBaseComponent
{
    /// <summary>
    /// <para lang="zh">图标主题服务</para>
    /// <para lang="en">Icon theme service</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 无匹配数据时显示提示信息 默认提示"无匹配数据"</para>
    /// <para lang="en">Gets or sets the tip info when no matching data. Default is "No matched data"</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NoDataTip { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [Parameter]
    public string? Offset { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 防抖时间 默认为 0 即不开启</para>
    /// <para lang="en">Gets or sets the debounce time. Default is 0 (disabled)</para>
    /// </summary>
    [Parameter]
    public int Debounce { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉菜单选择回调方法 默认 null</para>
    /// <para lang="en">Gets or sets the callback method for dropdown item selection. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否跳过 Enter 按键处理 默认 false</para>
    /// <para lang="en">Gets or sets whether to skip Enter key processing. Default is false</para>
    /// </summary>
    [Parameter]
    public bool SkipEnter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否跳过 Esc 按键处理 默认 false</para>
    /// <para lang="en">Gets or sets whether to skip Esc key processing. Default is false</para>
    /// </summary>
    [Parameter]
    public bool SkipEsc { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动行为 默认 <see cref="ScrollIntoViewBehavior.Smooth"/></para>
    /// <para lang="en">Gets or sets the scroll behavior. Default is <see cref="ScrollIntoViewBehavior.Smooth"/></para>
    /// </summary>
    [Parameter]
    public ScrollIntoViewBehavior ScrollIntoViewBehavior { get; set; } = ScrollIntoViewBehavior.Smooth;

    /// <summary>
    /// <para lang="zh">获得/设置 候选项模板 默认 null</para>
    /// <para lang="en">Gets or sets the item template. Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TValue>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择组件是否可清除 默认为 false</para>
    /// <para lang="en">Gets or sets whether the select component is clearable. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧清除图标 默认为 fa-solid fa-angle-up</para>
    /// <para lang="en">Gets or sets the right-side clear icon. Default is fa-solid fa-angle-up.</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh"><see cref="BootstrapBlazorOptions"/> 配置类实例</para>
    /// <para lang="en"><see cref="BootstrapBlazorOptions"/> configuration instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    /// <summary>
    /// <para lang="zh">获得 是否跳过 ESC 按键字符串</para>
    /// <para lang="en">Gets the skip ESC key string</para>
    /// </summary>
    protected string? SkipEscString => SkipEsc ? "true" : null;

    /// <summary>
    /// <para lang="zh">获得 是否跳过 Enter 按键字符串</para>
    /// <para lang="en">Gets the skip Enter key string</para>
    /// </summary>
    protected string? SkipEnterString => SkipEnter ? "true" : null;

    /// <summary>
    /// <para lang="zh">获得 滚动行为字符串</para>
    /// <para lang="en">Gets the scroll behavior string</para>
    /// </summary>
    protected string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

    /// <summary>
    /// <para lang="zh">防抖时长字符串</para>
    /// <para lang="en">Debounce duration string</para>
    /// </summary>
    protected string? DurationString => Debounce > 0 ? $"{Debounce}" : null;

    /// <summary>
    /// <para lang="zh">data-bs-toggle 值</para>
    /// <para lang="en">data-bs-toggle value</para>
    /// </summary>
    protected string? ToggleString => IsPopover ? Constants.DropdownToggleString : null;

    /// <summary>
    /// <para lang="zh">偏移量字符串</para>
    /// <para lang="en">Offset string</para>
    /// </summary>
    protected string? OffsetString => IsPopover ? null : Offset;

    /// <summary>
    /// <para lang="zh">输入框 Id</para>
    /// <para lang="en">Input Id</para>
    /// </summary>
    protected string InputId => $"{Id}_input";

    /// <summary>
    /// <para lang="zh">弹窗位置字符串</para>
    /// <para lang="en">Popover placement string</para>
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    /// <para lang="zh">获得输入框 Id</para>
    /// <para lang="en">Gets the input Id</para>
    /// </summary>
    protected override string? GetInputId() => InputId;

    /// <summary>
    /// <para lang="zh">获得 CustomClass 字符串</para>
    /// <para lang="en">Gets the CustomClass string</para>
    /// </summary>
    protected virtual string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("shadow", ShowShadow)
        .Build();

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputClearIcon);

        Offset ??= "[0, 6]";
        IsPopover |= BootstrapBlazorOptions.Value.IsPopover;
    }

    /// <summary>
    /// <para lang="zh">触发 OnBlurAsync 回调前回调方法</para>
    /// <para lang="en">Callback method before triggering OnBlurAsync</para>
    /// </summary>
    /// <returns></returns>
    protected virtual Task OnBeforeBlurAsync() => Task.CompletedTask;

    /// <summary>
    /// <para lang="zh">触发 OnBlur 回调方法 由 Javascript 触发</para>
    /// <para lang="en">Trigger OnBlur callback method. Triggered by Javascript</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerBlur()
    {
        await OnBeforeBlurAsync();

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }
}
