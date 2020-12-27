// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 控制台消息实体类
    /// </summary>
    public class ConsoleMessageItem
    {
        /// <summary>
        /// 获得/设置 控制台输出消息
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 获得/设置 控制台消息颜色 默认为 White 白色
        /// </summary>
        public Color Color { get; set; } = Color.None;
    }
}
