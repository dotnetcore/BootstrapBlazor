// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TabItem component
/// </summary>
public class TabItem : ComponentBase
{
    /// <summary>
    /// Gets or sets the text. Default is null
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the TabItem Header template. Default is null
    /// </summary>
    [Parameter]
    public RenderFragment<TabItem>? HeaderTemplate { get; set; }

    /// <summary>
    /// Gets or sets the request URL. Default is null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets whether the current state is active. Default is false
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets whether the current state is disabled, default is false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets whether the current TabItem is closable, default is true
    /// </summary>
    [Parameter]
    public bool Closable { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the current TabItem is always loaded, this parameter is used to set <see cref="Tab.IsLazyLoadTabItem"/>, default is false
    /// </summary>
    [Parameter]
    public bool AlwaysLoad { get; set; }

    /// <summary>
    /// Gets or sets the custom CSS class. Default is null
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets the icon string. Default is null
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets whether to show the full screen button, default is true
    /// </summary>
    [Parameter]
    public bool ShowFullScreen { get; set; } = true;

    /// <summary>
    /// Gets or sets the component content. Default is null
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the associated Tab instance
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
    /// Method to set whether it is active
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active) => IsActive = active;

    /// <summary>
    /// Method to set whether it is disabled
    /// </summary>
    /// <param name="disabled"></param>
    public void SetDisabled(bool disabled)
    {
        TabSet?.SetDisabledItem(this, disabled);
    }

    /// <summary>
    /// Method to set whether it is disabled without rendering
    /// </summary>
    /// <param name="disabled"></param>
    internal void SetDisabledWithoutRender(bool disabled) => IsDisabled = disabled;

    /// <summary>
    /// Method to reset the tab text and other parameters
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
    /// Gets a TabItem instance by specifying a set of parameters
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
