// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">时间搜索元数据类</para>
/// <para lang="en">DateTime search meta data class</para>
/// <para><version>v10.5.0</version></para>
/// </summary>
public class DateTimeSearchMetadata : SearchFormItemMetadataBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示快捷侧边栏 默认不显示</para>
    /// <para lang="en">Gets or sets Whether to Show Sidebar. Default is not shown</para>
    /// </summary>
    public bool ShowSidebar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏快捷选项集合</para>
    /// <para lang="en">Gets or sets Sidebar Items</para>
    /// <para><version>11.1.0</version></para>
    /// </summary>
    public List<DateTimePickerSidebarItem>? SidebarItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏模板 默认 null</para>
    /// <para lang="en">Gets or sets Sidebar Template. Default is null</para>
    /// </summary>
    public RenderFragment<Func<DateTime, Task>>? SidebarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索值</para>
    /// <para lang="en">Gets or sets the search start value</para>
    /// </summary>
    public DateTime? Value { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override FilterKeyValueAction? GetFilter(string fieldName)
    {
        // 如果用户设置了 GetFilterCallback 回调，则调用该回调获取过滤器实例
        if (GetFilterCallback != null)
        {
            return GetFilterCallback(Value);
        }

        if (Value == null)
        {
            return null;
        }

        return new FilterKeyValueAction()
        {
            FieldKey = fieldName,
            FieldValue = Value,
            FilterLogic = FilterLogic,
            FilterAction = FilterAction
        };
    }

    /// <summary>
    /// <para lang="zh">搜索值变化事件处理器</para>
    /// <para lang="en">Search value changed event handler</para>
    /// </summary>
    /// <param name="value"></param>
    public async Task ValueChangedHandler(DateTime? value)
    {
        Value = value;

        if (ValueChanged != null)
        {
            await ValueChanged();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        Value = null;
    }
}
