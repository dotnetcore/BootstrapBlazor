// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// ValidateForm 组件类
/// </summary>
public partial class ValidateForm : IAsyncDisposable
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
    /// A callback that will be invoked when the field's value has been changed
    /// </summary>
    [Parameter]
    [NotNull]
    public Action<string, object?>? OnFieldValueChanged { get; set; }

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
    /// 获得/设置 是否显示验证表单内的 Label 默认为 显示
    /// </summary>
    [Parameter]
    public bool ShowLabel { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    [Inject]
    [NotNull]
    private IOptions<JsonLocalizationOptions>? Options { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizerFactory? LocalizerFactory { get; set; }

    /// <summary>
    /// 验证组件缓存
    /// </summary>
    private ConcurrentDictionary<(string FieldName, Type ModelType), (FieldIdentifier FieldIdentifier, IValidateComponent ValidateComponent)> ValidatorCache { get; } = new();

    /// <summary>
    /// 添加数据验证组件到 EditForm 中
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    internal void AddValidator((string FieldName, Type ModelType) key, (FieldIdentifier FieldIdentifier, IValidateComponent IValidateComponent) value)
    {
        ValidatorCache.TryAdd(key, value);
    }

    /// <summary>
    /// 移除数据验证组件到 EditForm 中
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    internal bool TryRemoveValidator((string FieldName, Type ModelType) key, [MaybeNullWhen(false)] out (FieldIdentifier FieldIdentifier, IValidateComponent IValidateComponent) value) => ValidatorCache.TryRemove(key, out value);

    /// <summary>
    /// 设置指定字段错误信息
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="errorMessage">错误描述信息，可为空，为空时查找资源文件</param>
    public void SetError<TModel>(Expression<Func<TModel, object?>> expression, string errorMessage)
    {
        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression mem)
        {
            InternalSetError(mem, errorMessage);
        }
        else if (expression.Body is MemberExpression exp)
        {
            InternalSetError(exp, errorMessage);
        }
    }

    private void InternalSetError(MemberExpression exp, string errorMessage)
    {
        var fieldName = exp.Member.Name;
        if (exp.Expression != null)
        {
            var modelType = exp.Expression.Type;
            var validator = ValidatorCache.FirstOrDefault(c => c.Key.ModelType == modelType && c.Key.FieldName == fieldName).Value.ValidateComponent;
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
        if (TryGetModelField(propertyName, out var modelType, out var fieldName) && TryGetValidator(modelType, fieldName, out var validator))
        {
            var results = new List<ValidationResult>
            {
                new ValidationResult(errorMessage, new string[] { fieldName })
            };
            validator.ToggleMessage(results, true);
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
            var propertyInfo = modelType.GetPropertyByName(propName);
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

    private bool TryGetValidator(Type modelType, string fieldName, [NotNullWhen(true)] out IValidateComponent validator)
    {
        validator = ValidatorCache.FirstOrDefault(c => c.Key.ModelType == modelType && c.Key.FieldName == fieldName).Value.ValidateComponent;
        return validator != null;
    }

    private static bool IsPublic(PropertyInfo p) => p.GetMethod != null && p.SetMethod != null && p.GetMethod.IsPublic && p.SetMethod.IsPublic;

    /// <summary>
    /// EditModel 数据模型验证方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="results"></param>
    internal async Task ValidateObject(ValidationContext context, List<ValidationResult> results)
    {
        if (ValidateAllProperties)
        {
            await ValidateProperty(context, results);
        }
        else
        {
            // 遍历所有可验证组件进行数据验证
            foreach (var key in ValidatorCache.Keys)
            {
                // 验证 DataAnnotations
                var validatorValue = ValidatorCache[key];
                var validator = validatorValue.ValidateComponent;
                var fieldIdentifier = validatorValue.FieldIdentifier;
                if (validator.IsNeedValidate)
                {
                    var messages = new List<ValidationResult>();
                    var pi = key.ModelType.GetPropertyByName(key.FieldName);
                    if (pi != null)
                    {
                        var propertyValidateContext = new ValidationContext(fieldIdentifier.Model)
                        {
                            MemberName = fieldIdentifier.FieldName,
                            DisplayName = fieldIdentifier.GetDisplayName()
                        };

                        // 设置其关联属性字段
                        var propertyValue = Utility.GetPropertyValue(fieldIdentifier.Model, fieldIdentifier.FieldName);

                        await ValidateAsync(validator, propertyValidateContext, messages, pi, propertyValue);
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
    internal async Task ValidateFieldAsync(ValidationContext context, List<ValidationResult> results)
    {
        if (!string.IsNullOrEmpty(context.MemberName) && ValidatorCache.TryGetValue((context.MemberName, context.ObjectType), out var v))
        {
            var validator = v.ValidateComponent;
            if (validator.IsNeedValidate)
            {
                var pi = context.ObjectType.GetPropertyByName(context.MemberName);
                if (pi != null)
                {
                    var propertyValue = Utility.GetPropertyValue(context.ObjectInstance, context.MemberName);
                    await ValidateAsync(validator, context, results, pi, propertyValue);
                }

                // 客户端提示
                validator.ToggleMessage(results, true);
            }
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
        var rules = propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>();
        var metadataType = context.ObjectType.GetCustomAttribute<MetadataTypeAttribute>(false);
        if (metadataType != null)
        {
            var p = metadataType.MetadataClassType.GetPropertyByName(propertyInfo.Name);
            if (p != null)
            {
                rules = rules.Concat(p.GetCustomAttributes(true).OfType<ValidationAttribute>());
            }
        }
        var displayName = context.DisplayName;
        memberName ??= propertyInfo.Name;
        var attributeSpan = nameof(Attribute).AsSpan();
        foreach (var rule in rules)
        {
            var result = rule.GetValidationResult(value, context);
            if (result != null && result != ValidationResult.Success)
            {
                // 查找 resx 资源文件中的 ErrorMessage
                var ruleNameSpan = rule.GetType().Name.AsSpan();
                var index = ruleNameSpan.IndexOf(attributeSpan, StringComparison.OrdinalIgnoreCase);
                var ruleName = ruleNameSpan[..index];
                var find = false;
                if (!string.IsNullOrEmpty(rule.ErrorMessage))
                {
                    var resxType = Options.Value.ResourceManagerStringLocalizerType;
                    if (resxType != null
                        && LocalizerFactory.Create(resxType).TryGetLocalizerString(rule.ErrorMessage, out var resx))
                    {
                        rule.ErrorMessage = resx;
                        find = true;
                    }
                }

                // 通过设置 ErrorMessage 检索
                if (!context.ObjectType.Assembly.IsDynamic && !find
                    && !string.IsNullOrEmpty(rule.ErrorMessage)
                    && LocalizerFactory.Create(context.ObjectType).TryGetLocalizerString(rule.ErrorMessage, out var msg))
                {
                    rule.ErrorMessage = msg;
                    find = true;
                }

                // 通过 Attribute 检索
                if (!rule.GetType().Assembly.IsDynamic && !find
                    && LocalizerFactory.Create(rule.GetType()).TryGetLocalizerString(nameof(rule.ErrorMessage), out msg))
                {
                    rule.ErrorMessage = msg;
                    find = true;
                }

                // 通过 字段.规则名称 检索
                if (!context.ObjectType.Assembly.IsDynamic && !find
                    && LocalizerFactory.Create(context.ObjectType).TryGetLocalizerString($"{memberName}.{ruleName.ToString()}", out msg))
                {
                    rule.ErrorMessage = msg;
                    find = true;
                }

                if (!find)
                {
                    rule.ErrorMessage = result.ErrorMessage;
                }
                var errorMessage = !string.IsNullOrEmpty(rule.ErrorMessage) && rule.ErrorMessage.Contains("{0}")
                    ? rule.FormatErrorMessage(displayName)
                    : rule.ErrorMessage;
                results.Add(new ValidationResult(errorMessage, new string[] { memberName }));
            }
        }
    }

    /// <summary>
    /// 验证整个模型时验证属性方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="results"></param>
    private async Task ValidateProperty(ValidationContext context, List<ValidationResult> results)
    {
        // 获得所有可写属性
        var properties = context.ObjectType.GetRuntimeProperties().Where(p => IsPublic(p) && p.CanWrite && !p.GetIndexParameters().Any());
        foreach (var pi in properties)
        {
            // 设置其关联属性字段
            var propertyValue = Utility.GetPropertyValue(context.ObjectInstance, pi.Name);

            // 检查当前值是否为 Class 不是 string 不是集合
            if (propertyValue != null && propertyValue is not string
                && !propertyValue.GetType().IsAssignableTo(typeof(System.Collections.IEnumerable))
                && propertyValue.GetType().IsClass)
            {
                var fieldContext = new ValidationContext(propertyValue);
                await ValidateProperty(fieldContext, results);
            }
            else
            {
                // 验证 DataAnnotations
                var messages = new List<ValidationResult>();
                var fieldIdentifier = new FieldIdentifier(context.ObjectInstance, pi.Name);
                context.DisplayName = fieldIdentifier.GetDisplayName();
                context.MemberName = fieldIdentifier.FieldName;

                if (ValidatorCache.TryGetValue((fieldIdentifier.FieldName, fieldIdentifier.Model.GetType()), out var v) && v.ValidateComponent != null)
                {
                    var validator = v.ValidateComponent;
                    if (validator.IsNeedValidate)
                    {
                        // 组件进行验证
                        await ValidateAsync(validator, context, messages, pi, propertyValue);

                        // 客户端提示
                        validator.ToggleMessage(messages, true);
                    }
                }
                results.AddRange(messages);
            }
        }
    }

    private async Task ValidateAsync(IValidateComponent validator, ValidationContext context, List<ValidationResult> messages, PropertyInfo pi, object? propertyValue)
    {
        // 单独处理 Upload 组件
        if (validator is IUpload uploader)
        {
            if (uploader.UploadFiles.Count > 0)
            {
                // 处理多个上传文件
                uploader.UploadFiles.ForEach(file =>
                {
                    // 优先检查 File 流，不需要检查 FileName
                    ValidateDataAnnotations(file.File, context, messages, pi, file.ValidateId);
                });
            }
            else
            {
                // 未选择文件
                ValidateDataAnnotations(null, context, messages, pi);
            }
        }
        else
        {
            ValidateDataAnnotations(propertyValue, context, messages, pi);
            if (messages.Count == 0)
            {
                // 自定义验证组件
                await validator.ValidatePropertyAsync(propertyValue, context, messages);
            }
        }
    }

    private List<Button> AsyncSubmitButtons { get; } = new List<Button>();

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
            var isAsync = AsyncSubmitButtons.Any();
            foreach (var b in AsyncSubmitButtons)
            {
                b.TriggerAsync(true);
            }
            if (isAsync)
            {
                await Task.Yield();
            }
            await OnValidSubmit(context);
            foreach (var b in AsyncSubmitButtons)
            {
                b.TriggerAsync(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <param name="value"></param>
    public void NotifyFieldChanged(in FieldIdentifier fieldIdentifier, object? value)
    {
        ValueChagnedFields.AddOrUpdate(fieldIdentifier, key => value, (key, v) => value);
        OnFieldValueChanged?.Invoke(fieldIdentifier.FieldName, value);
    }

    /// <summary>
    /// 获取 当前表单值改变的属性集合
    /// </summary>
    /// <returns></returns>
    public ConcurrentDictionary<FieldIdentifier, object?> ValueChagnedFields { get; } = new();

    /// <summary>
    /// DisposeAsyncCore 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            await JSRuntime.InvokeVoidAsync(Id, "bb_form");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore(true);
        GC.SuppressFinalize(this);
    }
}
