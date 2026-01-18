// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TabItem 组件</para>
/// <para lang="en">TabItem Component</para>
/// </summary>
public class TabItem : IdComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 标签项文本，默认为 null</para>
    /// <para lang="en">Gets or sets the text. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 TabItem 头部模板，默认为 null</para>
    /// <para lang="en">Gets or sets the TabItem Header template. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TabItem>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 请求 URL，默认为 null</para>
    /// <para lang="en">Gets or sets the request URL. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前状态是否为活跃，默认为 false</para>
    /// <para lang="en">Gets or sets whether the current state is active. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前状态是否被禁用，默认为 false</para>
    /// <para lang="en">Gets or sets whether the current state is disabled. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前 TabItem 是否可关闭，默认为 true</para>
    /// <para lang="en">Gets or sets whether the current TabItem is closable. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Closable { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 当前 TabItem 是否始终被加载，此参数用于设置 Tab.IsLazyLoadTabItem，默认为 false</para>
    /// <para lang="en">Gets or sets whether the current TabItem is always loaded. This parameter is used to set Tab.IsLazyLoadTabItem. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AlwaysLoad { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义 CSS 类，默认为 null</para>
    /// <para lang="en">Gets or sets the custom CSS class. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标字符串，默认为 null</para>
    /// <para lang="en">Gets or sets the icon string. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示全屏按钮，默认为 true</para>
    /// <para lang="en">Gets or sets whether to show the full screen button. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowFullScreen { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 组件内容，默认为 null</para>
    /// <para lang="en">Gets or sets the component content. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关联的 Tab 实例</para>
    /// <para lang="en">Gets or sets the associated Tab instance</para>
    /// </summary>
    [CascadingParameter]
    protected internal Tab? TabSet { get; set; }

    private string? LastText { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Url ??= "";
        TabSet?.AddItem(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (LastText != null)
        {
            Text = LastText;
        }
    }

    /// <summary>
    /// <para lang="zh">设置是否为活跃的方法</para>
    /// <para lang="en">Sets whether it is active</para>
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active) => IsActive = active;

    /// <summary>
    /// <para lang="zh">设置是否被禁用的方法</para>
    /// <para lang="en">Sets whether it is disabled</para>
    /// </summary>
    /// <param name="disabled"></param>
    public void SetDisabled(bool disabled)
    {
        TabSet?.SetDisabledItem(this, disabled);
    }

    /// <summary>
    /// <para lang="zh">设置是否被禁用而不重新渲染的方法</para>
    /// <para lang="en">Method to set whether it is disabled without rendering</para>
    /// </summary>
    /// <param name="disabled"></param>
    internal void SetDisabledWithoutRender(bool disabled) => IsDisabled = disabled;

    /// <summary>
    /// <para lang="zh">方法重置标签文本和其他参数</para>
    /// <para lang="en">Method to reset the tab text and other parameters</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="icon"></param>
    /// <param name="closable"></param>
    public void SetHeader(string text, string? icon = null, bool? closable = null)
    {
        if (TabSet != null)
        {
            LastText = Text = text;

            if (!string.IsNullOrEmpty(icon))
            {
                Icon = icon;
            }
            if (closable.HasValue)
            {
                Closable = closable.Value;
            }
            TabSet.ActiveTab(this);
        }
    }

    /// <summary>
    /// <para lang="zh">通过指定一组参数获得 TabItem 实例</para>
    /// <para lang="en">Gets a TabItem instance by specifying a set of parameters</para>
    /// </summary>
    /// <param name="parameters"></param>
    public static TabItem Create(Dictionary<string, object?> parameters)
    {
        var item = new TabItem();
        if (parameters.TryGetValue(nameof(Url), out var url))
        {
            parameters[nameof(Url)] = url?.ToString()?.TrimStart('/') ?? "";
        }
        var pv = ParameterView.FromDictionary(parameters);
        pv.SetParameterProperties(item);
        return item;
    }
}
