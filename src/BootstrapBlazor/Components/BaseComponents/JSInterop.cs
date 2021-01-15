// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <param name="args"></param>
        public async ValueTask Invoke(TValue value, object? el, string func, params object[] args)
        {
            _objRef = DotNetObjectReference.Create(value);
            var paras = new List<object>()
            {
                _objRef
            };
            paras.AddRange(args);
            await _jsRuntime.InvokeVoidAsync(el, func, paras.ToArray());
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
                _objRef = null;
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
