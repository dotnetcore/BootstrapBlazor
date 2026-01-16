// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Search component</para>
/// <para lang="en">Search component</para>
/// </summary>
public partial class Search<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 模板. 默认为 null if not set.</para>
    /// <para lang="en">Gets or sets the icon template. Default is null if not set.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? IconTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the clear 按钮. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show the clear button. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 of clear 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the icon of clear button. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ClearButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the text of clear 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the text of clear button. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 颜色 of clear 按钮. 默认为 <see cref="Color.Primary"/>.</para>
    /// <para lang="en">Gets or sets the color of clear button. Default is <see cref="Color.Primary"/>.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color ClearButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the search 按钮. 默认为 true.</para>
    /// <para lang="en">Gets or sets whether to show the search button. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearchButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 the search 按钮 颜色. 默认为 <see cref="Color.Primary"/>.</para>
    /// <para lang="en">Gets or sets the search button color. Default is <see cref="Color.Primary"/>.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color SearchButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 the search 按钮 图标. 默认为 null.</para>
    /// <para lang="en">Gets or sets the search button icon. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? SearchButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the loading 图标 for the search 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the loading icon for the search button. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? SearchButtonLoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the search 按钮 text. 默认为 null.</para>
    /// <para lang="en">Gets or sets the search button text. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 按钮 模板. 默认为 null.</para>
    /// <para lang="en">Gets or sets the button template. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? ButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the prefix 按钮 模板. 默认为 null.</para>
    /// <para lang="en">Gets or sets the prefix button template. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? PrefixButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the prefix 图标. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show the prefix icon. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowPrefixIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the prefix 图标. 默认为 null.</para>
    /// <para lang="en">Gets or sets the prefix icon. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PrefixIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the prefix 图标 模板. 默认为 null.</para>
    /// <para lang="en">Gets or sets the prefix icon template. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? PrefixIconTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to automatically clear the search box after searching. Deprecated.</para>
    /// <para lang="en">Gets or sets whether to automatically clear the search box after searching. Deprecated.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public bool IsAutoClearAfterSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the search is triggered by input. 默认为 true. If false, the search 按钮 must be clicked to trigger.</para>
    /// <para lang="en">Gets or sets whether the search is triggered by input. Default is true. If false, the search button must be clicked to trigger.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsTriggerSearchByInput { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调 委托 when the search 按钮 is clicked.</para>
    /// <para lang="en">Gets or sets the callback delegate when the search button is clicked.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string?, Task<IEnumerable<TValue>>>? OnSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 to get 显示 text. 默认为 null, using ToString() method.</para>
    /// <para lang="en">Gets or sets the callback method to get display text. Default is null, using ToString() method.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the event 回调 when the clear 按钮 is clicked. 默认为 null.</para>
    /// <para lang="en">Gets or sets the event callback when the clear button is clicked. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已取消 合并到 OnSearch 方法中; Deprecated. Merged into the OnSearch method")]
    [ExcludeFromCodeCoverage]
    public Func<Task>? OnClear { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Search<TValue>>? Localizer { get; set; }

    private string? TriggerBlurString => OnBlurAsync != null ? "true" : null;

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
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _context = new SearchContext<TValue>(this, OnSearchClick, OnClearClick);
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchClearButtonIcon);
        SearchButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);
        SearchButtonLoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonLoadingIcon);
        PrefixIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);

        SearchButtonText ??= Localizer[nameof(SearchButtonText)];
        ButtonIcon ??= SearchButtonIcon;
        NoDataTip ??= Localizer[nameof(NoDataTip)];
        _filterItems ??= [];

        // <para lang="zh">这里应该获得初始值</para>
        // <para lang="en">Should get initial value here</para>
        _displayText = GetDisplayText(Value);

        if (Debounce == 0)
        {
            Debounce = 200;
        }
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, _displayText);

    private string? _displayText;
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
        // <para lang="zh">使用脚本更新 input 值</para>
        // <para lang="en">Update input value using script</para>
        await InvokeVoidAsync("setValue", Id, "");

        _displayText = null;
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
        _displayText = GetDisplayText(val);

        // <para lang="zh">使用脚本更新 input 值</para>
        // <para lang="en">Update input value using script</para>
        await InvokeVoidAsync("setValue", Id, _displayText);

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
    /// <para lang="zh">TriggerFilter method called by Javascript.</para>
    /// <para lang="en">TriggerFilter method called by Javascript.</para>
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
