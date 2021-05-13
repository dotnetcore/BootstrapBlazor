// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ValidateForm 组件类
    /// </summary>
    public partial class ValidateForm
    {
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
        [NotNull]
        public object? Model { get; set; }

        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 是否获取必填项标记 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowRequiredMark { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示验证表单内的 Label 默认为 null 未设置时默认显示
        /// </summary>
        [Parameter]
        public bool? ShowLabel { get; set; } = true;

        [Inject]
        [NotNull]
        private IOptions<JsonLocalizationOptions>? Options { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizerFactory? LocalizerFactory { get; set; }

        /// <summary>
        /// 验证组件缓存
        /// </summary>
        private ConcurrentDictionary<FieldIdentifier, IValidateComponent> ValidatorCache { get; } = new();

        /// <summary>
        /// 添加数据验证组件到 EditForm 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="comp"></param>
        internal void AddValidator(in FieldIdentifier key, IValidateComponent comp) => ValidatorCache.AddOrUpdate(key, k => comp, (k, c) => c = comp);

        /// <summary>
        /// 设置指定字段错误信息
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="errorMessage">错误描述信息，可为空，为空时查找资源文件</param>
        public void SetError<TModel>(Expression<Func<TModel, object?>> expression, string errorMessage)
        {
            if (expression.Body is MemberExpression exp)
            {
                var fieldName = exp.Member.Name;
                var modelType = exp.Expression?.Type;
                var validator = ValidatorCache.FirstOrDefault(c => c.Key.Model.GetType() == modelType && c.Key.FieldName == fieldName).Value;
                if (validator != null)
                {
                    var results = new List<ValidationResult>
                        {
                            new ValidationResult(errorMessage, new string[] { fieldName })
                        };
                    validator.ToggleMessage(results, true);
                }
            }
        }

        /// <summary>
        /// 设置指定字段错误信息
        /// </summary>
        /// <param name="propertyName">字段名，可以使用多层，如 a.b.c</param>
        /// <param name="errorMessage">错误描述信息，可为空，为空时查找资源文件</param>
        public void SetError(string propertyName, string errorMessage)
        {
            if (TryGetModelField(propertyName, out var modelType, out var fieldName))
            {
                var validator = GetValidator(modelType, fieldName);
                if (validator != null)
                {
                    var results = new List<ValidationResult>
                    {
                        new ValidationResult(errorMessage, new string[] { fieldName })
                    };
                    validator.ToggleMessage(results, true);
                }
            }
        }

        private bool TryGetModelField(string propertyName, [MaybeNullWhen(false)] out Type modelType, [MaybeNullWhen(false)] out string fieldName)
        {
            var propNames = new ConcurrentQueue<string>(propertyName.Split('.'));
            var modelTypeInfo = Model.GetType();
            modelType = null;
            fieldName = null;
            while (propNames.TryDequeue(out var propName))
            {
                modelType = modelTypeInfo;
                fieldName = propName;
                var propertyInfo = modelType.GetProperty(propName);
                if (propertyInfo == null)
                {
                    break;
                }
                var exp = Expression.Parameter(modelTypeInfo);
                var member = Expression.Property(exp, propertyInfo);
                modelTypeInfo = member.Type;
            }
            return propNames.IsEmpty;
        }

        private IValidateComponent GetValidator(Type modelType, string fieldName) => ValidatorCache.FirstOrDefault(c => c.Key.Model.GetType() == modelType && c.Key.FieldName == fieldName).Value;

        private static bool IsPublic(PropertyInfo p) => p.GetMethod != null && p.SetMethod != null && p.GetMethod.IsPublic && p.SetMethod.IsPublic;

        /// <summary>
        /// EditModel 数据模型验证方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="results"></param>
        internal void ValidateObject(ValidationContext context, List<ValidationResult> results)
        {
            if (ValidateAllProperties)
            {
                ValidateProperty(context, results);
            }
            else
            {
                // 遍历所有可验证组件进行数据验证
                foreach (var key in ValidatorCache.Keys.Where(k => k.Model == context.ObjectInstance))
                {
                    // 验证 DataAnnotations
                    var validator = ValidatorCache[key];
                    if (!validator.IsDisabled && !validator.SkipValidate)
                    {
                        var messages = new List<ValidationResult>();
                        var propertyValidateContext = new ValidationContext(key.Model)
                        {
                            MemberName = key.FieldName,
                            DisplayName = key.GetDisplayName()
                        };
                        var pi = key.Model.GetType().GetProperty(key.FieldName);
                        if (pi != null)
                        {
                            // 单独处理 Upload 组件
                            if (validator is IUpload uploader)
                            {
                                // 处理多个上传文件
                                uploader.UploadFiles.ForEach(file =>
                                {
                                    ValidateDataAnnotations(file.File, propertyValidateContext, messages, pi, file.ValidateId);
                                });
                            }
                            else
                            {
                                // 设置其关联属性字段
                                var propertyValue = LambdaExtensions.GetPropertyValue(key.Model, key.FieldName);

                                ValidateDataAnnotations(propertyValue, propertyValidateContext, messages, pi);

                                if (messages.Count == 0)
                                {
                                    // 自定义验证组件
                                    validator.ValidateProperty(propertyValue, propertyValidateContext, messages);
                                }
                            }
                        }
                        // 客户端提示
                        validator.ToggleMessage(messages, false);
                        results.AddRange(messages);
                    }
                }
            }
        }

        /// <summary>
        /// 通过表单内绑定的字段验证方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="results"></param>
        /// <param name="fieldIdentifier"></param>
        internal void ValidateField(ValidationContext context, List<ValidationResult> results, in FieldIdentifier fieldIdentifier)
        {
            if (ValidatorCache.TryGetValue(fieldIdentifier, out var validator) && !validator.IsDisabled && !validator.SkipValidate)
            {
                var pi = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName);

                if (pi != null)
                {
                    // 单独处理 Upload 组件
                    if (validator is IUpload uploader)
                    {
                        // 处理多个上传文件
                        uploader.UploadFiles.ForEach(file =>
                        {
                            ValidateDataAnnotations(file.File, context, results, pi, file.ValidateId);
                        });
                    }
                    else
                    {
                        var propertyValue = LambdaExtensions.GetPropertyValue(fieldIdentifier.Model, fieldIdentifier.FieldName);

                        // 验证 DataAnnotations
                        ValidateDataAnnotations(propertyValue, context, results, pi);

                        if (results.Count == 0)
                        {
                            // 自定义验证组件
                            validator.ValidateProperty(propertyValue, context, results);
                        }
                    }
                }

                // 客户端提示
                validator.ToggleMessage(results, true);
            }
        }

        /// <summary>
        /// 通过属性设置的 DataAnnotation 进行数据验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="memberName"></param>
        private void ValidateDataAnnotations(object? value, ValidationContext context, ICollection<ValidationResult> results, PropertyInfo propertyInfo, string? memberName = null)
        {
            var rules = propertyInfo.GetCustomAttributes(true).Where(i => i.GetType().IsSubclassOf(typeof(ValidationAttribute))).Cast<ValidationAttribute>();
            var displayName = context.DisplayName;
            memberName ??= propertyInfo.Name;
            var attributeSpan = "Attribute".AsSpan();
            foreach (var rule in rules)
            {
                var result = rule.GetValidationResult(value, context);
                if (result != null && result != ValidationResult.Success)
                {
                    // 查找 resx 资源文件中的 ErrorMessage
                    var ruleNameSpan = rule.GetType().Name.AsSpan();
                    var index = ruleNameSpan.IndexOf(attributeSpan, StringComparison.OrdinalIgnoreCase);
                    var ruleName = rule.GetType().Name.AsSpan().Slice(0, index);
                    var isResx = false;
                    rule.ErrorMessage = result.ErrorMessage;
                    if (!string.IsNullOrEmpty(rule.ErrorMessage))
                    {
                        var resxType = Options.Value.ResourceManagerStringLocalizerType;
                        if (resxType != null
                            && JsonStringLocalizerFactory.TryGetLocalizerString(
                                localizer: LocalizerFactory.Create(resxType),
                                key: rule.ErrorMessage, out var resx))
                        {
                            rule.ErrorMessage = resx;
                            isResx = true;
                        }
                    }
                    if (!isResx)
                    {
                        if (JsonStringLocalizerFactory.TryGetLocalizerString(
                            localizer: LocalizerFactory.Create(rule.GetType()),
                            key: nameof(rule.ErrorMessage), out var msg))
                        {
                            rule.ErrorMessage = msg;
                        }

                        if (JsonStringLocalizerFactory.TryGetLocalizerString(
                            localizer: LocalizerFactory.Create(context.ObjectType),
                            key: $"{memberName}.{ruleName.ToString()}", out msg))
                        {
                            rule.ErrorMessage = msg;
                        }
                    }

                    var errorMessage = rule.FormatErrorMessage(displayName ?? memberName);
                    results.Add(new ValidationResult(errorMessage, new string[] { memberName }));
                }
            }
        }

        /// <summary>
        /// 验证整个模型时验证属性方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="results"></param>
        private void ValidateProperty(ValidationContext context, List<ValidationResult> results)
        {
            var properties = context.ObjectType.GetRuntimeProperties().Where(p => IsPublic(p) && !p.GetIndexParameters().Any());
            foreach (var pi in properties)
            {
                // 设置其关联属性字段
                var propertyValue = LambdaExtensions.GetPropertyValue(context.ObjectInstance, pi.Name);

                // 检查当前值是否为 Class
                if (propertyValue != null && propertyValue is not string && propertyValue.GetType().IsClass)
                {
                    var fieldContext = new ValidationContext(propertyValue)
                    {
                        MemberName = pi.Name
                    };
                    ValidateProperty(fieldContext, results);
                }
                else
                {
                    // 验证 DataAnnotations
                    var messages = new List<ValidationResult>();
                    var fieldIdentifier = new FieldIdentifier(context.ObjectInstance, pi.Name);
                    context.DisplayName = fieldIdentifier.GetDisplayName();

                    if (ValidatorCache.TryGetValue(fieldIdentifier, out var validator) && !validator.IsDisabled && !validator.SkipValidate)
                    {
                        // 单独处理 Upload 组件
                        if (validator is IUpload uploader)
                        {
                            // 处理多个上传文件
                            uploader.UploadFiles.ForEach(file =>
                            {
                                ValidateDataAnnotations(file.File, context, messages, pi, file.ValidateId);
                            });
                        }
                        else
                        {
                            ValidateDataAnnotations(propertyValue, context, messages, pi);
                            if (messages.Count == 0)
                            {
                                // 自定义验证组件
                                validator.ValidateProperty(propertyValue, context, messages);
                            }
                        }
                        // 客户端提示
                        validator.ToggleMessage(messages, true);
                    }
                    results.AddRange(messages);
                }
            }
        }

        private List<Button> AsyncSubmitButtons { get; set; } = new List<Button>();

        /// <summary>
        /// 注册提交按钮
        /// </summary>
        /// <param name="button"></param>
        internal void RegisterAsyncSubmitButton(Button button)
        {
            AsyncSubmitButtons.Add(button);
        }

        private async Task OnValidSubmitForm(EditContext context)
        {
            if (OnValidSubmit != null)
            {
                foreach (var b in AsyncSubmitButtons)
                {
                    b.TriggerAsync(true);
                }
                await OnValidSubmit(context);
                foreach (var b in AsyncSubmitButtons)
                {
                    b.TriggerAsync(false);
                }
            }
        }
    }
}
