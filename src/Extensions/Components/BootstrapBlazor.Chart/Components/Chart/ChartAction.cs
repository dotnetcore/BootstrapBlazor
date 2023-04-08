// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart 组件方法枚举
/// </summary>
public enum ChartAction
{
    /// <summary>
    /// 更新数据源
    /// </summary>
    [Description("update")]
    Update,

    /// <summary>
    /// 增加数据集
    /// </summary>
    [Description("addDataset")]
    AddDataset,

    /// <summary>
    /// 减少数据集
    /// </summary>
    [Description("removeDataset")]
    RemoveDataset,

    /// <summary>
    /// 增加数据
    /// </summary>
    [Description("addData")]
    AddData,

    /// <summary>
    /// 减少数据
    /// </summary>
    [Description("removeData")]
    RemoveData,

    /// <summary>
    /// 全圆/半圆
    /// </summary>
    [Description("setAngle")]
    SetAngle,

    /// <summary>
    /// 重新渲染图表
    /// </summary>
    [Description("reload")]
    Reload
}
