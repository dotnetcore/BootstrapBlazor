using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

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
        protected virtual string? ClassString => CssBuilder.Default("form-checkbox")
            .AddClass("is-checked", State == CheckboxState.Checked)
            .AddClass("is-indeterminate", State == CheckboxState.Mixed)
            .AddClass("is-disabled", IsDisabled)
            .AddClass(CssClass).AddClass(ValidCss)
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
        /// State 状态改变回调方法
        /// </summary>
        /// <value></value>
        [Parameter] public EventCallback<CheckboxState> StateChanged { get; set; }

        /// <summary>
        /// 获得/设置 选择框状态改变时回调此方法
        /// </summary>
        [Parameter]
        public Func<CheckboxState, TItem, Task>? OnStateChanged { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 通过 Value 设置 State
            if (typeof(TItem) == typeof(bool))
            {
                var v = (bool?)Convert.ChangeType(Value, TypeCode.Boolean) ?? false;
                State = v ? CheckboxState.Checked : CheckboxState.UnChecked;
            }
        }

        /// <summary>
        /// 点击选择框方法
        /// </summary>
        protected virtual async Task OnToggleClick()
        {
            if (!IsDisabled)
            {
                State = State != CheckboxState.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;

                if (typeof(TItem) == typeof(bool))
                {
                    var v = (bool?)Convert.ChangeType(Value, TypeCode.Boolean) ?? false;
                    Value = (TItem)(object)(!v);
                    if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
                }
                if (StateChanged.HasDelegate) await StateChanged.InvokeAsync(State);
                if(OnStateChanged != null) await OnStateChanged.Invoke(State, Value);
            }
        }

        /// <summary>
        /// 设置 复选框状态方法
        /// </summary>
        /// <param name="state"></param>
        public virtual async Task SetState(CheckboxState state)
        {
            State = state;
            if (StateChanged.HasDelegate) await StateChanged.InvokeAsync(State);
            StateHasChanged();
        }
    }
}
