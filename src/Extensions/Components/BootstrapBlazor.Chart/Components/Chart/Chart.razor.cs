// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 组件基类
    /// </summary>
    public sealed partial class Chart : BootstrapComponentBase
    {
        private JSInterop<Chart>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 EChart DOM 元素实例
        /// </summary>
        private ElementReference ChartElement { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        private string? ClassName => CssBuilder.Default("chart is-loading")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 组件 Style 字符串
        /// </summary>
        private string? StyleString => CssBuilder.Default()
            .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width))
            .Build();

        /// <summary>
        /// 获得/设置 组件宽度支持单位
        /// </summary>
        [Parameter]
        public string? Width { get; set; }

        /// <summary>
        /// 获得/设置 组件数据初始化委托方法
        /// </summary>
        [Parameter]
        public Func<Task<ChartDataSource>>? OnInit { get; set; }

        /// <summary>
        /// 获得/设置 客户端绘制图表完毕后回调此委托方法
        /// </summary>
        [Parameter]
        public Action? OnAfterInit { get; set; }

        /// <summary>
        /// 获得/设置 图表组件渲染类型 默认为 line 图
        /// </summary>
        [Parameter]
        public ChartType ChartType { get; set; } = ChartType.Line;

        /// <summary>
        /// 获得/设置 Bubble 模式下显示角度 180 为 半圆 360 为正圆
        /// </summary>
        [Parameter]
        public int Angle { get; set; }

        /// <summary>
        /// 获得/设置 正在加载文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? LoadingText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Chart>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            LoadingText ??= Localizer[nameof(LoadingText)];
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                if (OnInit == null) throw new InvalidOperationException("OnInit paramenter must be set");

                if (Interop == null) Interop = new JSInterop<Chart>(JSRuntime);

                var ds = await OnInit.Invoke();

                await Interop.Invoke(this, ChartElement, "chart", nameof(Completed), ds, "", ChartType.ToDescriptionString());
            }
        }

        /// <summary>
        /// 图表绘制完成后回调此方法
        /// </summary>
        [JSInvokable]
        public void Completed()
        {
            OnAfterInit?.Invoke();
        }

        /// <summary>
        /// 设置 Doughnut 图形显示角度
        /// </summary>
        /// <param name="angle"></param>
        public void SetAngle(int angle) => Angle = angle;

        /// <summary>
        /// 更新图表方法
        /// </summary>
        public async Task Update(string method = "")
        {
            if (OnInit != null)
            {
                var ds = await OnInit.Invoke();
                if (Interop != null)
                {
                    await Interop.Invoke(this, ChartElement, "chart", nameof(Completed), ds, method, ChartType.ToDescriptionString(), Angle);
                }
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing) Interop?.Dispose();

            base.Dispose(disposing);
        }
    }
}
