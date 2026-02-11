// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Specialized;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Exception 异常扩展类</para>
/// <para lang="en">Exception extension class</para>
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// <para lang="zh">格式化异常信息</para>
    /// <para lang="en">Format exception information</para>
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="collection"></param>
    public static string Format(this Exception exception, NameValueCollection? collection = null)
    {
        var logger = new StringBuilder();

        if (collection != null)
        {
            foreach (string key in collection)
            {
                logger.AppendFormat("{0}: {1}", key, collection[key]);
                logger.AppendLine();
            }
        }
        logger.AppendFormat("{0}: {1}", nameof(Exception.Message), exception.Message);
        logger.AppendLine();

        logger.AppendLine(new string('*', 45));
        logger.AppendFormat("{0}: {1}", nameof(Exception.StackTrace), exception.StackTrace);
        logger.AppendLine();

        return logger.ToString();
    }

    /// <summary>
    /// <para lang="zh">格式化异常信息为 MarkupString</para>
    /// <para lang="en">Format exception information to MarkupString</para>
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="collection"></param>
    public static MarkupString FormatMarkupString(this Exception exception, NameValueCollection? collection = null)
    {
        var message = Format(exception, collection);
        return new MarkupString(message.Replace(Environment.NewLine, "<br />"));
    }
}
