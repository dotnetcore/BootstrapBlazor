// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBlazorLogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task Log(string message, params object[] args) => Log(LogLevel.Information, message, args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task Log(LogLevel logLevel, string message, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task Log(Exception ex, string message = "", params object[] args) => Log(LogLevel.Critical, ex, message, args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task Log(LogLevel logLevel, Exception exception, string message = "", params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        Task Log(LogLevel logLevel, Exception exception, NameValueCollection? collection = null);

        /// <summary>
        /// 格式化 Exception 信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        string FormatException(Exception ex, NameValueCollection? collection = null);
    }
}
