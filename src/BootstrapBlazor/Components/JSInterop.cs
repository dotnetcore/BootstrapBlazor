using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="el"></param>
        /// <param name="func"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public void Invoke(TValue value, object el, string func, string method, params object[] args)
        {
            _objRef = DotNetObjectReference.Create(value);
            var paras = new List<object>();
            if (!string.IsNullOrEmpty(method)) paras.Add(method);
            if (args != null) paras.AddRange(args);
            _jsRuntime.Invoke(el, func, _objRef, paras.ToArray());
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
