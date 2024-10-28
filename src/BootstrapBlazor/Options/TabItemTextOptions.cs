// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 菜单与标签捆绑配置类
/// </summary>
internal class TabItemTextOptions
{
    /// <summary>
    /// 获得/设置 Tab 标签文本
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 图标字符串
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否激活 默认为 true
    /// </summary>
    /// <value></value>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 获得/设置 当前 TabItem 是否可关闭 默认为 true
    /// </summary>
    public bool Closable { get; set; } = true;

    /// <summary>
    /// 重置方法
    /// </summary>
    public void Reset()
    {
        Text = null;
        Icon = null;
        IsActive = true;
        Closable = true;
    }

    /// <summary>
    /// 是否可用方法
    /// </summary>
    /// <returns></returns>
    public bool Valid() => Text != null;
}
