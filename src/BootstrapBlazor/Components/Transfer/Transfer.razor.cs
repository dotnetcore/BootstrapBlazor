// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 穿梭框组件
/// </summary>
public partial class Transfer<TValue>
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Transfer<TValue>>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 按钮文本样式
    /// </summary>
    private string? LeftButtonClassName => CssBuilder.Default()
        .AddClass("d-none", string.IsNullOrEmpty(LeftButtonText))
        .Build();

    /// <summary>
    /// 获得/设置 按钮文本样式
    /// </summary>
    private string? RightButtonClassName => CssBuilder.Default("me-1")
        .AddClass("d-none", string.IsNullOrEmpty(RightButtonText))
        .Build();

    private string? ValidateClass => CssBuilder.Default()
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得/设置 左侧数据集合
    /// </summary>
    private List<SelectedItem> LeftItems { get; } = new List<SelectedItem>();

    /// <summary>
    /// 获得/设置 右侧数据集合
    /// </summary>
    private List<SelectedItem> RightItems { get; } = new List<SelectedItem>();

    /// <summary>
    /// 获得/设置 组件绑定数据项集合
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 选中项集合发生改变时回调委托方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem>, Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    /// 获得/设置 左侧面板 Header 显示文本
    /// </summary>
    [Parameter]
    public string? LeftPanelText { get; set; }

    /// <summary>
    /// 获得/设置 右侧面板 Header 显示文本
    /// </summary>
    [Parameter]
    public string? RightPanelText { get; set; }

    /// <summary>
    /// 获得/设置 向左侧转移图标
    /// </summary>
    [Parameter]
    public string? LeftIcon { get; set; }

    /// <summary>
    /// 获得/设置 向右侧转移图标
    /// </summary>
    [Parameter]
    public string? RightIcon { get; set; }

    /// <summary>
    /// 获得/设置 左侧按钮显示文本
    /// </summary>
    [Parameter]
    public string? LeftButtonText { get; set; }

    /// <summary>
    /// 获得/设置 右侧按钮显示文本
    /// </summary>
    [Parameter]
    public string? RightButtonText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 左侧面板搜索框 placeholder 文字
    /// </summary>
    [Parameter]
    [Obsolete("已过期，请使用 LeftPanelSearchPlaceHolderString 代替 Please use LeftPanelSearchPlaceHolderString")]
    public string? LeftPannelSearchPlaceHolderString { get { return LeftPanelSearchPlaceHolderString; } set { LeftPanelSearchPlaceHolderString = value; } }

    /// <summary>
    /// 获得/设置 左侧面板搜索框 placeholder 文字
    /// </summary>
    [Parameter]
    public string? LeftPanelSearchPlaceHolderString { get; set; }

    /// <summary>
    /// 获得/设置 右侧面板搜索框 placeholder 文字
    /// </summary>
    [Parameter]
    [Obsolete("已过期，请使用 RightPanelSearchPlaceHolderString 代替 Please use RightPanelSearchPlaceHolderString")]
    public string? RightPannelSearchPlaceHolderString { get { return RightPanelSearchPlaceHolderString; } set { RightPanelSearchPlaceHolderString = value; } }

    /// <summary>
    /// 获得/设置 右侧面板搜索框 placeholder 文字
    /// </summary>
    [Parameter]
    public string? RightPanelSearchPlaceHolderString { get; set; }

    /// <summary>
    /// 获得/设置 右侧面板包含的最大数量, 默认为 0 不限制
    /// </summary>
    [Parameter]
    public int Max { get; set; }

    /// <summary>
    /// 获得/设置 设置最大值时错误消息文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MaxErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 右侧面板包含的最大数量，默认为 0 不限制
    /// </summary>
    [Parameter]
    public int Min { get; set; }

    /// <summary>
    /// 获得/设置 设置最小值时错误消息文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MinErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 数据样式回调方法 默认为 null
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<SelectedItem, string?>? OnSetItemClass { get; set; }

    /// <summary>
    /// 获得/设置 左侧 Panel Header 模板
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? LeftHeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 左侧 Panel Item 模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? LeftItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 右侧 Panel Header 模板
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? RightHeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 右侧 Panel Item 模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? RightItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 IStringLocalizerFactory 注入服务实例 默认为 null
    /// </summary>
    [Inject]
    [NotNull]
    public IStringLocalizerFactory? LocalizerFactory { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (OnSetItemClass == null)
        {
            OnSetItemClass = _ => null;
        }

        // 处理 Required 标签
        if (EditContext != null && FieldIdentifier != null)
        {
            var pi = FieldIdentifier.Value.Model.GetType().GetPropertyByName(FieldIdentifier.Value.FieldName);
            if (pi != null)
            {
                var required = pi.GetCustomAttribute<RequiredAttribute>(true);
                if (required != null)
                {
                    Rules.Add(new RequiredValidator() { LocalizerFactory = LocalizerFactory, ErrorMessage = required.ErrorMessage, AllowEmptyString = required.AllowEmptyStrings });
                }
            }
        }
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        LeftPanelText ??= Localizer[nameof(LeftPanelText)];
        RightPanelText ??= Localizer[nameof(RightPanelText)];
        MinErrorMessage ??= Localizer[nameof(MinErrorMessage)];
        MaxErrorMessage ??= Localizer[nameof(MaxErrorMessage)];

        LeftIcon ??= IconTheme.GetIconByKey(ComponentIcons.TransferLeftIcon);
        RightIcon ??= IconTheme.GetIconByKey(ComponentIcons.TransferRightIcon);

        var list = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
        LeftItems.Clear();
        RightItems.Clear();

        Items ??= Enumerable.Empty<SelectedItem>();

        // 左侧移除
        LeftItems.AddRange(Items);
        LeftItems.RemoveAll(i => list.Any(l => l == i.Value));

        // 右侧插入
        foreach (var t in list)
        {
            var item = Items.FirstOrDefault(i => i.Value == t);
            if (item != null)
            {
                RightItems.Add(item);
            }
        }

        ResetRules();
    }

    private int _min;
    private int _max;
    private void ResetRules()
    {
        if (Max != _max)
        {
            _max = Max;
            Rules.RemoveAll(v => v is MaxValidator);

            if (Max > 0)
            {
                Rules.Add(new MaxValidator() { Value = Max, ErrorMessage = MaxErrorMessage });
            }
        }

        if (Min != _min)
        {
            _min = Min;
            Rules.RemoveAll(v => v is MinValidator);

            if (Min > 0)
            {
                Rules.Add(new MinValidator() { Value = Min, ErrorMessage = MinErrorMessage });
            }
        }
    }

    /// <summary>
    /// 选中数据移动方法
    /// </summary>
    private async Task TransferItems(List<SelectedItem> source, List<SelectedItem> target, bool isLeft)
    {
        if (Items != null)
        {
            var items = source.Where(i => i.Active).ToList();
            items.ForEach(i => i.Active = false);

            source.RemoveAll(i => items.Contains(i));
            target.AddRange(items);

            if (isLeft)
            {
                CurrentValueAsString = string.Join(",", target.Select(i => i.Value));
            }
            else
            {
                CurrentValueAsString = string.Join(",", source.Select(i => i.Value));
            }
            if (OnSelectedItemsChanged != null)
            {
                await OnSelectedItemsChanged(isLeft ? target : source);
            }

            if (ValidateForm == null && (Min > 0 || Max > 0))
            {
                var validationContext = new ValidationContext(Value);
                if (FieldIdentifier.HasValue)
                {
                    validationContext.MemberName = FieldIdentifier.Value.FieldName;
                }
                var validationResults = new List<ValidationResult>();

                await ValidatePropertyAsync(RightItems, validationContext, validationResults);
                ToggleMessage(validationResults, true);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, out TValue result, out string? validationErrorMessage)
    {
        validationErrorMessage = null;
        if (typeof(TValue) == typeof(string))
        {
            result = (TValue)(object)value;
        }
        else if (typeof(IEnumerable<string>).IsAssignableFrom(typeof(TValue)))
        {
            var v = value.Split(",", StringSplitOptions.RemoveEmptyEntries);
            result = (TValue)(object)new List<string>(v);
        }
        else if (typeof(IEnumerable<SelectedItem>).IsAssignableFrom(typeof(TValue)))
        {
            result = (TValue)(object)RightItems.Select(i => new SelectedItem(i.Value, i.Text)).ToList();
        }
        else
        {
            result = default!;
        }
        return true;
    }

    /// <summary>
    /// FormatValueAsString 方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(TValue value) => value == null
        ? null
        : Utility.ConvertValueToString(value);

    /// <summary>
    /// 选项状态改变时回调此方法
    /// </summary>
    private Task SelectedItemsChanged()
    {
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得按钮是否可用
    /// </summary>
    /// <returns></returns>
    private static bool GetButtonState(IEnumerable<SelectedItem> source) => !(source.Any(i => i.Active));
}
