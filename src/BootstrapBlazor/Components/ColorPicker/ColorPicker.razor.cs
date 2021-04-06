// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ColorPicker : IDisposable
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        [NotNull]
        private ElementReference ColorPickerElement { get; set; }

        private JSInterop<ColorPicker>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 input 类型 placeholder 属性
        /// </summary>
        protected string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 是否显示右侧颜色预览框 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowBar { get; set; } = true;

        /// <summary>
        /// 获得/设置 颜色更改回调委托 默认为 null
        /// </summary>
        [Parameter]
        public Func<string, Task>? ColorChanged { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var ph))
            {
                PlaceHolder = ph?.ToString();
            }
            if (string.IsNullOrEmpty(PlaceHolder) && FieldIdentifier.HasValue)
            {
                PlaceHolder = FieldIdentifier.Value.GetPlaceHolder();
            }
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Interop ??= new JSInterop<ColorPicker>(JSRuntime);
                await Interop.InvokeVoidAsync(this, ColorPickerElement, "bb_color_picker", nameof(UpdateColor));
            }
        }

        /// <summary>
        /// 更新颜色
        /// </summary>
        /// <param name="val"></param>
        [JSInvokable]
        public async Task UpdateColor(string val)
        {
            CurrentValue = val;

            if (ColorChanged != null)
            {
                await ColorChanged(val);
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
