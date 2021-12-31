// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 初始化编辑器显示界面
/// </summary>
public enum InitialEditType
{
    /// <summary>
    /// Markdown 界面
    /// </summary>
    [Description("markdown")]
    Markdown,

    /// <summary>
    /// 所见即所得界面
    /// </summary>
    [Description("wysiwyg")]
    Wysiwyg
}
