﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

static class ConsoleMessageItemExtensions
{
    /// <summary>
    /// 渲染消息方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static RenderFragment RenderMessage(this ConsoleMessageItem item) => builder =>
    {
        if (item.IsHtml)
        {
            builder.AddContent(0, new MarkupString(item.Message));
        }
        else
        {
            builder.AddContent(0, item.Message);
        }
    };
}
