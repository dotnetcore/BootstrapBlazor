// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Forms
    {
        #region 参数说明
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Model",
                Description = "表单组件绑定的数据模型，必填属性",
                Type = "object",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "子组件模板实例",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSubmit",
                Description = "表单提交时的回调委托",
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnValidSubmit",
                Description = "表单提交时数据合规检查通过时的回调委托",
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnInvalidSubmit",
                Description = "表单提交时数据合规检查未通过时的回调委托",
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            new MethodItem()
            {
                Name = "Validate",
                Description="表单验证方法",
                Parameters =" — ",
                ReturnValue = "Task<bool>"
            }
        };
        #endregion
    }
}
