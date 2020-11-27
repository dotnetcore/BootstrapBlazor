// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 验证组件基类
    /// </summary>
    public abstract class ValidatorComponentBase : ComponentBase, IValidator
    {
        /// <summary>
        /// 获得/设置 错误描述信息
        /// </summary>
        [Parameter]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 获得/设置 IValidateRules 实例
        /// </summary>
        [CascadingParameter]
        public IValidateRules? Validators { get; set; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (Validators == null)
            {
                throw new InvalidOperationException($"{nameof(IValidator)} requires a cascading " +
                    $"parameter of type {nameof(IValidateRules)}. For example, you can use {nameof(RequiredValidator)} " +
                    $"inside an ValidateInputBase<TItem>.");
            }

            Validators.Rules.Add(this);
            Validators.OnRuleAdded(this);
        }

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public abstract void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results);
    }
}
