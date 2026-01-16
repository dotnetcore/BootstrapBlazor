// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Reflection.Metadata;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">控制台消息组件</para>
/// <para lang="en">Console message component</para>
/// </summary>
public partial class Console
{
    /// <summary>
    /// <para lang="zh">获得 组件样式</para>
    /// <para lang="en">Get component style</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("card console")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 Console Body Style 字符串</para>
    /// <para lang="en">Get Console Body Style string</para>
    /// </summary>
    private string? BodyStyleString => CssBuilder.Default()
        .AddClass($"height: {Height}px;", Height > 0)
        .Build();

    /// <summary>
    /// <para lang="zh">获取消息样式</para>
    /// <para lang="en">Get message style</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetClassString(ConsoleMessageItem item) => CssBuilder.Default()
        .AddClass($"text-{item.Color.ToDescriptionString()}", item.Color != Color.None)
        .AddClass(item.CssClass, !string.IsNullOrEmpty(item.CssClass))
        .Build();

    /// <summary>
    /// <para lang="zh">获得 客户端是否自动滚屏标识</para>
    /// <para lang="en">Get client auto scroll flag</para>
    /// </summary>
    private string? AutoScrollString => IsAutoScroll ? "auto" : null;

    /// <summary>
    /// <para lang="zh">获得/设置 组件绑定数据源</para>
    /// <para lang="en">Get/Set component data source</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh"><see cref="ConsoleMessageCollection"/> 集合内置了最大消息数量功能</para>
    /// <para lang="en"><see cref="ConsoleMessageCollection"/> collection has built-in maximum message count function</para>
    /// </remarks>
    [Parameter]
    [NotNull]
    public IEnumerable<ConsoleMessageItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 显示文字 默认值为 系统监控</para>
    /// <para lang="en">Get/Set Header display text, default is System Monitor</para>
    /// </summary>
    [Parameter]
    public string? HeaderText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 指示灯 Title 显示文字</para>
    /// <para lang="en">Get/Set indicator Title display text</para>
    /// </summary>
    [Parameter]
    public string? LightTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 指示灯 是否闪烁 默认 true 闪烁</para>
    /// <para lang="en">Get/Set whether indicator flashes, default is true(flashing)</para>
    /// </summary>
    [Parameter]
    public bool IsFlashLight { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 指示灯颜色</para>
    /// <para lang="en">Get/Set indicator color</para>
    /// </summary>
    [Parameter]
    public Color LightColor { get; set; } = Color.Success;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示指示灯 默认 true 显示</para>
    /// <para lang="en">Get/Set whether to show indicator, default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowLight { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 自动滚屏显示文字</para>
    /// <para lang="en">Get/Set auto scroll display text</para>
    /// </summary>
    [Parameter]
    public string? AutoScrollText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示自动滚屏选项 默认 false</para>
    /// <para lang="en">Get/Set whether to show auto scroll option, default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowAutoScroll { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动滚屏 默认 true</para>
    /// <para lang="en">Get/Set whether to auto scroll, default is true</para>
    /// </summary>
    [Parameter]
    public bool IsAutoScroll { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 按钮 显示文字 默认值为 清屏</para>
    /// <para lang="en">Get/Set button display text, default is Clear</para>
    /// </summary>
    [Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮 显示图标 默认值为 fa-solid fa-xmark</para>
    /// <para lang="en">Get/Set button display icon, default is fa-solid fa-xmark</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清除按钮颜色 默认值为 Color.Secondary</para>
    /// <para lang="en">Get/Set clear button color, default is Color.Secondary</para>
    /// </summary>
    [Parameter]
    public Color ClearButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// <para lang="zh">获得/设置 清空委托方法</para>
    /// <para lang="en">Get/Set clear delegate method</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClear { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件高度 默认为 126px;</para>
    /// <para lang="en">Get/Set component height, default is 126px</para>
    /// </summary>
    [Parameter]
    public int Height { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 模板</para>
    /// <para lang="en">Get/Set Footer template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板</para>
    /// <para lang="en">Get/Set Header template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Item 模板</para>
    /// <para lang="en">Get/Set Item template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<ConsoleMessageItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得 是否显示 Footer</para>
    /// <para lang="en">Get whether to show Footer</para>
    /// </summary>
    private bool ShowFooter => OnClear != null || ShowAutoScroll || FooterTemplate != null;

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
        Items ??= [];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// <para lang="zh">清空控制台消息方法</para>
    /// <para lang="en">Clear console messages method</para>
    /// </summary>
    public async Task OnClearConsole()
    {
        if (OnClear != null)
        {
            await OnClear();
        }
    }
}
