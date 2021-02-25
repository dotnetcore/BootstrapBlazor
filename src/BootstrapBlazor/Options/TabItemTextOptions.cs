// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
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
        /// 获得/设置 是否激活 默认为 null
        /// </summary>
        /// <value></value>
        public bool? IsActive { get; set; }

        /// <summary>
        /// 获得/设置 当前 TabItem 是否可关闭 默认为 null
        /// </summary>
        public bool? Closable { get; set; }

        public void Reset()
        {
            Text = null;
            Icon = null;
            IsActive = null;
            Closable = null;
        }
    }
}
