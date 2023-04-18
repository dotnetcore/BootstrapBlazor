// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Reflection.Metadata;

namespace BootstrapBlazor.Components;

/// <summary>
/// 控制台消息组件
/// </summary>
[JSModuleAutoLoader]
public partial class Console
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("card console")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 Console Body Style 字符串
    /// </summary>
    private string? BodyStyleString => CssBuilder.Default()
        .AddClass($"height: {Height}px;", Height > 0)
        .Build();

    /// <summary>
    /// 获取消息样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetClassString(ConsoleMessageItem item) => CssBuilder.Default()
        .AddClass($"text-{item.Color.ToDescriptionString()}", item.Color != Color.None)
        .Build();

    /// <summary>
    /// 获得 客户端是否自动滚屏标识
    /// </summary>
    private string? AutoScrollString => IsAutoScroll ? "auto" : null;

    /// <summary>
    /// 获得/设置 组件绑定数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<ConsoleMessageItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 Header 显示文字 默认值为 系统监控
    /// </summary>
    [Parameter]
    public string? HeaderText { get; set; }

    /// <summary>
    /// 获得/设置 指示灯 Title 显示文字
    /// </summary>
    [Parameter]
    public string? LightTitle { get; set; }

    /// <summary>
    /// 获得/设置 指示灯 是否闪烁 默认 true 闪烁
    /// </summary>
    [Parameter]
    public bool IsFlashLight { get; set; } = true;

    /// <summary>
    /// 获得/设置 指示灯颜色
    /// </summary>
    [Parameter]
    public Color LightColor { get; set; } = Color.Success;

    /// <summary>
    /// 获得/设置 是否显示指示灯 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowLight { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动滚屏显示文字
    /// </summary>
    [Parameter]
    public string? AutoScrollText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示自动滚屏选项 默认 false
    /// </summary>
    [Parameter]
    public bool ShowAutoScroll { get; set; }

    /// <summary>
    /// 获得/设置 是否自动滚屏 默认 true
    /// </summary>
    [Parameter]
    public bool IsAutoScroll { get; set; } = true;

    /// <summary>
    /// 获得/设置 按钮 显示文字 默认值为 清屏
    /// </summary>
    [Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 获得/设置 按钮 显示图标 默认值为 fa-solid fa-xmark
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 清除按钮颜色 默认值为 Color.Secondary
    /// </summary>
    [Parameter]
    public Color ClearButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 清空委托方法
    /// </summary>
    [Parameter]
    public Action? OnClear { get; set; }

    /// <summary>
    /// 获得/设置 组件高度 默认为 126px;
    /// </summary>
    [Parameter]
    public int Height { get; set; }

    /// <summary>
    /// 获得/设置 Footer 模板
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Item 模板
    /// </summary>
    [Parameter]
    public RenderFragment<ConsoleMessageItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得 是否显示 Footer
    /// </summary>
    protected bool ShowFooter => OnClear != null || ShowAutoScroll || FooterTemplate != null;

    [Inject]
    [NotNull]
    private IStringLocalizer<Console>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        HeaderText ??= Localizer[nameof(HeaderText)];
        LightTitle ??= Localizer[nameof(LightTitle)];
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        AutoScrollText ??= Localizer[nameof(AutoScrollText)];

        ClearButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.ConsoleClearButtonIcon);
        Items ??= Enumerable.Empty<ConsoleMessageItem>();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if(!firstRender)
        {
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// 清空控制台消息方法
    /// </summary>
    public void ClearConsole()
    {
        OnClear?.Invoke();
    }
}
