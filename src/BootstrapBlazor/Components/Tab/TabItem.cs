// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">TabItem component</para>
///  <para lang="en">TabItem component</para>
/// </summary>
public class TabItem : IdComponentBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 the text. 默认为 null</para>
    ///  <para lang="en">Gets or sets the text. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the TabItem Header 模板. 默认为 null</para>
    ///  <para lang="en">Gets or sets the TabItem Header template. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TabItem>? HeaderTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the request URL. 默认为 null</para>
    ///  <para lang="en">Gets or sets the request URL. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Url { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否 the current state is active. 默认为 false</para>
    ///  <para lang="en">Gets or sets whether the current state is active. Default is false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否 the current state is disabled, default is false</para>
    ///  <para lang="en">Gets or sets whether the current state is disabled, default is false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否 the current TabItem is closable, default is true</para>
    ///  <para lang="en">Gets or sets whether the current TabItem is closable, default is true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Closable { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否 the current TabItem is always loaded, this parameter is used to set <see cref="Tab.IsLazyLoadTabItem"/>, default is false</para>
    ///  <para lang="en">Gets or sets whether the current TabItem is always loaded, this parameter is used to set <see cref="Tab.IsLazyLoadTabItem"/>, default is false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AlwaysLoad { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the custom CSS class. 默认为 null</para>
    ///  <para lang="en">Gets or sets the custom CSS class. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the 图标 string. 默认为 null</para>
    ///  <para lang="en">Gets or sets the icon string. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否 to show the full screen 按钮, default is true</para>
    ///  <para lang="en">Gets or sets whether to show the full screen button, default is true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowFullScreen { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 the component 内容. 默认为 null</para>
    ///  <para lang="en">Gets or sets the component content. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the associated Tab 实例</para>
    ///  <para lang="en">Gets or sets the associated Tab instance</para>
    /// </summary>
    [CascadingParameter]
    protected internal Tab? TabSet { get; set; }

    private string? LastText { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Url ??= "";
        TabSet?.AddItem(this);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
    ///  <para lang="zh">Method to set 是否 it is active</para>
    ///  <para lang="en">Method to set whether it is active</para>
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active) => IsActive = active;

    /// <summary>
    ///  <para lang="zh">Method to set 是否 it is disabled</para>
    ///  <para lang="en">Method to set whether it is disabled</para>
    /// </summary>
    /// <param name="disabled"></param>
    public void SetDisabled(bool disabled)
    {
        TabSet?.SetDisabledItem(this, disabled);
    }

    /// <summary>
    ///  <para lang="zh">Method to set 是否 it is disabled without rendering</para>
    ///  <para lang="en">Method to set whether it is disabled without rendering</para>
    /// </summary>
    /// <param name="disabled"></param>
    internal void SetDisabledWithoutRender(bool disabled) => IsDisabled = disabled;

    /// <summary>
    ///  <para lang="zh">Method to reset the tab text and other parameters</para>
    ///  <para lang="en">Method to reset the tab text and other parameters</para>
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
    ///  <para lang="zh">获得 a TabItem 实例 by specifying a set of parameters</para>
    ///  <para lang="en">Gets a TabItem instance by specifying a set of parameters</para>
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
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
