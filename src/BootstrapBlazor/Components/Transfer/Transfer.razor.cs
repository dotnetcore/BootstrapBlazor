// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">穿梭框组件
///</para>
/// <para lang="en">穿梭框component
///</para>
/// </summary>
public partial class Transfer<TValue>
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Transfer<TValue>>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮文本样式
    ///</para>
    /// <para lang="en">Gets or sets button文本style
    ///</para>
    /// </summary>
    private string? LeftButtonClassName => CssBuilder.Default()
        .AddClass("d-none", string.IsNullOrEmpty(LeftButtonText))
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 按钮文本样式
    ///</para>
    /// <para lang="en">Gets or sets button文本style
    ///</para>
    /// </summary>
    private string? RightButtonClassName => CssBuilder.Default("me-1")
        .AddClass("d-none", string.IsNullOrEmpty(RightButtonText))
        .Build();

    private string? ValidateClass => CssBuilder.Default()
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 左侧数据集合
    ///</para>
    /// <para lang="en">Gets or sets 左侧datacollection
    ///</para>
    /// </summary>
    private List<SelectedItem> LeftItems { get; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 右侧数据集合
    ///</para>
    /// <para lang="en">Gets or sets 右侧datacollection
    ///</para>
    /// </summary>
    private List<SelectedItem> RightItems { get; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 组件绑定数据项集合
    ///</para>
    /// <para lang="en">Gets or sets component绑定data项collection
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中项集合发生改变时回调委托方法
    ///</para>
    /// <para lang="en">Gets or sets 选中项collection发生改变时回调delegate方法
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem>, Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 左侧面板 Header 显示文本
    ///</para>
    /// <para lang="en">Gets or sets 左侧面板 Header display文本
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LeftPanelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧面板 Header 显示文本
    ///</para>
    /// <para lang="en">Gets or sets 右侧面板 Header display文本
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RightPanelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 向左侧转移图标
    ///</para>
    /// <para lang="en">Gets or sets 向左侧转移icon
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LeftIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 向右侧转移图标
    ///</para>
    /// <para lang="en">Gets or sets 向右侧转移icon
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RightIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 左侧按钮显示文本
    ///</para>
    /// <para lang="en">Gets or sets 左侧buttondisplay文本
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LeftButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧按钮显示文本
    ///</para>
    /// <para lang="en">Gets or sets 右侧buttondisplay文本
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RightButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索框
    ///</para>
    /// <para lang="en">Gets or sets whetherdisplay搜索框
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 左侧面板搜索框 placeholder 文字
    ///</para>
    /// <para lang="en">Gets or sets 左侧面板搜索框 placeholder 文字
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已过期，请使用 LeftPanelSearchPlaceHolderString 代替 Please use LeftPanelSearchPlaceHolderString")]
    [ExcludeFromCodeCoverage]
    public string? LeftPannelSearchPlaceHolderString { get => LeftPanelSearchPlaceHolderString; set => LeftPanelSearchPlaceHolderString = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 左侧面板搜索框 placeholder 文字
    ///</para>
    /// <para lang="en">Gets or sets 左侧面板搜索框 placeholder 文字
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LeftPanelSearchPlaceHolderString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧面板搜索框 placeholder 文字
    ///</para>
    /// <para lang="en">Gets or sets 右侧面板搜索框 placeholder 文字
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已过期，请使用 RightPanelSearchPlaceHolderString 代替 Please use RightPanelSearchPlaceHolderString")]
    [ExcludeFromCodeCoverage]
    public string? RightPannelSearchPlaceHolderString { get => RightPanelSearchPlaceHolderString; set => RightPanelSearchPlaceHolderString = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧面板搜索框 placeholder 文字
    ///</para>
    /// <para lang="en">Gets or sets 右侧面板搜索框 placeholder 文字
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RightPanelSearchPlaceHolderString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧面板包含的最大数量, 默认为 0 不限制
    ///</para>
    /// <para lang="en">Gets or sets 右侧面板包含的最大数量, Default is为 0 不限制
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Max { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置最大值时错误消息文字
    ///</para>
    /// <para lang="en">Gets or sets Sets最大值时错误消息文字
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MaxErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧面板包含的最大数量，默认为 0 不限制
    ///</para>
    /// <para lang="en">Gets or sets 右侧面板包含的最大数量，Default is为 0 不限制
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Min { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置最小值时错误消息文字
    ///</para>
    /// <para lang="en">Gets or sets Sets最小值时错误消息文字
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MinErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据样式回调方法 默认为 null
    ///</para>
    /// <para lang="en">Gets or sets datastylecallback method Default is为 null
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<SelectedItem, string?>? OnSetItemClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 左侧 Panel Header 模板
    ///</para>
    /// <para lang="en">Gets or sets 左侧 Panel Header template
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? LeftHeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 左侧 Panel Item 模板
    ///</para>
    /// <para lang="en">Gets or sets 左侧 Panel Item template
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? LeftItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧 Panel Header 模板
    ///</para>
    /// <para lang="en">Gets or sets 右侧 Panel Header template
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? RightHeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧 Panel Item 模板
    ///</para>
    /// <para lang="en">Gets or sets 右侧 Panel Item template
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? RightItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件高度 默认值 null 未设置
    ///</para>
    /// <para lang="en">Gets or sets componentheight Default is值 null 未Sets
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 候选项是否为换行模式 默认 false 不换行
    ///</para>
    /// <para lang="en">Gets or sets 候选项whether为换行模式 Default is false 不换行
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsWrapItem { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 候选项宽度 默认 null 未设置
    ///</para>
    /// <para lang="en">Gets or sets 候选项width Default is null 未Sets
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ItemWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 候选项文本是否为换行 默认 false 不换行
    ///</para>
    /// <para lang="en">Gets or sets 候选项文本whether为换行 Default is false 不换行
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsWrapItemText { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ClassString => CssBuilder.Default("bb-transfer")
        .AddClass("has-height", !string.IsNullOrEmpty(Height))
        .AddClass("wrap-item", IsWrapItem)
        .AddClass("wrap-item-text", IsWrapItemText)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-transfer-height: {Height};", !string.IsNullOrEmpty(Height))
        .AddClass($"--bb-transfer-panel-item-width: {ItemWidth};", !string.IsNullOrEmpty(ItemWidth))
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">OnInitialized 方法
    ///</para>
    /// <para lang="en">OnInitialized 方法
    ///</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 处理 Required 标签
        AddRequiredValidator();
    }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法
    ///</para>
    /// <para lang="en">OnParametersSet 方法
    ///</para>
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

        Items ??= [];

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
    /// <para lang="zh">选中数据移动方法
    ///</para>
    /// <para lang="en">选中data移动方法
    ///</para>
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
                await ToggleMessage(validationResults);
            }
        }
    }

    /// <summary>
    /// <para lang="zh">///</para>
    /// <para lang="en">///</para>
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
    /// <para lang="zh">FormatValueAsString 方法
    ///</para>
    /// <para lang="en">FormatValueAsString 方法
    ///</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(TValue? value) => value == null
        ? null
        : Utility.ConvertValueToString(value);

    /// <summary>
    /// <para lang="zh">选项状态改变时回调此方法
    ///</para>
    /// <para lang="en">选项状态改变时回调此方法
    ///</para>
    /// </summary>
    private Task SelectedItemsChanged()
    {
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">获得按钮是否可用
    ///</para>
    /// <para lang="en">Getsbuttonwhether可用
    ///</para>
    /// </summary>
    /// <returns></returns>
    private static bool GetButtonState(IEnumerable<SelectedItem> source) => !(source.Any(i => i.Active));
}
