// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// EditContextDataAnotation 扩展操作类
    /// </summary>
    internal static class BootstrapBlazorEditContextDataAnnotationsExtensions
    {
        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), Func<object, object>> PropertyValueInvokerCache = new ConcurrentDictionary<(Type, string), Func<object, object>>();

        /// <summary>
        /// 添加数据合规检查
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/>.</param>
        /// <param name="editForm"></param>
        public static EditContext AddEditContextDataAnnotationsValidation(this EditContext editContext, ValidateFormBase editForm)
        {
            if (editContext == null)
            {
                throw new ArgumentNullException(nameof(editContext));
            }

            var messages = new ValidationMessageStore(editContext);

            editContext.OnValidationRequested +=
                (sender, eventArgs) => ValidateModel(sender as EditContext, messages, editForm);

            editContext.OnFieldChanged +=
                (sender, eventArgs) => ValidateField(editContext, messages, eventArgs.FieldIdentifier, editForm);

            return editContext;
        }

        private static void ValidateModel(EditContext? editContext, ValidationMessageStore messages, ValidateFormBase editForm)
        {
            if (editContext != null)
            {
                var validationContext = new ValidationContext(editContext.Model);
                var validationResults = new List<ValidationResult>();

                TryValidateObject(editContext.Model, validationContext, validationResults);
                editForm.ValidateObject(editContext.Model, validationContext, validationResults);

                messages.Clear();

                foreach (var validationResult in validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)))
                {
                    if (!validationResult.MemberNames.Any())
                    {
                        messages.Add(new FieldIdentifier(editContext.Model, fieldName: string.Empty), validationResult.ErrorMessage!);
                        continue;
                    }

                    foreach (var memberName in validationResult.MemberNames)
                    {
                        messages.Add(editContext.Field(memberName), validationResult.ErrorMessage!);
                    }
                }
                editContext.NotifyValidationStateChanged();
            }
        }

        private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier, ValidateFormBase editForm)
        {
            // 获取验证消息
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(fieldIdentifier.Model)
            {
                MemberName = fieldIdentifier.FieldName,
                DisplayName = fieldIdentifier.GetDisplayName()
            };

            var propertyValue = fieldIdentifier.GetPropertyValue();
            TryValidateProperty(propertyValue, validationContext, results);
            editForm.ValidateProperty(propertyValue, validationContext, results);

            messages.Clear(fieldIdentifier);
            messages.Add(fieldIdentifier, results.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)).Select(result => result.ErrorMessage!));

            editContext.NotifyValidationStateChanged();
        }

        /// <summary>
        /// 获取 FieldIdentifier 属性值
        /// </summary>
        /// <param name="fieldIdentifier"></param>
        /// <returns></returns>
        internal static object GetPropertyValue(this in FieldIdentifier fieldIdentifier)
        {
            var cacheKey = (fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
            var model = fieldIdentifier.Model;
            var invoker = PropertyValueInvokerCache.GetOrAdd(cacheKey, key => model.GetPropertyValueLambda<object, object>(key.FieldName).Compile());

            return invoker.Invoke(model);
        }

        private static void TryValidateObject(object model, ValidationContext context, ICollection<ValidationResult> results)
        {
            var modelType = model.GetType();
            foreach (var p in modelType.GetProperties())
            {
                var fieldIdentifier = new FieldIdentifier(model, fieldName: p.Name);
                var propertyValue = fieldIdentifier.GetPropertyValue();
                TryValidateProperty(propertyValue, context, results, p);
            }
        }

        private static void TryValidateProperty(object value, ValidationContext context, ICollection<ValidationResult> results, PropertyInfo? propertyInfo = null)
        {
            var modelType = context.ObjectType;
            if (propertyInfo == null)
            {
                propertyInfo = modelType.GetProperty(context.MemberName!);
            }

            if (propertyInfo != null)
            {
                var rules = propertyInfo.GetCustomAttributes(true).Where(i => i.GetType().BaseType == typeof(ValidationAttribute)).Cast<ValidationAttribute>();
                var displayName = new FieldIdentifier(context.ObjectInstance, propertyInfo.Name).GetDisplayName();
                var memberName = propertyInfo.Name;
                var attributeSpan = "Attribute".AsSpan();
                foreach (var rule in rules)
                {
                    if (!rule.IsValid(value))
                    {
                        if (!string.IsNullOrEmpty(displayName) && TryGetLocalizer(context.ObjectType, displayName, out var d))
                        {
                            displayName = d;
                        }
                        else if (TryGetLocalizer(context.ObjectType, $"{memberName}.Display", out var dn))
                        {
                            displayName = dn;
                        }
                        var ruleNameSpan = rule.GetType().Name.AsSpan();
                        var index = ruleNameSpan.IndexOf(attributeSpan, StringComparison.OrdinalIgnoreCase);
                        var ruleName = rule.GetType().Name.AsSpan().Slice(0, index);
                        if (!string.IsNullOrEmpty(rule.ErrorMessage) && TryGetLocalizer(context.ObjectType, rule.ErrorMessage, out var resx))
                        {
                            rule.ErrorMessage = resx;
                        }
                        else if (TryGetLocalizer(context.ObjectType, $"{memberName}.{ruleName.ToString()}", out var msg))
                        {
                            rule.ErrorMessage = msg;
                        }
                        var errorMessage = rule.FormatErrorMessage(displayName ?? memberName);
                        results.Add(new ValidationResult(errorMessage, new string[] { memberName }));
                    }
                }
            }
        }

        private static bool TryGetLocalizer(Type type, string key, [MaybeNullWhen(false)] out string? text)
        {
            var localizer = JsonStringLocalizerFactory.CreateLocalizer(type);
            var l = localizer[key];
            text = !l.ResourceNotFound ? l.Value : null;
            return !l.ResourceNotFound;
        }
    }
}
