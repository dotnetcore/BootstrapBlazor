// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// MessageItem 组件
/// </summary>
public partial class MessageItem
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    protected override string? ClassName => CssBuilder.Default("alert")
        .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("is-bar", ShowBar)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? AutoHideString => IsAutoHide ? "true" : null;

    /// <summary>
    /// 获得/设置 Toast Body 子组件
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 是否自动隐藏
    /// </summary>
    [Parameter]
    public bool IsAutoHide { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动隐藏时间间隔
    /// </summary>
    [Parameter]
    public int Delay { get; set; } = 4000;

    /// <summary>
    /// 获得/设置 Message 实例
    /// </summary>
    /// <value></value>
    [CascadingParameter]
    public Func<string, Task>? PushMessageIdAsync { get; set; }

    /// <summary>
    /// 获得 IComponentIdGenerator 实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IComponentIdGenerator? ComponentIdGenerator { get; set; }

    [NotNull]
    private string? Id { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Id = ComponentIdGenerator.Generate(this);
    }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && PushMessageIdAsync != null)
        {
            await PushMessageIdAsync(Id);
        }
    }

    private async Task OnClick()
    {
        if (OnDismiss != null) await OnDismiss();
    }
}
