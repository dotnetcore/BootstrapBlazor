﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Search 组件
/// </summary>
public partial class Search<TValue>
{
    /// <summary>
    /// 获得/设置 是否显示清除按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// Clear button icon
    /// </summary>
    [Parameter]
    public string? ClearButtonIcon { get; set; }

    /// <summary>
    /// Clear button text
    /// </summary>
    [Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// Clear button color
    /// </summary>
    [Parameter]
    public Color ClearButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 搜索按钮颜色
    /// </summary>
    [Parameter]
    public Color SearchButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 搜索按钮图标
    /// </summary>
    [Parameter]
    public string? SearchButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 正在搜索按钮图标
    /// </summary>
    [Parameter]
    public string? SearchButtonLoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 搜索按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchButtonText { get; set; }

    /// <summary>
    /// 获得/设置 点击搜索后是否自动清空搜索框
    /// </summary>
    [Parameter]
    public bool IsAutoClearAfterSearch { get; set; }

    /// <summary>
    /// 获得/设置 搜索模式是否为输入即触发 默认 true 值为 false 时需要点击搜索按钮触发
    /// </summary>
    [Parameter]
    public bool IsOnInputTrigger { get; set; } = true;

    /// <summary>
    /// 获得/设置 点击搜索按钮时回调委托
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnSearch { get; set; }

    /// <summary>
    /// 获得/设置 通过模型获得显示文本方法 默认使用 ToString 重载方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// 获得/设置 点击清空按钮时回调委托
    /// </summary>
    [Parameter]
    public Func<string?, Task>? OnClear { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Search<TValue>>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    private string? ClassString => CssBuilder.Default("search auto-complete")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? UseInputString => IsOnInputTrigger ? null : "false";

    [NotNull]
    private string? ButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 UI 呈现数据集合
    /// </summary>
    [NotNull]
    private List<TValue>? FilterItems { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchClearButtonIcon);
        SearchButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);
        SearchButtonLoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonLoadingIcon);
        SearchButtonText ??= Localizer[nameof(SearchButtonText)];
        ButtonIcon ??= SearchButtonIcon;

        FilterItems ??= [];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_show)
        {
            _show = false;
            await InvokeVoidAsync("showList", Id);
        }
    }

    private string _displayText = "";
    private bool _show;
    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnSearchClick()
    {
        if (OnSearch != null)
        {
            ButtonIcon = SearchButtonLoadingIcon;
            await Task.Yield();

            var items = await OnSearch(_displayText);
            FilterItems = items.ToList();
            ButtonIcon = SearchButtonIcon;
            if (IsAutoClearAfterSearch)
            {
                _displayText = "";
            }
            if (IsOnInputTrigger == false)
            {
                _show = true;
            }
            StateHasChanged();
        }
    }

    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClearClick()
    {
        if (OnClear != null)
        {
            await OnClear(_displayText);
        }
        _displayText = "";
        FilterItems = [];
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

    /// <summary>
    /// TriggerOnChange 方法
    /// </summary>
    /// <param name="val"></param>
    /// <param name="search"></param>
    [JSInvokable]
    public async Task TriggerOnChange(string val, bool search = true)
    {
        _displayText = val;

        if (search)
        {
            await OnSearchClick();
        }
    }
}
