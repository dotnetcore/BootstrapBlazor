// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Upload<TValue>
    {
        /// <summary>
        /// 获得/设置 上传组件模式 默认为 Normal 正常模式多用于表单中
        /// </summary>
        [Parameter]
        public UploadStyle Style { get; set; }

        /// <summary>
        /// 获得/设置 文件预览框宽度
        /// </summary>
        [Parameter]
        public int Width { get; set; } = 100;

        /// <summary>
        /// 获得/设置 文件预览框高度
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 100;

        /// <summary>
        /// 获得/设置 是否圆形图片框 Avatar 模式时生效 默认为 false
        /// </summary>
        [Parameter]
        public bool IsCircle { get; set; }
    }
}
