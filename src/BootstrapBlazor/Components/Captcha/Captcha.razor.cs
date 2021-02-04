// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Captcha
    {
        private static Random ImageRandomer { get; set; } = new Random();

        private int OriginX { get; set; }

        private JSInterop<Captcha>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 Captcha DOM 元素实例
        /// </summary>
        private ElementReference CaptchaElement { get; set; }

        /// <summary>
        /// 获得 组件宽度
        /// </summary>
        private string? StyleString => CssBuilder.Default()
            .AddClass($"width: {Width + 42}px;", Width > 0)
            .Build();

        /// <summary>
        /// 获得 加载图片失败样式
        /// </summary>
        private string? FailedStyle => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width > 0)
            .AddClass($"height: {Height}px;", Height > 0)
            .Build();

        /// <summary>
        /// 获得/设置 Header 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? HeaderText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? BarText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? FailedText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? LoadText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        public string? TryText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Captcha>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            HeaderText ??= Localizer[nameof(HeaderText)];
            BarText ??= Localizer[nameof(BarText)];
            FailedText ??= Localizer[nameof(FailedText)];
            LoadText ??= Localizer[nameof(LoadText)];
            TryText ??= Localizer[nameof(TryText)];
        }

        /// <summary>
        /// 清除 ToastBox 方法
        /// </summary>
        [JSInvokable]
        public Task<bool> Verify(int offset, IEnumerable<int> trails)
        {
            var ret = Math.Abs(offset - OriginX) < Offset && CalcStddev(trails);
            OnValid?.Invoke(ret);
            return Task.FromResult(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CaptchaOption GetCaptchaOption()
        {
            var option = new CaptchaOption()
            {
                Width = Width,
                Height = Height
            };
            option.BarWidth = option.SideLength + option.Diameter * 2 + 6; // 滑块实际边长
            var start = option.BarWidth + 10;
            var end = option.Width - start;
            option.OffsetX = Convert.ToInt32(Math.Ceiling(ImageRandomer.Next(0, 100) / 100.0 * (end - start) + start));
            OriginX = option.OffsetX;

            start = 10 + option.Diameter * 2;
            end = option.Height - option.SideLength - 10;
            option.OffsetY = Convert.ToInt32(Math.Ceiling(ImageRandomer.Next(0, 100) / 100.0 * (end - start) + start));

            if (GetImageName == null)
            {
                var index = Convert.ToInt32(ImageRandomer.Next(0, 8) / 1.0);
                var imageName = Path.GetFileNameWithoutExtension(ImagesName);
                var extendName = Path.GetExtension(ImagesName);
                var fileName = $"{imageName}{index}{extendName}";
                option.ImageUrl = Path.Combine(ImagesPath, fileName);
            }
            else
                option.ImageUrl = GetImageName();

            return option;
        }

        private bool CalcStddev(IEnumerable<int> trails)
        {
            var ret = false;
            if (trails.Any())
            {
                var average = trails.Sum() * 1.0 / trails.Count();
                var dev = trails.Select(t => t - average);
                var stddev = Math.Sqrt(dev.Sum() * 1.0 / dev.Count());
                ret = stddev != 0;
            }
            return ret;
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) Interop?.Dispose();
        }

        /// <summary>
        /// 重置组件方法
        /// </summary>
        protected override void Reset()
        {
            var option = GetCaptchaOption();
            if (Interop == null) Interop = new JSInterop<Captcha>(JSRuntime);
            Interop?.Invoke(this, CaptchaElement, "captcha", nameof(Verify), option);
        }
    }
}
