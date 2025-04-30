// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Search component
/// </summary>
public partial class Search<TValue>
{
    /// <summary>
    /// Gets or sets the icon template. Default is null if not set.
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? IconTemplate { get; set; }

    /// <summary>
    /// Gets or sets whether to show the clear button. Default is false.
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// Gets or sets the clear icon. Default is null.
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// Gets or sets whether to show the clear button. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// Gets or sets the icon of clear button. Default is null.
    /// </summary>
    [Parameter]
    public string? ClearButtonIcon { get; set; }

    /// <summary>
    /// Gets or sets the text of clear button. Default is null.
    /// </summary>
    [Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// Gets or sets the color of clear button. Default is <see cref="Color.Primary"/>.
    /// </summary>
    [Parameter]
    public Color ClearButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// Gets or sets whether to show the search button. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowSearchButton { get; set; } = true;

    /// <summary>
    /// Gets or sets the search button color. Default is <see cref="Color.Primary"/>.
    /// </summary>
    [Parameter]
    public Color SearchButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// Gets or sets the search button icon. Default is null.
    /// </summary>
    [Parameter]
    public string? SearchButtonIcon { get; set; }

    /// <summary>
    /// Gets or sets the loading icon for the search button. Default is null.
    /// </summary>
    [Parameter]
    public string? SearchButtonLoadingIcon { get; set; }

    /// <summary>
    /// Gets or sets the search button text. Default is null.
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchButtonText { get; set; }

    /// <summary>
    /// Gets or sets the button template. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? ButtonTemplate { get; set; }

    /// <summary>
    /// Gets or sets the prefix button template. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? PrefixButtonTemplate { get; set; }

    /// <summary>
    /// Gets or sets whether to show the prefix icon. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowPrefixIcon { get; set; }

    /// <summary>
    /// Gets or sets the prefix icon. Default is null.
    /// </summary>
    [Parameter]
    public string? PrefixIcon { get; set; }

    /// <summary>
    /// Gets or sets the prefix icon template. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? PrefixIconTemplate { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically clear the search box after searching. Deprecated.
    /// </summary>
    [Parameter]
    [Obsolete("Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public bool IsAutoClearAfterSearch { get; set; }

    /// <summary>
    /// Gets or sets whether the search is triggered by input. Default is true. If false, the search button must be clicked to trigger.
    /// </summary>
    [Parameter]
    public bool IsTriggerSearchByInput { get; set; } = true;

    /// <summary>
    /// Gets or sets the callback delegate when the search button is clicked.
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnSearch { get; set; }

    /// <summary>
    /// Gets or sets the callback method to get display text. Default is null, using ToString() method.
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// Gets or sets the event callback when the clear button is clicked. Default is null.
    /// </summary>
    [Parameter]
    public Func<Task>? OnClear { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Search<TValue>>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("search auto-complete")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? UseInputString => IsTriggerSearchByInput ? null : "false";

    private string? ShowDropdownListOnFocusString => IsTriggerSearchByInput ? "true" : null;

    [NotNull]
    private string? ButtonIcon { get; set; }

    [NotNull]
    private List<TValue>? _filterItems = null;

    [NotNull]
    private SearchContext<TValue>? _context = null;

    [NotNull]
    private RenderTemplate? _dropdown = null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _context = new SearchContext<TValue>(this, OnSearchClick, OnClearClick);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputClearIcon);
        ClearButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchClearButtonIcon);
        SearchButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);
        SearchButtonLoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonLoadingIcon);
        PrefixIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);

        SearchButtonText ??= Localizer[nameof(SearchButtonText)];
        ButtonIcon ??= SearchButtonIcon;
        NoDataTip ??= Localizer[nameof(NoDataTip)];
        _filterItems ??= [];

        if (Debounce == 0)
        {
            Debounce = 200;
        }
    }

    private string _displayText = "";
    private async Task OnSearchClick()
    {
        if (OnSearch != null)
        {
            ButtonIcon = SearchButtonLoadingIcon;
            await Task.Yield();

            var items = await OnSearch(_displayText);
            _filterItems = [.. items];
            ButtonIcon = SearchButtonIcon;
            if (IsTriggerSearchByInput == false)
            {
                await InvokeVoidAsync("showList", Id);
            }
        }
    }

    private async Task OnClearClick()
    {
        _displayText = "";
        if (OnClear != null)
        {
            await OnClear();
        }
        await OnSearchClick();
    }

    private string? GetDisplayText(TValue item)
    {
        var displayText = item?.ToString();
        if (OnGetDisplayText != null)
        {
            displayText = OnGetDisplayText(item);
        }
        return displayText;
    }

    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        _displayText = GetDisplayText(val) ?? "";

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }

    /// <summary>
    /// TriggerFilter method called by Javascript.
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public async Task TriggerFilter(string val)
    {
        _displayText = val;
        if (IsTriggerSearchByInput)
        {
            await OnSearchClick();
            StateHasChanged();
        }
        _dropdown.Render();
    }
}
