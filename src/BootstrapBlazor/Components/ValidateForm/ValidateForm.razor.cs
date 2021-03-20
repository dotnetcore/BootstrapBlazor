// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ValidateForm 组件类
    /// </summary>
    public sealed partial class ValidateForm
    {
        /// <summary>
        /// A callback that will be invoked when the form is submitted.
        /// If using this parameter, you are responsible for triggering any validation
        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be valid.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnValidSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be invalid.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnInvalidSubmit { get; set; }

        /// <summary>
        /// 获得/设置 是否验证所有字段 默认 false
        /// </summary>
        [Parameter]
        public bool ValidateAllProperties { get; set; }

        /// <summary>
        /// Specifies the top-level model object for the form. An edit context will
        /// be constructed for this model. If using this parameter, do not also supply
        /// a value for <see cref="EditContext"/>.
        /// </summary>
        [Parameter]
        public object? Model { get; set; }

        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 验证组件缓存
        /// </summary>
        private ConcurrentDictionary<(Type ModelType, string FieldName), IValidateComponent> ValidatorCache { get; } = new();

        /// <summary>
        /// 添加数据验证组件到 EditForm 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="comp"></param>
        internal void AddValidator((Type ModelType, string FieldName) key, IValidateComponent comp) => ValidatorCache.AddOrUpdate(key, k => comp, (k, c) => c = comp);

        /// <summary>
        /// 设置指定字段错误信息
        /// </summary>
        /// <param name="modelType">数据类型</param>
        /// <param name="filedName">字段名称</param>
        /// <param name="errorMessage">错误描述信息，可为空，为空时查找资源文件</param>
        public void SetError(Type modelType, string filedName, string errorMessage)
        {
            if (ValidatorCache.TryGetValue((modelType, filedName), out var validator))
            {
                var results = new List<ValidationResult>
                {
                    new ValidationResult(errorMessage, new string[] { filedName })
                };
                validator.ToggleMessage(results, true);
            }
        }

        /// <summary>
        /// 通过指定的模型类型获取当前表单中的绑定属性
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        internal IEnumerable<string> GetPropertiesByModelType(Type modelType) => ValidatorCache.Keys.Where(k => k.ModelType == modelType).Select(k => k.FieldName);

        /// <summary>
        /// EditModel 数据模型验证方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        internal void ValidateObject(object model, ValidationContext context, List<ValidationResult> results)
        {
            // 遍历所有可验证组件进行数据验证
            foreach (var key in ValidatorCache)
            {
                if (key.Key.ModelType == context.ObjectType)
                {
                    var fi = new FieldIdentifier(model, key.Key.FieldName);

                    // 设置其关联属性字段
                    var propertyValue = fi.GetPropertyValue();
                    var validator = ValidatorCache[key.Key];

                    // 数据验证
                    context.MemberName = fi.FieldName;
                    validator.ValidateProperty(propertyValue, context, results);
                    validator.ToggleMessage(results, false);
                }
            }
        }

        /// <summary>
        /// 字段验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        internal void ValidateProperty(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            if (!string.IsNullOrEmpty(context.MemberName) && ValidatorCache.TryGetValue((context.ObjectType, context.MemberName), out var validator))
            {
                validator.ValidateProperty(propertyValue, context, results);
                validator.ToggleMessage(results, true);
            }
        }
    }
}
