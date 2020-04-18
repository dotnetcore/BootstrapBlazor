using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

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
        /// Invoke 方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="el"></param>
        /// <param name="func"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public void Invoke(TValue value, object el, string func, string method, params object[] args)
        {
            _objRef = DotNetObjectReference.Create(value);
            _jsRuntime.InvokeRun(el, func, _objRef, method, args);
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
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
