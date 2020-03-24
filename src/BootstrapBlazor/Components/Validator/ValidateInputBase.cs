using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 内置验证组件基类
    /// </summary>
    public abstract class ValidateInputBase<TItem> : InputBase<TItem>, IValidateComponent, IRules
    {
        /// <summary>
        /// 获得 IJSRuntime 实例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 获得 LgbEditFormBase 实例
        /// </summary>
        [CascadingParameter]
        public ValidateFormBase? EditForm { get; set; }

        /// <summary>
        /// 获得 当前组件 Id
        /// </summary>
        public string Id
        {
            get { return $"{EditForm?.Id}_{FieldIdentifier.FieldName}"; }
        }

        /// <summary>
        /// 获得 子组件 RenderFragment 实例
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得 PlaceHolder 属性
        /// </summary>
        protected string? PlaceHolder
        {
            get
            {
                if (AdditionalAttributes != null &&
                    AdditionalAttributes.TryGetValue("placeholder", out var ph) &&
                    !string.IsNullOrEmpty(Convert.ToString(ph)))
                {
                    return ph.ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// 获得/设置 错误描述信息
        /// </summary>
        protected string ErrorMessage { get; set; } = "";

        /// <summary>
        /// 获得/设置 数据合规样式
        /// </summary>
        protected string ValidCss { get; set; } = "";

        /// <summary>
        /// 获得/设置 显示名称 默认为 -
        /// </summary>
        protected string DisplayName { get; set; } = "-";

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            EditForm?.AddValidator((EditForm, FieldIdentifier.Model.GetType(), FieldIdentifier.FieldName), this);
            DisplayName = FieldIdentifier.GetDisplayName();
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            if (!string.IsNullOrEmpty(_tooltipMethod) && !string.IsNullOrEmpty(Id))
            {
                JSRuntime.Tooltip(Id, _tooltipMethod);
                _tooltipMethod = "";
            }
        }

        /// <summary>
        /// 获得 数据验证方法集合
        /// </summary>
        public ICollection<ValidatorComponentBase> Rules { get; } = new HashSet<ValidatorComponentBase>();

        private string _tooltipMethod = "";
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
            if (Rules.Any())
            {
                var messages = results.Where(item => item.MemberNames.Any(m => m == FieldIdentifier.FieldName));
                if (messages.Any())
                {
                    ErrorMessage = messages.First().ErrorMessage;
                    ValidCss = "is-invalid";

                    // 控件自身数据验证时显示 tooltip
                    // EditForm 数据验证时调用 tooltip('enable') 保证 tooltip 组件生成
                    // 调用 tooltip('hide') 后导致鼠标悬停时 tooltip 无法正常显示
                    _tooltipMethod = validProperty ? "show" : "enable";
                }
                else
                {
                    ErrorMessage = "";
                    ValidCss = "is-valid";
                    _tooltipMethod = "dispose";
                }
            }
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
            if (typeof(TItem) == typeof(string))
            {
                result = (TItem)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TItem).IsEnum)
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
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }
            else if (typeof(TItem).IsValueType)
            {
                result = (TItem)Convert.ChangeType(value, typeof(TItem));
                validationErrorMessage = null;
                return true;
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TItem)}'.");
        }
    }
}
