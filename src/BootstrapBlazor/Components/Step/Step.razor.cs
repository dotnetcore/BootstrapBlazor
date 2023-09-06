// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Step 组件类
/// </summary>
public partial class Step
{
    /// <summary>
    /// 获得/设置 步骤集合
    /// </summary>
    [Parameter]
    [NotNull]
    public List<StepOption>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否垂直渲染 默认 false 水平渲染
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 当前步骤索引 默认 0
    /// </summary>
    [Parameter]
    public int StepIndex { get; set; }

    /// <summary>
    /// 获得/设置 组件内容实例
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 步骤组件状态改变时回调委托
    /// </summary>
    [Parameter]
    public Action<StepStatus>? OnStatusChanged { get; set; }

    /// <summary>
    /// 获得/设置 每个 step 的模板
    /// </summary>
    [Parameter]
    public RenderFragment? DescriptionTemplate { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private int _currentStepIndex;

    /// <summary>
    /// 获得 组件样式字符串
    /// </summary>
    private string? ClassString => CssBuilder.Default("step")
        .AddClass("steps-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string GetStepString(StepOption option) => $"{Items.IndexOf(option) + 1}";

    private string? GetHeaderClassString(StepOption option)
    {
        var index = Items.IndexOf(option);
        return CssBuilder.Default("step-item")
            .AddClass("is-done", index < _currentStepIndex)
            .AddClass("active", index == _currentStepIndex)
            .Build();
    }

    private string? GetBodyClassString(StepOption option)
    {
        var index = Items.IndexOf(option);
        return CssBuilder.Default("step-body-item")
            .AddClass("active", index == _currentStepIndex)
            .Build();
    }

    private bool ShowFinishedIcon(StepOption option) => !string.IsNullOrEmpty(option.FinishedIcon) && Items.IndexOf(option) < _currentStepIndex;

    private static string? GetIconClassString(StepOption option) => CssBuilder.Default("step-icon")
        .AddClass(option.Icon)
        .Build();

    private static string? GetFinishedIconClassString(StepOption option) => CssBuilder.Default("step-icon")
        .AddClass(option.FinishedIcon)
        .Build();

    private string? _finishedIcon;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _currentStepIndex = StepIndex;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _finishedIcon ??= IconTheme.GetIconByKey(ComponentIcons.StepIcon);
        Items ??= new();
    }

    /// <summary>
    /// 上一步
    /// </summary>
    public void Prev()
    {
        _currentStepIndex--;
        StateHasChanged();
    }

    /// <summary>
    /// 下一步
    /// </summary>
    public void Next()
    {
        _currentStepIndex++;
        StateHasChanged();
    }

    /// <summary>
    /// 下一步
    /// </summary>
    public void Reset()
    {
        _currentStepIndex = 0;
        StateHasChanged();
    }

    /// <summary>
    /// 添加步骤到组件中
    /// </summary>
    /// <param name="option"></param>
    public void Add(StepOption option)
    {
        Items.Add(option);
    }

    /// <summary>
    /// 插入步骤到组件中
    /// </summary>
    /// <param name="index"></param>
    /// <param name="option"></param>
    public void Insert(int index, StepOption option)
    {
        Items.Insert(index, option);
    }

    /// <summary>
    /// 从组件中移除步骤
    /// </summary>
    /// <param name="option"></param>
    public void Remove(StepOption option)
    {
        Items.Remove(option);
    }
}
