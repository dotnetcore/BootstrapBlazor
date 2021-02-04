// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ValidateFormBase 支持客户端验证组件基类
    /// </summary>
    public abstract class ValidateFormBase : IdComponentBase
    {
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
        private ConcurrentDictionary<(Type ModelType, string FieldName), IValidateComponent> ValidatorCache { get; } = new ConcurrentDictionary<(Type, string), IValidateComponent>();

        /// <summary>
        /// 添加数据验证组件到 EditForm 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="comp"></param>
        internal void AddValidator((Type ModelType, string FieldName) key, IValidateComponent comp) => ValidatorCache.AddOrUpdate(key, k => comp, (k, c) => c = comp);

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
