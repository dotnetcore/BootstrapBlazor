// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Specialized;
using System.Text;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ExceptionExtensions
    {
        /// <summary>
        /// 格式化异常信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string Format(this Exception exception, NameValueCollection collection)
        {
            var logger = new StringBuilder();
            foreach (string key in collection)
            {
                logger.AppendFormat("{0}: {1}", key, collection[key]);
                logger.AppendLine();
            }
            logger.AppendFormat("{0}: {1}", nameof(Exception.Message), exception.Message);
            logger.AppendLine();

            logger.AppendLine(new string('*', 45));
            logger.AppendFormat("{0}: {1}", nameof(Exception.StackTrace), exception.StackTrace);
            logger.AppendLine();

            return logger.ToString();
        }
    }
}
