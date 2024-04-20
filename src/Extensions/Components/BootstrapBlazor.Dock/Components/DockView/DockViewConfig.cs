// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

class DockViewConfig
{
    /// <summary>
    /// 获得/设置 DockView 名称 要求页面内唯一
    /// </summary>
    public string Name { get; set; } = "default";

    /// <summary>
    /// 获得/设置 是否启用本地布局保持 默认 true
    /// </summary>
    public bool EnableLocalStorage { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否锁定 默认 false
    /// </summary>
    /// <remarks>锁定后无法拖动</remarks>
    [JsonPropertyName("lock")]
    public bool IsLock { get; set; }

    /// <summary>
    /// 获得/设置 配置信息版本号 默认 null
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// 获得/设置 标签页可见状态改变事件回调
    /// </summary>
    public string? VisibleChangedCallback { get; set; }

    /// <summary>
    /// 获得/设置 组件初始化完成事件回调
    /// </summary>
    public string? InitializedCallback { get; set; }

    /// <summary>
    /// 获得/设置 拖动标签页事件回调
    /// </summary>
    public string? TabDropCallback { get; set; }

    /// <summary>
    /// 获得/设置 调整标签页分割线事件回调
    /// </summary>
    public string? SplitterCallback { get; set; }

    /// <summary>
    /// 获得/设置 锁定事件回调
    /// </summary>
    public string? LockChangedCallback { get; set; }

    /// <summary>
    /// 获得/设置 客户端缓存键值
    /// </summary>
    [JsonPropertyName("prefix")]
    public string? LocalStorageKeyPrefix { get; set; }

    /// <summary>
    /// 获得/设置 Golden-Layout 配置项集合 默认 空集合
    /// </summary>
    [JsonPropertyName("content")]
    [JsonConverter(typeof(DockContentRootConverter))]
    public List<DockContent> Contents { get; set; } = [];

    /// <summary>
    /// 获得/设置 布局配置 默认 null 未设置
    /// </summary>
    public string? LayoutConfig { get; set; }
}
