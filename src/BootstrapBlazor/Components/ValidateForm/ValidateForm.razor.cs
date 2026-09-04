// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ValidateForm 组件类</para>
/// <para lang="en">ValidateForm component</para>
/// </summary>
public partial class ValidateForm
{
    /// <summary>
    /// <para lang="zh">获得/设置 表单提交后验证合规时回调方法</para>
    /// <para lang="en">Gets or sets the callback method when form submission is validated</para>
    /// </summary>
    [Parameter]
    public Func<EditContext, Task>? OnValidSubmit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表单提交后验证不合规时回调方法</para>
    /// <para lang="en">Gets or sets the callback method when form submission is invalid</para>
    /// </summary>
    [Parameter]
    public Func<EditContext, Task>? OnInvalidSubmit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表单内绑定字段值变化时回调方法</para>
    /// <para lang="en">Gets or sets the callback method when a bound field's value has changed within the form</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Action<string, object?>? OnFieldValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示所有验证失败字段的提示信息 默认 false 仅显示第一个验证失败字段的提示信息</para>
    /// <para lang="en">Gets or sets whether to display all validation failure messages. The default is false, which only displays the first validation failure message</para>
    /// </summary>
    [Parameter]
    public bool ShowAllInvalidResult { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否验证所有字段 默认 false</para>
    /// <para lang="en">Gets or sets whether to validate all properties. The default is false</para>
    /// </summary>
    [Parameter]
    public bool ValidateAllProperties { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表单绑定模型对象</para>
    /// <para lang="en">Gets or sets the top-level model object for the form</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public object? Model { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件子内容</para>
    /// <para lang="en">Gets or sets the content to be rendered inside this component</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否获取必填项标记 默认为 true 显示</para>
    /// <para lang="en">Gets or sets whether to display the required mark. The default is true, which means the required mark is displayed</para>
    /// </summary>
    [Parameter]
    public bool ShowRequiredMark { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示验证表单内的 Label 默认为 null</para>
    /// <para lang="en">Gets or sets whether to display labels within the validation form. The default value is null</para>
    /// </summary>
    [Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null</para>
    /// <para lang="en">Gets or sets whether to display a tooltip for the label, often used when the label text is too long and gets truncated. The default is null</para>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为无表单模式 默认 false</para>
    /// <para lang="en">Gets or sets whether to use a formless mode. The default is false</para>
    /// </summary>
    /// <remarks>设置为 true 时不渲染 form 元素，仅级联 EditContext 用于 Table InCell 编辑模式</remarks>
    [Parameter]
    public bool IsFormless { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置</para>
    /// <para lang="en">Gets or sets whether to disable auto-submit form by enter key. Default is null</para>
    /// </summary>
    [Parameter]
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标签宽度 默认 null 未设置 使用全局设置 <code>--bb-row-label-width</code> 值</para>
    /// <para lang="en">Gets or sets the label width. The default is null, which means the global setting <code>--bb-row-label-width</code> is used</para>
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<JsonLocalizationOptions>? Options { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizerFactory? LocalizerFactory { get; set; }

    private readonly ConcurrentDictionary<FieldIdentifier, IValidateComponent> _validatorCache = new();

    private readonly ConcurrentDictionary<IValidateComponent, List<ValidationResult>> _validateResults = new();

    private readonly SemaphoreSlim _validationLock = new(1, 1);

    private string? DisableAutoSubmitString => IsFormless
        ? null
        : DisableAutoSubmitFormByEnter is true ? "true" : null;

    /// <summary>
    /// <para lang="zh">获得验证合法成员集合</para>
    /// <para lang="en">Gets the collection of valid member names</para>
    /// </summary>
    internal List<string> ValidMemberNames { get; } = [];

    /// <summary>
    /// <para lang="zh">获得验证非法成员集合</para>
    /// <para lang="en">Gets the collection of invalid member names</para>
    /// </summary>
    internal List<ValidationResult> InvalidMemberNames { get; } = [];

    private string? ShowAllInvalidResultString => ShowAllInvalidResult ? "true" : null;

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-row-label-width: {LabelWidth}px;", LabelWidth.HasValue)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!DisableAutoSubmitFormByEnter.HasValue && BootstrapBlazorOptions.CurrentValue.DisableAutoSubmitFormByEnter.HasValue)
        {
            DisableAutoSubmitFormByEnter = BootstrapBlazorOptions.CurrentValue.DisableAutoSubmitFormByEnter.Value;
        }

        // 无表单模式下创建/更新 EditContext
        if (IsFormless)
        {
            _formlessEditContext ??= new EditContext(Model);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// <para lang="zh">添加数据验证组件到 EditForm 中</para>
    /// <para lang="en">Adds a data validation component to the EditForm</para>
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <param name="component"></param>
    internal void AddValidator(FieldIdentifier fieldIdentifier, IValidateComponent component)
    {
        _validatorCache.TryAdd(fieldIdentifier, component);
    }

    /// <summary>
    /// <para lang="zh">移除数据验证组件到 EditForm 中</para>
    /// <para lang="en">Removes a data validation component from the EditForm</para>
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <param name="component"></param>
    internal bool TryRemoveValidator(FieldIdentifier fieldIdentifier, [MaybeNullWhen(false)] out IValidateComponent component) => _validatorCache.TryRemove(fieldIdentifier, out component);

    /// <summary>
    /// <para lang="zh">设置指定字段错误信息</para>
    /// <para lang="en">Sets the error message for the specified field</para>
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="errorMessage"><para lang="zh">错误描述信息，可为空，为空时查找资源文件</para><para lang="en">Error description info, can be empty, searches resource file when empty</para></param>
    public async Task SetError<TModel>(Expression<Func<TModel, object?>> expression, string errorMessage)
    {
        switch (expression.Body)
        {
            case UnaryExpression { Operand: MemberExpression mem }:
                await InternalSetError(mem, errorMessage);
                break;
            case MemberExpression exp:
                await InternalSetError(exp, errorMessage);
                break;
        }
    }

    private async Task InternalSetError(MemberExpression exp, string errorMessage)
    {
        if (exp.Expression != null)
        {
            var fieldName = exp.Member.Name;
            var modelType = exp.Expression.Type;
            var validator = _validatorCache.FirstOrDefault(c => c.Key.Model.GetType() == modelType && c.Key.FieldName == fieldName).Value;
            if (validator == null)
            {
                return;
            }

            var results = new List<ValidationResult>
            {
                new(errorMessage, [fieldName])
            };
            await validator.ToggleMessage(results);
        }
    }

    /// <summary>
    /// <para lang="zh">设置指定字段错误信息</para>
    /// <para lang="en">Sets the error message for the specified field</para>
    /// </summary>
    /// <param name="propertyName"><para lang="zh">字段名，可以使用多层，如 a.b.c</para><para lang="en">Field name, can be multi-level, such as a.b.c</para></param>
    /// <param name="errorMessage"><para lang="zh">错误描述信息，可为空，为空时查找资源文件</para><para lang="en">Error description info, can be empty, if empty, resource file is searched</para></param>
    public async Task SetError(string propertyName, string errorMessage)
    {
        if (TryGetModelField(propertyName, out var modelType, out var fieldName) && TryGetValidator(modelType, fieldName, out var validator))
        {
            var results = new List<ValidationResult>
            {
                new(errorMessage, [fieldName])
            };
            await validator.ToggleMessage(results);
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
        validator = _validatorCache.FirstOrDefault(c => c.Key.Model.GetType() == modelType && c.Key.FieldName == fieldName).Value;
        return validator != null;
    }

    private static bool IsPublic(PropertyInfo p) => p.GetMethod != null && p.SetMethod != null && p.GetMethod.IsPublic && p.SetMethod.IsPublic;

    /// <summary>
    /// <para lang="zh">EditModel 数据模型验证方法</para>
    /// <para lang="en">EditModel data model validation method</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="results"></param>
    /// <param name="cancellationToken"></param>
    internal async Task ValidateObjectAsync(ValidationContext context, List<ValidationResult> results, CancellationToken cancellationToken = default)
    {
        await _validationLock.WaitAsync(cancellationToken);
        try
        {
            await ValidateObjectCoreAsync(context, results, cancellationToken);
        }
        finally
        {
            _validationLock.Release();
        }
    }

    private async Task ValidateObjectCoreAsync(ValidationContext context, List<ValidationResult> results, CancellationToken cancellationToken)
    {
        foreach (var validator in _validatorCache.Values)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await validator.SetValidationPendingState();
        }

        _validateResults.Clear();

        if (ValidateAllProperties)
        {
            await ValidatePropertyAsync(context, results, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
        }
        else
        {
            // 遍历所有可验证组件进行数据验证
            foreach (var (fieldIdentifier, validator) in _validatorCache)
            {
                if (!validator.IsNeedValidate)
                {
                    continue;
                }

                var messages = new List<ValidationResult>();
                var pi = fieldIdentifier.Model.GetType().GetPropertyByName(fieldIdentifier.FieldName);
                if (pi != null)
                {
                    var propertyValidateContext = new ValidationContext(fieldIdentifier.Model, context, null)
                    {
                        MemberName = fieldIdentifier.FieldName,
                        DisplayName = fieldIdentifier.GetDisplayName()
                    };

                    // 设置其关联属性字段
                    var propertyValue = Utility.GetPropertyValue(fieldIdentifier.Model, fieldIdentifier.FieldName);

                    await ValidateAsync(validator, propertyValidateContext, messages, pi, propertyValue, cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                }
                _validateResults.TryAdd(validator, messages);
                results.AddRange(messages);
            }

            // 验证 IValidatableObject
            if (results.Count == 0)
            {
                var messages = new List<ValidationResult>();
#if NET11_0_OR_GREATER
                IAsyncValidatableObject? asyncValidate;
                if (context.ObjectInstance is IAsyncValidatableObject asyncValidatableObject)
                {
                    asyncValidate = asyncValidatableObject;
                }
                else
                {
                    asyncValidate = context.GetInstanceFromMetadataType<IAsyncValidatableObject>();
                }

                if (asyncValidate != null)
                {
                    await foreach (var message in asyncValidate.ValidateAsync(context, cancellationToken).WithCancellation(cancellationToken))
                    {
                        messages.Add(message);
                    }
                }
                else
#endif
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
                        messages.AddRange(validate.Validate(context));
                    }
                }

                if (messages.Count > 0)
                {
                    foreach (var validator in _validatorCache.Values)
                    {
                        if (validator.IsNeedValidate)
                        {
                            _validateResults[validator].AddRange(messages);
                        }
                    }
                    results.AddRange(messages);
                }
            }

            ValidMemberNames.RemoveAll(name => _validateResults.Values.SelectMany(i => i).Any(i => i.MemberNames.Contains(name)));
            foreach (var (validator, messages) in _validateResults)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await validator.ToggleMessage(messages);
            }
        }

    }

    /// <summary>
    /// <para lang="zh">通过表单内绑定的字段验证方法</para>
    /// <para lang="en">Validates a field bound within the form</para>
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    /// <param name="cancellationToken"></param>
    internal async Task ValidateFieldAsync(FieldIdentifier fieldIdentifier, ValidationContext context, List<ValidationResult> results, CancellationToken cancellationToken = default)
    {
        await _validationLock.WaitAsync(cancellationToken);
        try
        {
            if (_validatorCache.TryGetValue(fieldIdentifier, out var validator))
            {
                if (validator.IsNeedValidate)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await validator.SetValidationPendingState();

                    var pi = fieldIdentifier.Model.GetType().GetPropertyByName(fieldIdentifier.FieldName);
                    if (pi != null)
                    {
                        var propertyValue = Utility.GetPropertyValue(fieldIdentifier.Model, fieldIdentifier.FieldName);
                        await ValidateAsync(validator, context, results, pi, propertyValue, cancellationToken);
                    }

                    // 客户端提示
                    cancellationToken.ThrowIfCancellationRequested();
                    await validator.ToggleMessage(results);
                }
            }
        }
        finally
        {
            _validationLock.Release();
        }
    }

    /// <summary>
    /// <para lang="zh">通过属性设置的 DataAnnotation 进行数据验证</para>
    /// <para lang="en">Validates data using DataAnnotations set on properties</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    /// <param name="propertyInfo"></param>
    /// <param name="memberName"></param>
    /// <param name="cancellationToken"></param>
    private async Task ValidateDataAnnotationsAsync(object? value, ValidationContext context, List<ValidationResult> results, PropertyInfo propertyInfo, string? memberName = null, CancellationToken cancellationToken = default)
    {
        IEnumerable<ValidationAttribute> rules = propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>();
        var metadataType = context.ObjectType.GetCustomAttribute<MetadataTypeAttribute>(false);
        if (metadataType != null)
        {
            var p = metadataType.MetadataClassType.GetPropertyByName(propertyInfo.Name);
            if (p != null)
            {
                rules = rules.Concat(p.GetCustomAttributes(true).OfType<ValidationAttribute>());
            }
        }

        var validationRules = rules.ToList();
        memberName ??= propertyInfo.Name;

        foreach (var rule in validationRules.Where(static rule => rule is RequiredAttribute))
        {
            var result = rule.GetValidationResult(value, context);
            AddValidationResult(rule, result, context, results, memberName);
        }

        if (results.Count == 0)
        {
            foreach (var rule in validationRules.Where(static rule => rule is not RequiredAttribute and not AsyncValidationAttribute))
            {
                cancellationToken.ThrowIfCancellationRequested();
                var result = rule.GetValidationResult(value, context);
                AddValidationResult(rule, result, context, results, memberName);
            }
        }

        if (results.Count == 0)
        {
            var validationTasks = validationRules
                .OfType<AsyncValidationAttribute>()
                .Select(async rule => (Rule: rule, Result: await rule.GetValidationResultAsync(value, context, cancellationToken)));
            var validationResults = await Task.WhenAll(validationTasks);
            cancellationToken.ThrowIfCancellationRequested();

            foreach (var (rule, result) in validationResults)
            {
                AddValidationResult(rule, result, context, results, memberName);
            }
        }
    }

    private void AddValidationResult(ValidationAttribute rule, ValidationResult? result, ValidationContext context, List<ValidationResult> results, string memberName)
    {
        if (result != null && result != ValidationResult.Success)
        {
            var find = false;
            if (!string.IsNullOrEmpty(rule.ErrorMessage))
            {
                var resourceType = Options.CurrentValue.ResourceManagerStringLocalizerType;
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
                    rule.ErrorMessage = msg;
                    find = true;
                }

                if (!find && LocalizerFactory.Create(rule.GetType()).TryGetLocalizerString(nameof(rule.ErrorMessage), out msg))
                {
                    rule.ErrorMessage = msg;
                    find = true;
                }

                if (!find)
                {
                    var ruleNameSpan = rule.GetType().Name.AsSpan();
                    var index = ruleNameSpan.IndexOf(nameof(Attribute).AsSpan(), StringComparison.OrdinalIgnoreCase);
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
                ? rule.FormatErrorMessage(context.DisplayName)
                : rule.ErrorMessage;
            results.Add(new ValidationResult(errorMessage, [memberName]));
        }
    }

    /// <summary>
    /// <para lang="zh">验证整个模型时验证属性方法</para>
    /// <para lang="en">Validates properties when validating the entire model</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="results"></param>
    /// <param name="cancellationToken"></param>
    private async Task ValidatePropertyAsync(ValidationContext context, List<ValidationResult> results, CancellationToken cancellationToken = default)
    {
        var properties = context.ObjectType.GetRuntimeProperties().Where(p => IsPublic(p) && p.IsCanWrite() && p.GetIndexParameters().Length == 0);
        foreach (var pi in properties)
        {
            var propertyValue = Utility.GetPropertyValue(context.ObjectInstance, pi.Name);
            var fieldIdentifier = new FieldIdentifier(context.ObjectInstance, pi.Name);
            context.DisplayName = fieldIdentifier.GetDisplayName();
            context.MemberName = fieldIdentifier.FieldName;

            if (_validatorCache.TryGetValue(fieldIdentifier, out var validator))
            {
                if (validator.IsComplexValue(propertyValue) && propertyValue != null)
                {
                    var fieldContext = new ValidationContext(propertyValue, context, null);
                    await ValidatePropertyAsync(fieldContext, results, cancellationToken);
                }
                else
                {
                    var messages = new List<ValidationResult>();
                    if (validator.IsNeedValidate)
                    {
                        await ValidateAsync(validator, context, messages, pi, propertyValue, cancellationToken);

                        cancellationToken.ThrowIfCancellationRequested();
                        await validator.ToggleMessage(messages);
                    }
                    results.AddRange(messages);
                }
            }
            else
            {
                var messages = new List<ValidationResult>();
                await ValidateDataAnnotationsAsync(propertyValue, context, messages, pi, cancellationToken: cancellationToken);
                results.AddRange(messages);
            }
        }
    }

    private async Task ValidateAsync(IValidateComponent validator, ValidationContext context, List<ValidationResult> messages, PropertyInfo pi, object? propertyValue, CancellationToken cancellationToken = default)
    {
        if (validator is IUpload uploader)
        {
            if (uploader.UploadFiles.Count > 0)
            {
                foreach (var file in uploader.UploadFiles)
                {
                    await ValidateDataAnnotationsAsync(file.File, context, messages, pi, file.ValidateId, cancellationToken);
                }
            }
            else
            {
                if (propertyValue is string)
                {

                }
                else if (propertyValue is IEnumerable)
                {
                    propertyValue = null;
                }
                await ValidateDataAnnotationsAsync(propertyValue, context, messages, pi, cancellationToken: cancellationToken);
            }
        }
        else
        {
            await ValidateDataAnnotationsAsync(propertyValue, context, messages, pi, cancellationToken: cancellationToken);
            if (messages.Count == 0)
            {
                await validator.ValidatePropertyAsync(propertyValue, context, messages, cancellationToken);
            }

            if (messages.Count == 0)
            {
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
    }

    private List<ButtonBase> AsyncSubmitButtons { get; } = [];

    /// <summary>
    /// <para lang="zh">注册提交按钮</para>
    /// <para lang="en">Registers a submit button</para>
    /// </summary>
    /// <param name="button"></param>
    internal void RegisterAsyncSubmitButton(ButtonBase button)
    {
        AsyncSubmitButtons.Add(button);
    }

    /// <summary>
    /// <para lang="zh">注销提交按钮</para>
    /// <para lang="en">Unregisters a submit button</para>
    /// </summary>
    /// <param name="button"></param>
    internal void UnregisterAsyncSubmitButton(ButtonBase button)
    {
        AsyncSubmitButtons.Remove(button);
    }

    private async Task OnSubmitForm(EditContext context)
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

        try
        {
            var valid = await _validator.ValidateAsync();
            if (valid)
            {
                if (OnValidSubmit != null)
                {
                    await OnValidSubmit(context);
                }
            }
            else if (OnInvalidSubmit != null)
            {
                await OnInvalidSubmit(context);
            }
        }
        finally
        {
            foreach (var b in AsyncSubmitButtons)
            {
                b.TriggerAsync(false);
            }
        }
    }

    private BootstrapBlazorDataAnnotationsValidator _validator = default!;

    private EditContext? _formlessEditContext;

    /// <summary>
    /// <para lang="zh">支持取消操作的异步验证方法</para>
    /// <para lang="en">Asynchronously validates the form with cancellation support</para>
    /// </summary>
    /// <param name="cancellationToken"></param>
    public Task<bool> ValidateAsync(CancellationToken cancellationToken = default) => _validator.ValidateAsync(cancellationToken);

    /// <summary>
    /// <para lang="zh">通知属性改变方法</para>
    /// <para lang="en">Notifies that a property has changed</para>
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <param name="value"></param>
    public void NotifyFieldChanged(in FieldIdentifier fieldIdentifier, object? value)
    {
        ValueChangedFields.AddOrUpdate(fieldIdentifier, key => value, (key, v) => value);
        OnFieldValueChanged?.Invoke(fieldIdentifier.FieldName, value);
    }

    /// <summary>
    /// <para lang="zh">获得 当前表单值改变的属性集合</para>
    /// <para lang="en">Gets the collection of properties whose values have changed in the current form</para>
    /// </summary>
    public ConcurrentDictionary<FieldIdentifier, object?> ValueChangedFields { get; } = new();

    /// <summary>
    /// <para lang="zh">获得 当前表单值改变的属性集合</para>
    /// <para lang="en">Gets the collection of properties whose values have changed in the current form</para>
    /// </summary>
    [Obsolete("已弃用，单词拼写错误，请使用 ValueChangedFields，Deprecated Please use ValueChangedFields instead. wrong typo")]
    [ExcludeFromCodeCoverage]
    public ConcurrentDictionary<FieldIdentifier, object?> ValueChagnedFields { get; } = new();
}
