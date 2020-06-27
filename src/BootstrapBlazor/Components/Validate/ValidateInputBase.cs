using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 内置验证组件基类
    /// </summary>
    public abstract class ValidateInputBase<TItem> : ValidateBase<TItem>, IValidateComponent, IValidateRules
    {
        /// <summary>
        /// 获得/设置 错误描述信息
        /// </summary>
        protected string? ErrorMessage { get; set; }

        /// <summary>
        /// 获得/设置 数据合规样式
        /// </summary>
        protected string? ValidCss => IsValid.HasValue ? (IsValid.Value ? "is-valid" : "is-invalid") : null;

        /// <summary>
        /// 获得/设置 Tooltip 命令
        /// </summary>
        protected string TooltipMethod { get; set; } = "";

        /// <summary>
        /// 获得/设置 组件是否合规 默认为 null 未检查
        /// </summary>
        protected bool? IsValid { get; set; }

        /// <summary>
        /// 获得 ValidateFormBase 实例
        /// </summary>
        [CascadingParameter]
        public ValidateFormBase? EditForm { get; set; }

        private string? _id;
        /// <summary>
        /// 获得 当前组件 Id
        /// </summary>
        [Parameter]
        public override string? Id
        {
            get { return (EditForm != null && !string.IsNullOrEmpty(EditForm.Id) && FieldIdentifier != null) ? $"{EditForm.Id}_{FieldIdentifier.Value.FieldName}" : _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 获得 子组件 RenderFragment 实例
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 显示名称
        /// </summary>
        [Parameter]
        public string? DisplayText { get; set; }

        /// <summary>
        /// 验证组件添加时调用此方法
        /// </summary>
        /// <param name="validator"></param>
        public virtual void OnRuleAdded(IValidator validator)
        {

        }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (FieldIdentifier != null && DisplayText == null)
            {
                DisplayText = FieldIdentifier.Value.GetDisplayName();
            }

            if (EditForm != null && FieldIdentifier != null)
            {
                EditForm.AddValidator((EditForm, FieldIdentifier.Value.Model.GetType(), FieldIdentifier.Value.FieldName), this);
            }
        }

        /// <summary>
        /// 调用 Tooltip 脚本方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void InvokeTooltip(bool firstRender)
        {
            base.InvokeTooltip(firstRender);

            if (!firstRender && !string.IsNullOrEmpty(TooltipMethod))
            {
                InvokeAsync(async () =>
                {
                    await Task.Delay(10);

                    // 异步执行
                    JSRuntime.Tooltip(Id, TooltipMethod, title: ErrorMessage);
                    TooltipMethod = "";
                }).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 获得 数据验证方法集合
        /// </summary>
        public ICollection<IValidator> Rules { get; } = new HashSet<IValidator>();

        /// <summary>
        /// 属性验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public void ValidateProperty(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            Rules.ToList().ForEach(validator => validator.Validate(propertyValue, context, results));
        }

        /// <summary>
        /// 显示/隐藏验证结果方法
        /// </summary>
        /// <param name="results"></param>
        /// <param name="validProperty">是否对本属性进行数据验证</param>
        public void ToggleMessage(IEnumerable<ValidationResult> results, bool validProperty)
        {
            if (Rules.Any() && FieldIdentifier != null)
            {
                var messages = results.Where(item => item.MemberNames.Any(m => m == FieldIdentifier.Value.FieldName));
                if (messages.Any())
                {
                    ErrorMessage = messages.First().ErrorMessage;
                    IsValid = false;

                    // 控件自身数据验证时显示 tooltip
                    // EditForm 数据验证时调用 tooltip('enable') 保证 tooltip 组件生成
                    // 调用 tooltip('hide') 后导致鼠标悬停时 tooltip 无法正常显示
                    TooltipMethod = validProperty ? "show" : "enable";
                }
                else
                {
                    ErrorMessage = null;
                    IsValid = true;
                    TooltipMethod = "dispose";
                }

                OnValidate(IsValid ?? true);
            }
        }

        /// <summary>
        /// 客户端检查完成时调用此方法
        /// </summary>
        /// <param name="valid">检查结果</param>
        protected virtual void OnValidate(bool valid)
        {
            InvokeTooltip(false);
        }

        /// <summary>
        /// 将 字符串 Value 属性转化为 泛型 Value 方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="validationErrorMessage"></param>
        /// <returns></returns>
        protected override bool TryParseValueFromString(string value, out TItem result, out string? validationErrorMessage)
        {
            var t = typeof(TItem);
            if (t == typeof(string))
            {
                result = (TItem)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (t.IsEnum)
            {
                var success = BindConverter.TryConvertTo<TItem>(value, CultureInfo.CurrentCulture, out var parsedValue);
                if (success)
                {
                    result = parsedValue;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
#nullable disable
                    result = default;
#nullable restore
                    if (FieldIdentifier != null)
                    {
                        validationErrorMessage = $"The {FieldIdentifier.Value.FieldName} field is not valid.";
                        return false;
                    }
                    else
                    {
                        validationErrorMessage = null;
                        return true;
                    }
                }
            }
            else if (t.IsValueType)
            {
                if (string.IsNullOrEmpty(value))
                {
                    validationErrorMessage = null;
#nullable disable
                    result = default;
#nullable restore
                    return true;
                }

                try
                {
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var ft = t.GetGenericArguments().FirstOrDefault();
                        if (ft != null)
                        {
                            result = (TItem)Convert.ChangeType(value, ft);
                            validationErrorMessage = null;
                            return true;
                        }
                    }
                    else
                    {
                        if (value.TryParse<TItem>(out var v))
                        {
                            result = v;
                            validationErrorMessage = null;
                            return true;
                        }
                        else
                        {
                            result = (TItem)Convert.ChangeType(value, typeof(TItem));
                            validationErrorMessage = null;
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    validationErrorMessage = ex.Message;
#nullable disable
                    result = default;
#nullable restore
                    return false;
                }
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TItem)}'.");
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                JSRuntime.Tooltip(Id, "dispose");
            }
        }
    }
}
