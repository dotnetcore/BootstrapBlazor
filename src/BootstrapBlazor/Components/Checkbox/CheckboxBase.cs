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
        protected override string? ClassName => CssBuilder.Default("form-checkbox")
            .AddClass("is-checked", State == CheckboxState.Checked)
            .AddClass("is-indeterminate", State == CheckboxState.Mixed)
            .AddClass(CssClass).AddClass(ValidCss)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

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
        protected void OnToggleClick()
        {
            State = State == CheckboxState.Checked ? CheckboxState.UnChecked : CheckboxState.Checked;
            OnStateChanged?.Invoke(State, Value);
        }
    }
}
