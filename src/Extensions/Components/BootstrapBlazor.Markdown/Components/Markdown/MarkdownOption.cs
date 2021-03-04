// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Markdown组件设置项
    /// </summary>
    internal class MarkdownOption
    {
        /// <summary>
        /// 编辑器呈现界面：markdown,wysiwyg(所见即所得)
        /// </summary>
        public string InitialEditType { get; set; } = "markdown";

        /// <summary>
        /// 预览方式：vertical(分栏), tab(tab页)
        /// </summary>
        public string PreviewStyle { get; set; } = "vertical";

        /// <summary>
        /// 高度：px值，默认300px
        /// </summary>
        public string Height { get; set; } = "300px";

        /// <summary>
        /// 最小高度：px值，默认200px
        /// </summary>
        public string MinHeight { get; set; } = "200px";

        /// <summary>
        /// 语言，默认为简体中文，如果改变，需要自行引入语言包
        /// </summary>
        public string Language { get; set; } = "zh-CN";

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Placeholder { get; set; } = "";

        /// <summary>
        /// 使用google分析，默认不使用
        /// </summary>
        public bool UsageStatistics { get; set; } = false;
    }
}
