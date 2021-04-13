// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateForms
    {
        [NotNull]
        private Logger? Trace { get; set; }

        [NotNull]
        private Logger? Trace2 { get; set; }

        [NotNull]
        private Logger? Trace3 { get; set; }

        [NotNull]
        private Logger? Trace4 { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

        private Foo Model { get; set; } = new() { Name = "Name", Education = EnumEducation.Primary, DateTime = DateTime.Now };

        [NotNull]
        private IEnumerable<SelectedItem>? Hobbys { get; set; }

        [NotNull]
        private ValidateForm? FooForm { get; set; }

        [NotNull]
        private ValidateForm? ComplexForm { get; set; }

        [NotNull]
        private ComplexFoo? ComplexModel { get; set; }

        /// <summary>baise
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 初始化参数
            Hobbys = Foo.GenerateHobbys(LocalizerFoo);
            ComplexModel = new ComplexFoo()
            {
                Dummy = new Dummy1() { Dummy2 = new Dummy2() },
            };
        }

        private Task OnInvalidSubmit1(EditContext context)
        {
            Trace.Log("OnInvalidSubmit 回调委托");
            return Task.CompletedTask;
        }

        private async Task OnValidSubmit(EditContext context)
        {
            Trace2.Log("OnValidSubmit 回调委托: Starting ...");
            await Task.Delay(3000);
            Trace2.Log("OnValidSubmit 回调委托: Done!");
        }

        private Task OnInvalidSubmit(EditContext context)
        {
            Trace2.Log("OnInvalidSubmit 回调委托");
            return Task.CompletedTask;
        }

        private Task OnInvalidSubmitAddress(EditContext context)
        {
            Trace3.Log("OnInvalidSubmit 回调委托");
            return Task.CompletedTask;
        }

        private Task OnInvalidComplexModel(EditContext context)
        {
            Trace4.Log("OnInvalidSubmit 回调委托");
            return Task.CompletedTask;
        }

        private Task OnValidComplexModel(EditContext context)
        {
            Trace4.Log("OnValidSubmit 回调委托");
            ComplexForm.SetError("Dummy.Dummy2.Name", "数据库中已存在");
            return Task.CompletedTask;
        }

        private Task OnValidSetError(EditContext context)
        {
            FooForm.SetError<Foo>(f => f.Name, "数据库中已存在");
            return Task.CompletedTask;
        }

        private class ComplexFoo : Foo
        {
            [NotNull]
            public Dummy1? Dummy { get; set; }
        }

        private class Dummy1
        {
            [NotNull]
            public Dummy2? Dummy2 { get; set; }
        }

        private class Dummy2
        {
            [Required]
            public string? Name { get; set; }
        }

        #region 参数说明
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
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
                Name = "ValidateAllProperties",
                Description = "是否检查所有字段",
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowRequiredMark",
                Description = "表单内必填项是否显示 * 标记",
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
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
        private static IEnumerable<MethodItem> GetMethods() => new MethodItem[]
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
