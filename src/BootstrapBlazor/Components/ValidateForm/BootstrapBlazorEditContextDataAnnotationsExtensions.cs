using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// EditContextDataAnotation 扩展操作类
    /// </summary>
    internal static class BootstrapBlazorEditContextDataAnnotationsExtensions
    {
        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> _propertyInfoCache = new ConcurrentDictionary<(Type, string), PropertyInfo>();

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

                foreach (var validationResult in validationResults)
                {
                    if (!validationResult.MemberNames.Any())
                    {
                        messages.Add(new FieldIdentifier(editContext.Model, fieldName: string.Empty), validationResult.ErrorMessage);
                        continue;
                    }

                    foreach (var memberName in validationResult.MemberNames)
                    {
                        messages.Add(editContext.Field(memberName), validationResult.ErrorMessage);
                    }
                }
                editContext.NotifyValidationStateChanged();
            }
        }

        private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier, ValidateFormBase editForm)
        {
            // 获取验证消息
            if (TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
            {
                var results = new List<ValidationResult>();
                var validationContext = new ValidationContext(fieldIdentifier.Model)
                {
                    MemberName = propertyInfo.Name
                };
                var message = editContext.GetValidationMessages(fieldIdentifier);
                if (message.Any())
                {
                    var msg = message.First();
                    results.Add(new ValidationResult(msg, new string[] { validationContext.MemberName }));

                    // 通知表单验证信息
                    editForm.ValidateProperty(null, validationContext, results);

                    messages.Clear(fieldIdentifier);
                    messages.Add(fieldIdentifier, msg);
                }
                else
                {
                    var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);

                    Validator.TryValidateProperty(propertyValue, validationContext, results);
                    editForm.ValidateProperty(propertyValue, validationContext, results);

                    messages.Clear(fieldIdentifier);
                    messages.Add(fieldIdentifier, results.Select(result => result.ErrorMessage));
                }
                editContext.NotifyValidationStateChanged();
            }
        }

#nullable disable
        internal static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, out PropertyInfo propertyInfo)
        {
            var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
            if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
            {
                // Validator.TryValidateProperty 只能对 Public 属性生效
                propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

                _propertyInfoCache[cacheKey] = propertyInfo;
            }

            return propertyInfo != null;
        }
#nullable restore
    }
}
