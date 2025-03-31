// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

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
    public Func<EditContext, Task>? OnValidSubmit { get; set; }

    /// <summary>
    /// A callback that will be invoked when the form is submitted and the
    /// <see cref="EditContext"/> is determined to be invalid.
    /// </summary>
    [Parameter]
    public Func<EditContext, Task>? OnInvalidSubmit { get; set; }

    /// <summary>
    /// A callback that will be invoked when the field's value has been changed
    /// </summary>
    [Parameter]
    [NotNull]
    public Action<string, object?>? OnFieldValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否显示所有验证失败字段的提示信息 默认 false 仅显示第一个验证失败字段的提示信息
    /// </summary>
    [Parameter]
    public bool ShowAllInvalidResult { get; set; }

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
    /// 获得/设置 是否显示验证表单内的 Label 默认为 null
    /// </summary>
    [Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置
    /// </summary>
    [Parameter]
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// 获得/设置 标签宽度 默认 null 未设置使用全局设置 <code>--bb-row-label-width</code> 值
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }

    [Inject]
    [NotNull]
    private IOptions<JsonLocalizationOptions>? Options { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizerFactory? LocalizerFactory { get; set; }

    /// <summary>
    /// 验证组件缓存
    /// </summary>
    private readonly ConcurrentDictionary<(string FieldName, Type ModelType), (FieldIdentifier FieldIdentifier, IValidateComponent ValidateComponent)> _validatorCache = new();

    /// <summary>
    /// 验证组件验证结果缓存
    /// </summary>
    private readonly ConcurrentDictionary<IValidateComponent, List<ValidationResult>> _validateResults = new();

    private string? DisableAutoSubmitString => (DisableAutoSubmitFormByEnter.HasValue && DisableAutoSubmitFormByEnter.Value) ? "true" : null;

    /// <summary>
    /// 验证合法成员集合
    /// </summary>
    internal List<string> ValidMemberNames { get; } = [];

    /// <summary>
    /// 验证非法成员集合
    /// </summary>
    internal List<ValidationResult> InvalidMemberNames { get; } = [];

    private string? ShowAllInvalidResultString => ShowAllInvalidResult ? "true" : null;

    private string? StyleString => CssBuilder.Default()
        .AddStyle("--bb-row-label-width", $"{LabelWidth}px", LabelWidth.HasValue)
        .Build();

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!DisableAutoSubmitFormByEnter.HasValue && BootstrapBlazorOptions.CurrentValue.DisableAutoSubmitFormByEnter.HasValue)
        {
            DisableAutoSubmitFormByEnter = BootstrapBlazorOptions.CurrentValue.DisableAutoSubmitFormByEnter.Value;
        }
    }

    /// <summary>
    /// 添加数据验证组件到 EditForm 中
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    internal void AddValidator((string FieldName, Type ModelType) key, (FieldIdentifier FieldIdentifier, IValidateComponent IValidateComponent) value)
    {
        _validatorCache.TryAdd(key, value);
    }

    /// <summary>
    /// 移除数据验证组件到 EditForm 中
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    internal bool TryRemoveValidator((string FieldName, Type ModelType) key, out (FieldIdentifier FieldIdentifier, IValidateComponent IValidateComponent) value) => _validatorCache.TryRemove(key, out value);

    /// <summary>
    /// 设置指定字段错误信息
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="errorMessage">错误描述信息，可为空，为空时查找资源文件</param>
    public void SetError<TModel>(Expression<Func<TModel, object?>> expression, string errorMessage)
    {
        switch (expression.Body)
        {
            case UnaryExpression { Operand: MemberExpression mem }:
                InternalSetError(mem, errorMessage);
                break;
            case MemberExpression exp:
                InternalSetError(exp, errorMessage);
                break;
        }
    }

    private void InternalSetError(MemberExpression exp, string errorMessage)
    {
        if (exp.Expression != null)
        {
            var fieldName = exp.Member.Name;
            var modelType = exp.Expression.Type;
            var validator = _validatorCache.FirstOrDefault(c => c.Key.ModelType == modelType && c.Key.FieldName == fieldName).Value.ValidateComponent;
            if (validator == null)
            {
                return;
            }

            var results = new List<ValidationResult>
            {
                new(errorMessage, [fieldName])
            };
            validator.ToggleMessage(results);
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
                new(errorMessage, [fieldName])
            };
            validator.ToggleMessage(results);
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

    private bool TryGetValidator(Type modelType, string fieldName, out IValidateComponent validator)
    {
        validator = _validatorCache.FirstOrDefault(c => c.Key.ModelType == modelType && c.Key.FieldName == fieldName).Value.ValidateComponent;
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
        _validateResults.Clear();

        if (ValidateAllProperties)
        {
            await ValidateProperty(context, results);
        }
        else
        {
            // 遍历所有可验证组件进行数据验证
            foreach (var key in _validatorCache.Keys)
            {
                // 验证 DataAnnotations
                var (fieldIdentifier, validator) = _validatorCache[key];
                if (!validator.IsNeedValidate)
                {
                    continue;
                }

                var messages = new List<ValidationResult>();
                var pi = key.ModelType.GetPropertyByName(key.FieldName);
                if (pi != null)
                {
                    var propertyValidateContext = new ValidationContext(fieldIdentifier.Model, context, null)
                    {
                        MemberName = fieldIdentifier.FieldName,
                        DisplayName = fieldIdentifier.GetDisplayName()
                    };

                    // 设置其关联属性字段
                    var propertyValue = Utility.GetPropertyValue(fieldIdentifier.Model, fieldIdentifier.FieldName);

                    await ValidateAsync(validator, propertyValidateContext, messages, pi, propertyValue);
                }
                _validateResults.TryAdd(validator, messages);
                results.AddRange(messages);
            }

            // 验证 IValidatableObject
            if (results.Count == 0)
            {
                IValidatableObject? validate;
                if (context.ObjectInstance is IValidatableObject v)
                {
                    validate = v;
                }
                else
                {
                    validate = context.GetInstanceFromMetadataType<IValidatableObject>();
                }
                if (validate != null)
                {
                    var messages = validate.Validate(context).ToList();
                    if (messages.Count > 0)
                    {
                        foreach (var key in _validatorCache.Keys)
                        {
                            var validatorValue = _validatorCache[key];
                            var validator = validatorValue.ValidateComponent;
                            if (validator.IsNeedValidate)
                            {
                                _validateResults[validator].AddRange(messages);
                            }
                        }
                        results.AddRange(messages);
                    }
                }
            }

            ValidMemberNames.RemoveAll(name => _validateResults.Values.SelectMany(i => i).Any(i => i.MemberNames.Contains(name)));
            foreach (var (validator, messages) in _validateResults)
            {
                validator.ToggleMessage(messages);
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
        if (!string.IsNullOrEmpty(context.MemberName) && _validatorCache.TryGetValue((context.MemberName, context.ObjectType), out var v))
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
                validator.ToggleMessage(results);
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
    private void ValidateDataAnnotations(object? value, ValidationContext context, List<ValidationResult> results, PropertyInfo propertyInfo, string? memberName = null)
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
                var find = false;
                if (!string.IsNullOrEmpty(rule.ErrorMessage))
                {
                    var resourceType = Options.Value.ResourceManagerStringLocalizerType;
                    if (resourceType != null && LocalizerFactory.Create(resourceType).TryGetLocalizerString(rule.ErrorMessage, out var text))
                    {
                        rule.ErrorMessage = text;
                        find = true;
                    }
                }

                if (!context.ObjectType.Assembly.IsDynamic)
                {
                    if (!find && !string.IsNullOrEmpty(rule.ErrorMessage)
                        && LocalizerFactory.Create(context.ObjectType).TryGetLocalizerString(rule.ErrorMessage, out var msg))
                    {
                        // 通过设置 ErrorMessage 检索
                        rule.ErrorMessage = msg;
                        find = true;
                    }

                    if (!find && LocalizerFactory.Create(rule.GetType()).TryGetLocalizerString(nameof(rule.ErrorMessage), out msg))
                    {
                        // 通过 Attribute 检索
                        rule.ErrorMessage = msg;
                        find = true;
                    }

                    if (!find)
                    {
                        // 通过 字段.规则名称 检索
                        // 查找 resource 资源文件中的 ErrorMessage
                        var ruleNameSpan = rule.GetType().Name.AsSpan();
                        var index = ruleNameSpan.IndexOf(attributeSpan, StringComparison.OrdinalIgnoreCase);
                        var ruleName = index == -1 ? ruleNameSpan[..] : ruleNameSpan[..index];
                        if (LocalizerFactory.Create(context.ObjectType).TryGetLocalizerString($"{memberName}.{ruleName.ToString()}", out msg))
                        {
                            rule.ErrorMessage = msg;
                            find = true;
                        }
                    }
                }

                if (!find)
                {
                    rule.ErrorMessage = result.ErrorMessage;
                }
                var errorMessage = !string.IsNullOrEmpty(rule.ErrorMessage) && rule.ErrorMessage.Contains("{0}")
                    ? rule.FormatErrorMessage(displayName)
                    : rule.ErrorMessage;
                results.Add(new ValidationResult(errorMessage, [memberName]));
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
        var properties = context.ObjectType.GetRuntimeProperties().Where(p => IsPublic(p) && p.IsCanWrite() && p.GetIndexParameters().Length == 0);
        foreach (var pi in properties)
        {
            // 设置其关联属性字段
            var propertyValue = Utility.GetPropertyValue(context.ObjectInstance, pi.Name);
            var fieldIdentifier = new FieldIdentifier(context.ObjectInstance, pi.Name);
            context.DisplayName = fieldIdentifier.GetDisplayName();
            context.MemberName = fieldIdentifier.FieldName;

            if (_validatorCache.TryGetValue((fieldIdentifier.FieldName, fieldIdentifier.Model.GetType()), out var v))
            {
                var validator = v.ValidateComponent;

                // 检查当前值是否为 Class 即复杂类型 x.y.z 形式的属性值
                if (validator.IsComplexValue(propertyValue) && propertyValue != null)
                {
                    var fieldContext = new ValidationContext(propertyValue, context, null);
                    await ValidateProperty(fieldContext, results);
                }
                else
                {
                    // 验证 DataAnnotations
                    var messages = new List<ValidationResult>();
                    if (validator.IsNeedValidate)
                    {
                        // 组件进行验证
                        await ValidateAsync(validator, context, messages, pi, propertyValue);

                        // 客户端提示
                        validator.ToggleMessage(messages);
                    }
                    results.AddRange(messages);
                }
            }
            else
            {
                var messages = new List<ValidationResult>();
                ValidateDataAnnotations(propertyValue, context, messages, pi);
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
                ValidateDataAnnotations(propertyValue, context, messages, pi);
            }
        }
        else
        {
            ValidateDataAnnotations(propertyValue, context, messages, pi);
            if (messages.Count == 0)
            {
                // 自定义验证组件
                _tcs = new TaskCompletionSource<bool>();
                await validator.ValidatePropertyAsync(propertyValue, context, messages);
                _tcs.TrySetResult(messages.Count == 0);
            }

            if (messages.Count == 0)
            {
                // 联动字段验证 IValidateCollection
                IValidateCollection? validate;
                if (context.ObjectInstance is IValidateCollection v)
                {
                    validate = v;
                }
                else
                {
                    validate = context.GetInstanceFromMetadataType<IValidateCollection>();
                }
                if (validate != null)
                {
                    messages.AddRange(validate.Validate(context));
                    ValidMemberNames.AddRange(validate.GetValidMemberNames());
                    InvalidMemberNames.AddRange(validate.GetInvalidMemberNames());
                }
            }
        }

        _invalid = messages.Count > 0;
    }

    private bool _invalid = false;

    private List<ButtonBase> AsyncSubmitButtons { get; } = [];

    /// <summary>
    /// 注册提交按钮
    /// </summary>
    /// <param name="button"></param>
    internal void RegisterAsyncSubmitButton(ButtonBase button)
    {
        AsyncSubmitButtons.Add(button);
    }

    private TaskCompletionSource<bool>? _tcs;

    private async Task OnValidSubmitForm(EditContext context)
    {
        var isAsync = AsyncSubmitButtons.Count > 0;
        foreach (var b in AsyncSubmitButtons)
        {
            b.TriggerAsync(true);
        }
        if (isAsync)
        {
            await Task.Yield();
        }

        var valid = true;
        // 由于可能有异步验证，需要等待异步验证结束
        if (_tcs != null)
        {
            valid = await _tcs.Task;
        }
        if (valid)
        {
            if (OnValidSubmit != null)
            {
                await OnValidSubmit(context);
            }
        }
        else
        {
            if (OnInvalidSubmit != null)
            {
                await OnInvalidSubmit(context);
            }
        }

        foreach (var b in AsyncSubmitButtons)
        {
            b.TriggerAsync(false);
        }
    }

    private async Task OnInvalidSubmitForm(EditContext context)
    {
        var isAsync = AsyncSubmitButtons.Count > 0;
        foreach (var b in AsyncSubmitButtons)
        {
            b.TriggerAsync(true);
        }
        if (isAsync)
        {
            await Task.Yield();
        }
        if (OnInvalidSubmit != null)
        {
            await OnInvalidSubmit(context);
        }
        foreach (var b in AsyncSubmitButtons)
        {
            b.TriggerAsync(false);
        }
    }

    [NotNull]
    private BootstrapBlazorDataAnnotationsValidator? Validator { get; set; }

    /// <summary>
    /// 验证方法 用于代码调用触发表单验证
    /// </summary>
    /// <returns></returns>
    public bool Validate()
    {
        _invalid = true;
        return Validator.Validate() && !_invalid;
    }

    /// <summary>
    /// 通知属性改变方法
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <param name="value"></param>
    public void NotifyFieldChanged(in FieldIdentifier fieldIdentifier, object? value)
    {
        ValueChangedFields.AddOrUpdate(fieldIdentifier, key => value, (key, v) => value);
        OnFieldValueChanged?.Invoke(fieldIdentifier.FieldName, value);
    }

    /// <summary>
    /// 获取 当前表单值改变的属性集合
    /// </summary>
    /// <returns></returns>
    public ConcurrentDictionary<FieldIdentifier, object?> ValueChangedFields { get; } = new();

    /// <summary>
    /// 获取 当前表单值改变的属性集合
    /// </summary>
    /// <returns></returns>
    [Obsolete("已弃用，单词拼写错误，请使用 ValueChangedFields，Deprecated Please use ValueChangedFields instead. wrong typo")]
    [ExcludeFromCodeCoverage]
    public ConcurrentDictionary<FieldIdentifier, object?> ValueChagnedFields { get; } = new();
}
