using System;
using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BootstrapInputTextBase 组件
    /// </summary>
    public abstract class CheckboxBase<TItem> : ValidateInputBase<TItem>
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("form-checkbox")
            .AddClass("is-checked", State == CheckboxState.Checked)
            .AddClass("is-indeterminate", State == CheckboxState.Mixed)
            .AddClass("is-disabled", IsDisabled)
            .AddClass(CssClass).AddClass(ValidCss)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 复选框状态字符串
        /// </summary>
        protected string StateString => State switch
        {
            CheckboxState.UnChecked => "false",
            CheckboxState.Checked => "true",
            _ => "mixed"
        };

        /// <summary>
        /// 获得 按钮 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 选择框状态
        /// </summary>
        [Parameter]
        public CheckboxState State { get; set; }

        /// <summary>
        /// 获得/设置 选择框状态改变时回调此方法
        /// </summary>
        [Parameter]
        public Action<CheckboxState, TItem>? OnStateChanged { get; set; }

        /// <summary>
        /// 点击选择框方法
        /// </summary>
        protected virtual void OnToggleClick()
        {
            if (!IsDisabled)
            {
                State = State == CheckboxState.Checked ? CheckboxState.UnChecked : CheckboxState.Checked;

                if (typeof(TItem) == typeof(bool))
                {
                    var v = (bool?)Convert.ChangeType(Value, TypeCode.Boolean) ?? State != CheckboxState.Checked;
                    Value = (TItem)(object)(!v);
                    if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
                }
                OnStateChanged?.Invoke(State, Value);
            }
        }
    }
}
