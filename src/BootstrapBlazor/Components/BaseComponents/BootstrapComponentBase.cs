using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Bootstrap Blazor 组件基类
    /// </summary>
    public abstract class BootstrapComponentBase : ComponentBase, IDisposable
    {
        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 组件初始化回调方法 用户扩展
        /// </summary>
        [Parameter]
        public Action<BootstrapComponentBase>? OnInitializedCallback { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject] protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 调用订阅信息
            OnInitializedCallback?.Invoke(this);
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

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
