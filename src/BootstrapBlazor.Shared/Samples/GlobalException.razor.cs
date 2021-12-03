// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Samples
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GlobalException
    {
        private static void OnClick()
        {
            // NET6.0 采用 ErrorLogger 统一处理
            var a = 0;
            _ = 1 / a;
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = nameof(ErrorLogger.ChildContent),
                Description = "子组件模板",
                Type = nameof(RenderTemplate),
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = nameof(ErrorLogger.ErrorContent),
                Description = "异常显示模板",
                Type = nameof(RenderTemplate),
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = nameof(ErrorLogger.ShowToast),
                Description = "是否显示错误消息弹窗",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            }
        };
    }
}
