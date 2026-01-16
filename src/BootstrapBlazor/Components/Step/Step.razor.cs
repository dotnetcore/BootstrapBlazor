// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Step 组件类</para>
/// <para lang="en">Step Component Class</para>
/// </summary>
public partial class Step
{
    /// <summary>
    /// <para lang="zh">获得/设置 步骤集合</para>
    /// <para lang="en">Get/Set Items</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public List<StepOption>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否垂直渲染 默认 false 水平渲染</para>
    /// <para lang="en">Get/Set Is Vertical. Default false (Horizontal)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前步骤索引 默认 0</para>
    /// <para lang="en">Get/Set Current Step Index. Default 0</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int StepIndex { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件内容实例</para>
    /// <para lang="en">Get/Set Child Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 步骤全部完成时模板 默认 null</para>
    /// <para lang="en">Get/Set Finished Template. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? FinishedTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 步骤全部完成时回调方法</para>
    /// <para lang="en">Get/Set Callback method when all steps are finished</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnFinishedCallback { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private int _currentStepIndex;

    /// <summary>
    /// <para lang="zh">获得当前步骤索引（只读）</para>
    /// <para lang="en">Get Current Step Index (Read-only)</para>
    /// </summary>
    public int CurrentStepIndex => _currentStepIndex;

    /// <summary>
    /// <para lang="zh">获得 组件样式字符串</para>
    /// <para lang="en">Get Component Class String</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("step")
        .AddClass("step-vertical", IsVertical)
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

    private bool IsFinished => _currentStepIndex == Items.Count;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _currentStepIndex = StepIndex;
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _finishedIcon ??= IconTheme.GetIconByKey(ComponentIcons.StepIcon);
        Items ??= [];
    }

    /// <summary>
    /// <para lang="zh">移动到上一步方法 返回当前 StepIndex 值</para>
    /// <para lang="en">Move to previous step method. Return current StepIndex</para>
    /// </summary>
    public int Prev()
    {
        _currentStepIndex = Math.Max(0, _currentStepIndex - 1);
        StateHasChanged();
        return _currentStepIndex;
    }

    /// <summary>
    /// <para lang="zh">移动到下一步方法 返回当前 StepIndex 值</para>
    /// <para lang="en">Move to next step method. Return current StepIndex</para>
    /// </summary>
    public async Task<int> Next()
    {
        _currentStepIndex = Math.Min(Items.Count, _currentStepIndex + 1);
        if (IsFinished && OnFinishedCallback != null)
        {
            await OnFinishedCallback();
        }
        StateHasChanged();
        return _currentStepIndex;
    }

    /// <summary>
    /// <para lang="zh">重置步骤方法</para>
    /// <para lang="en">Reset Step Method</para>
    /// </summary>
    public void Reset()
    {
        _currentStepIndex = 0;
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">添加步骤到组件中</para>
    /// <para lang="en">Add step to component</para>
    /// </summary>
    /// <param name="option"></param>
    public void Add(StepOption option)
    {
        Items.Add(option);
    }

    /// <summary>
    /// <para lang="zh">插入步骤到组件中</para>
    /// <para lang="en">Insert step to component</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="option"></param>
    public void Insert(int index, StepOption option)
    {
        Items.Insert(index, option);
    }

    /// <summary>
    /// <para lang="zh">从组件中移除步骤</para>
    /// <para lang="en">Remove step from component</para>
    /// </summary>
    /// <param name="option"></param>
    public void Remove(StepOption option)
    {
        Items.Remove(option);
    }
}
