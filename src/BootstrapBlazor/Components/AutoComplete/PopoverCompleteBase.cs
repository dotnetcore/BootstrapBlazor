// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗可悬浮组件基类
/// </summary>
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

        if (firstRender)
        {
            // 汉字多次触发问题
            if (ValidateForm != null)
            {
                await InvokeVoidAsync("composition", Id);
            }

            if (Debounce > 0)
            {
                await InvokeVoidAsync("debounce", Id);
            }
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
}
