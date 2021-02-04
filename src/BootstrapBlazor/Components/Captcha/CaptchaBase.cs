// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Captcha 滑块验证码组件
    /// </summary>
    public abstract class CaptchaBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 验证码结果回调委托
        /// </summary>
        [Parameter]
        public Action<bool>? OnValid { get; set; }

        /// <summary>
        /// 获得/设置 图床路径 默认值为 images
        /// </summary>
        [Parameter]
        public string ImagesPath { get; set; } = "images";

        /// <summary>
        /// 获得/设置 图床路径 默认值为 Pic.jpg
        /// </summary>
        [Parameter]
        public string ImagesName { get; set; } = "Pic.jpg";

        /// <summary>
        /// 获得/设置 获取背景图方法委托
        /// </summary>
        [Parameter]
        public Func<string>? GetImageName { get; set; }

        /// <summary>
        /// 获得/设置 容错偏差
        /// </summary>
        [Parameter]
        public int Offset { get; set; } = 5;

        /// <summary>
        /// 获得/设置 图片宽度
        /// </summary>
        [Parameter]
        public int Width { get; set; } = 280;

        /// <summary>
        /// 获得/设置 图片高度
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 155;

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender) Reset();
        }

        /// <summary>
        /// 点击刷新按钮时回调此方法
        /// </summary>
        protected void OnClickRefresh() => Reset();

        /// <summary>
        /// 重置组件方法
        /// </summary>
        protected abstract void Reset();
    }
}
