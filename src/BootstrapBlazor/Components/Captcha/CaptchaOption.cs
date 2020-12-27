// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Captcha 滑块验证码组件
    /// </summary>
    public class CaptchaOption
    {
        /// <summary>
        /// 获得/设置 验证码图片宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 获得/设置 验证码图片高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 获得/设置 拼图边长
        /// </summary>
        public int SideLength { get; set; } = 42;

        /// <summary>
        /// 获得/设置 拼图直径
        /// </summary>
        public int Diameter { get; set; } = 9;

        /// <summary>
        /// 获得/设置 拼图 X 位置
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// 获得/设置 拼图 Y 位置
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// 获得/设置 拼图宽度
        /// </summary>
        public int BarWidth { get; set; }

        /// <summary>
        /// 获得/设置 拼图背景图片路径
        /// </summary>
        public string ImageUrl { get; set; } = "images/Pic0.jpg";
    }
}
