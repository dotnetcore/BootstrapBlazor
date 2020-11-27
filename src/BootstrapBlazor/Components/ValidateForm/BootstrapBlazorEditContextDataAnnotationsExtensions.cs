// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
                Validator.TryValidateObject(editContext.Model, validationContext, validationResults, true);
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
            Validator.TryValidateProperty(propertyValue, validationContext, results);
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
    }
}
