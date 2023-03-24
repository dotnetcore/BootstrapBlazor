// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class TabLink
{
    /// <summary>
    /// 获得/设置 文本文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 请求地址
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 图标字符串
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 当前 TabItem 是否可关闭 默认为 true 可关闭
    /// </summary>
    [Parameter]
    public bool Closable { get; set; } = true;

    /// <summary>
    /// 点击组件时回调此委托方法 默认为空
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    [Inject]
    [NotNull]
    private TabItemTextOptions? TabItemOptions { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private async Task OnClickLink()
    {
        if (OnClick != null)
        {
            await OnClick();
        }

        TabItemOptions.Icon = Icon;
        TabItemOptions.Text = Text;
        TabItemOptions.Closable = Closable;
    }

    private RenderFragment RenderChildContent() => builder =>
    {
        if (ChildContent == null)
        {
            if (!string.IsNullOrEmpty(Icon))
            {
                builder.OpenElement(0, "i");
                builder.AddAttribute(1, "class", Icon);
                builder.CloseElement();
            }
            if (!string.IsNullOrEmpty(Text))
            {
                builder.AddContent(2, Text);
            }
        }
        else
        {
            builder.AddContent(3, ChildContent);
        }
    };
}
