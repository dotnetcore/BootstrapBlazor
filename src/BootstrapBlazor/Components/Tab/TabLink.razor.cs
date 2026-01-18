// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表示标签页组件内的链接</para>
/// <para lang="en">Represents a link within a tab component</para>
/// </summary>
public sealed partial class TabLink
{
    /// <summary>
    /// <para lang="zh">获得/设置 链接的文本，默认为 null</para>
    /// <para lang="en">Gets or sets the text of the link. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 链接的 URL，默认为 null</para>
    /// <para lang="en">Gets or sets the URL of the link. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 链接的图标，默认为 null</para>
    /// <para lang="en">Gets or sets the icon of the link. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标签项是否可关闭的值，默认为 true</para>
    /// <para lang="en">Gets or sets a value indicating whether the tab item is closable. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Closable { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 链接被单击时的回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when the link is clicked. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    [Inject]
    [NotNull]
    private TabItemTextOptions? TabItemOptions { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件的内容，默认为 null</para>
    /// <para lang="en">Gets or sets the content of the component. Default is null.</para>
    /// <para><version>10.2.2</version></para>
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
