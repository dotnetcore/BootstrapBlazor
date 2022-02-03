// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Search 组件基类
/// </summary>
public partial class Search
{
    [NotNull]
    private string? ButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示清除按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// Clear button icon
    /// </summary>
    [Parameter]
    public string ClearButtonIcon { get; set; } = "fa fa-fw fa-trash";

    /// <summary>
    /// Clear button text
    /// </summary>
    [Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// Clear button color
    /// </summary>
    [Parameter]
    public Color ClearButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 搜索按钮颜色
    /// </summary>
    [Parameter]
    public Color SearchButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 搜索按钮图标
    /// </summary>
    [Parameter]
    public string SearchButtonIcon { get; set; } = "fa fa-fw fa-search";

    /// <summary>
    /// 获得/设置 正在搜索按钮图标
    /// </summary>
    [Parameter]
    public string SearchButtonLoadingIcon { get; set; } = "fa fa-fw fa-spinner fa-spin";

    /// <summary>
    /// 获得/设置 点击搜索后是否自动清空搜索框
    /// </summary>
    [Parameter]
    public bool IsAutoClearAfterSearch { get; set; }

    /// <summary>
    /// 获得/设置 搜索模式是否为输入即触发 默认 false 点击搜索按钮触发
    /// </summary>
    [Parameter]
    public bool IsOnInputTrigger { get; set; }

    /// <summary>
    /// 获得/设置 搜索按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchButtonText { get; set; }

    /// <summary>
    /// 获得/设置 点击搜索按钮时回调委托
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnSearch { get; set; }

    /// <summary>
    /// 获得/设置 点击清空按钮时回调委托
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnClear { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Search>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        SearchButtonText ??= Localizer[nameof(SearchButtonText)];
        ButtonIcon = SearchButtonIcon;

        SkipEnter = true;
        SkipEsc = true;
    }

    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnSearchClick()
    {
        if (OnSearch != null)
        {
            ButtonIcon = SearchButtonLoadingIcon;
            await OnSearch(CurrentValueAsString);
            ButtonIcon = SearchButtonIcon;
        }

        if (IsAutoClearAfterSearch)
        {
            CurrentValueAsString = "";
        }

        await FocusAsync();
    }

    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClearClick()
    {
        if (OnClear != null)
        {
            await OnClear(CurrentValueAsString);
        }
        CurrentValueAsString = "";
    }

    /// <summary>
    /// OnKeyUp 方法
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override async Task OnKeyUp(KeyboardEventArgs args)
    {
        await base.OnKeyUp(args);

        if (!string.IsNullOrEmpty(CurrentValueAsString))
        {
            if (args.Key == "Escape")
            {
                if (OnEscAsync != null)
                {
                    await OnEscAsync(Value);
                }

                // 清空
                await OnClearClick();
            }

            if (IsOnInputTrigger || args.Key == "Enter")
            {
                if (OnEnterAsync != null)
                {
                    await OnEnterAsync(Value);
                }

                // 搜索
                await OnSearchClick();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected override async Task OnClickItem(string item)
    {
        await base.OnClickItem(item);

        if (IsOnInputTrigger)
        {
            await OnSearchClick();
        }
    }
}
