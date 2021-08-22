// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ColorPickers
    {
        [NotNull]
        private BlockLogger? Trace { get; set; }

        private string? Value1 { get; set; }

        private string Value2 { get; set; } = "#FFFFFF";

        private string Value3 { get; set; } = "#DDDDDD";

        private string? Value4 { get; set; }

        private string? Value5 { get; set; }

        [NotNull]
        private Foo? Dummy { get; set; } = new Foo() { Name = "#dddddd" };

        private Task OnColorChanged(string color)
        {
            Trace.Log($"Selected color: {color}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem()
            {
                Name = "OnValueChanged",
                Description = "颜色改变回调委托方法",
                Type = "Func<string, Task>",
                ValueList = "",
                DefaultValue = ""
            }
        };
    }
}
