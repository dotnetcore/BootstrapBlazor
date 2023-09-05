// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Step 组件类
/// </summary>
public partial class Steps
{
    /// <summary>
    /// 获得 组件样式字符串
    /// </summary>
    private string? ClassString => CssBuilder.Default("steps")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private static string? GetContentClassString(StepItem item) => CssBuilder.Default("step-body")
        .AddClass("d-none", !item.IsActive)
        .Build();

    private StepItem? CurrentStep { get; set; }

    /// <summary>
    /// 获得/设置 步骤集合
    /// </summary>
    [Parameter]
    [NotNull]
    public List<StepItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否垂直渲染 默认 false 水平渲染
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 是否居中对齐
    /// </summary>
    [Parameter]
    public bool IsCenter { get; set; }

    /// <summary>
    /// 获得/设置 设置当前激活步骤
    /// </summary>
    [Parameter]
    public StepStatus Status { get; set; }

    /// <summary>
    /// 获得/设置 组件内容实例
    /// </summary>
    private RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 步骤组件状态改变时回调委托
    /// </summary>
    [Parameter]
    public Func<StepStatus, Task>? OnStatusChanged { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ChildContent = new RenderFragment(builder =>
        {
            var index = 0;
            builder.OpenElement(index++, "div");
            builder.AddAttribute(index++, "class", CssBuilder.Default("steps-header")
                .AddClass("steps-horizontal", !IsVertical)
                .AddClass("steps-vertical", IsVertical)
                .Build());
            foreach (var item in Items)
            {
                builder.AddContent(index++, RenderStep(item));
            }
            builder.CloseElement();

            foreach (var item in Items)
            {
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", GetContentClassString(item));
                builder.AddContent(index++, item.Template);
                builder.CloseElement();
            }
        });
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= new List<StepItem>();
    }

    /// <summary>
    /// OnParametersSetAsync 方法
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        CurrentStep = null;
        if (Items.Any())
        {
            CurrentStep = Items.FirstOrDefault(i => i.IsActive);
            var status = CurrentStep?.Status ?? StepStatus.Wait;
            if (Status != status)
            {
                Status = status;
                if (OnStatusChanged != null)
                {
                    await OnStatusChanged.Invoke(Status);
                }
            }
        }
    }

    /// <summary>
    /// 渲染 Step 组件方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected virtual RenderFragment RenderStep(StepItem item) => new(builder =>
    {
        item.Space = ParseSpace(item.Space);
        var index = 0;
        builder.OpenComponent<Step>(index++);
        builder.SetKey(item);
        builder.AddAttribute(index++, nameof(Step.Title), item.Title);
        builder.AddAttribute(index++, nameof(Step.Icon), item.Icon);
        builder.AddAttribute(index++, nameof(Step.Description), item.Description);
        builder.AddAttribute(index++, nameof(Step.Space), item.Space);
        builder.AddAttribute(index++, nameof(Step.Status), item.Status);
        builder.AddAttribute(index++, nameof(Step.IsLast), item == Items.Last());
        builder.AddAttribute(index++, nameof(Step.IsCenter), IsCenter);
        builder.AddAttribute(index, nameof(Step.StepIndex), Items.ToList().IndexOf(item));
        builder.CloseComponent();
    });

    private string ParseSpace(string? space)
    {
        if (!string.IsNullOrEmpty(space) && !double.TryParse(space.TrimEnd('%'), out _)) space = null;
        if (string.IsNullOrEmpty(space)) space = $"{Math.Round(100 * 1.0d / Math.Max(1, Items.Count - 1), 2)}%";
        return space;
    }

    /// <summary>
    /// 上一步
    /// </summary>
    /// <param name="status">当前要设定的状态</param>
    public void Prev(StepStatus status = StepStatus.Wait)
    {
        var item = Items.FirstOrDefault(x => x.IsActive);
        if (item != null)
        {
            var index = Items.IndexOf(item);
            if (index <= 0)
            {
                return;
            }

            index--;
            item.IsActive = false;
            item.Status = status;
            item = Items.ElementAt(index);
            item.Status = StepStatus.Process;
            item.IsActive = true;
            StateHasChanged();
            OnStatusChanged?.Invoke(status);
        }
        else
        {
            item = Items.LastOrDefault();
            if (item == null)
            {
                return;
            }
            item.IsActive = true;
            item.Status = StepStatus.Process;
            StateHasChanged();
        }
    }

    /// <summary>
    /// 下一步
    /// </summary>
    /// <param name="status">当前要设定的状态</param>
    public void Next(StepStatus status = StepStatus.Success)
    {
        var item = Items.FirstOrDefault(x => x.IsActive);
        if (item != null)
        {
            var index = Items.IndexOf(item);
            if (index >= Items.Count)
            {
                return;
            }
            item.IsActive = false;
            item.Status = status;
            index++;
            if (index < Items.Count)
            {
                item = Items.ElementAt(index);
                item.Status = StepStatus.Process;
                item.IsActive = true;
            }
            StateHasChanged();
            OnStatusChanged?.Invoke(status);
        }
    }
}
