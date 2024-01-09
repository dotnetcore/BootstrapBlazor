// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// CollapseItem 组件
/// </summary>
public class CollapseItem : BootstrapComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 文本文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 当前状态是否激活 默认 true
    /// </summary>
    [Parameter]
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// 获得/设置 图标字符串 默认为 null
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 标题颜色 默认无颜色 Color.None
    /// </summary>
    [Parameter]
    public Color TitleColor { get; set; }

    /// <summary>
    /// 获得/设置 CSS 样式名称 默认 null
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 所属 Collapse 实例
    /// </summary>
    [CascadingParameter]
    protected Collapse? Collpase { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Collpase?.AddItem(this);
    }

    /// <summary>
    /// 设置是否被选中方法
    /// </summary>
    /// <param name="collapsed"></param>
    public virtual void SetCollapsed(bool collapsed) => IsCollapsed = collapsed;

    private bool disposedValue;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;

            if (disposing)
            {
                Collpase?.RemoveItem(this);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
