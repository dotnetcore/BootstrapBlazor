// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BreadcrumbItem 实体类
    /// </summary>
    public class BreadcrumbItem
    {
        /// <summary>
        /// 获得/设置 导航地址
        /// </summary>
        public string? Url { get; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        public BreadcrumbItem(string text, string? url = null)
        {
            Text = text;
            Url = url;
        }
    }
}
