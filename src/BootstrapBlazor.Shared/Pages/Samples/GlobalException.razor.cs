// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GlobalException
    {
        [CascadingParameter]
        [NotNull]
        private IBlazorLogger? Logger { get; set; }

        [NotNull]
        private BlockLogger? Trace { get; set; }

        private void OnClick()
        {
            try
            {
                var a = 0;
                var b = 1 / a;
            }
            catch (Exception ex)
            {
                Trace.Log(Logger.FormatException(ex).Replace(Environment.NewLine, "<br />"), true);

                // 输出到消息中心
                Logger.Log(ex);
            }
        }
    }
}
