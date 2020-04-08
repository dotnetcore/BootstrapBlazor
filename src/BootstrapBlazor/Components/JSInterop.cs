using System;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// JSInterop 类
    /// </summary>
    public class JSInterop<TValue> : IDisposable where TValue : class
    {
        private readonly IJSRuntime _jsRuntime;
        private DotNetObjectReference<TValue>? _objRef;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsRuntime"></param>
        public JSInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Invoke 方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public void Invoke(TValue value, Action<IJSRuntime, DotNetObjectReference<TValue>> callback)
        {
            _objRef = DotNetObjectReference.Create(value);
            callback(_jsRuntime, _objRef);
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _objRef?.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}