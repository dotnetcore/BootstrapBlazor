// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Enums;

/// <summary>
/// head标签元素类型枚举
/// </summary>
public enum HeadMetaType
{
    /// <summary>
    /// 定义文档的标题，这个标题会显示在浏览器的标题栏或者标签页上。
    /// </summary>
    [Description("title")]
    Title,
    /// <summary>
    /// 提供关于HTML文档的元数据，例如文档的字符编码、视窗设置、SEO相关的关键词和描述等。
    /// </summary>
    [Description("meta")]
    Meta,
    /// <summary>
    /// 定义文档与外部资源的关系，常用于链接CSS样式表或者设置网站图标（favicon）。
    /// </summary>
    [Description("link")]
    Link,
    /// <summary>
    /// 包含文档的内联CSS样式。
    /// </summary>
    [Description("style")]
    Style,
    /// <summary>
    /// 包含文档的JavaScript代码。
    /// </summary>
    [Description("script")]
    Script,
    /// <summary>
    /// 定义在不支持脚本或者脚本被禁用的情况下显示的替代内容。
    /// </summary>
    [Description("noscript")]
    NoScript
}
