// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// TabItem 组件基类
/// </summary>
public class TabItem : ComponentBase
{
    /// <summary>
    /// 获得/设置 文本文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 TabItem Header 模板
    /// </summary>
    [Parameter]
    public RenderFragment<TabItem>? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 请求地址
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 当前状态是否激活
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 当前 TabItem 是否可关闭 默认为 true 可关闭
    /// </summary>
    [Parameter]
    public bool Closable { get; set; } = true;

    /// <summary>
    /// 获得/设置 当前 TabItem 是否始终加载 此参数作用于设置 <see cref="Tab.IsLazyLoadTabItem"/> 默认 false
    /// </summary>
    [Parameter]
    public bool AlwaysLoad { get; set; }

    /// <summary>
    /// 获得/设置 图标字符串
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示全屏按钮 默认 true
    /// </summary>
    [Parameter]
    public bool ShowFullScreen { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 所属 Tab 实例
    /// </summary>
    [CascadingParameter]
    protected internal Tab? TabSet { get; set; }

    private string? LastText { get; set; }

    /// <summary>
    /// OnInitialized 方法
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
    /// 设置是否被选中方法
    /// </summary>
    /// <param name="active"></param>
    public virtual void SetActive(bool active) => IsActive = active;

    /// <summary>
    /// 重新设置标签文字等参数
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
    /// 通过指定参数集合获取 TabItem 实例
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
        _ = item.SetParametersAsync(ParameterView.FromDictionary(parameters!));
        return item;
    }
}
