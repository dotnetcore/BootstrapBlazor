using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;
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
        /// 判断双向绑定类型是否为 boolean 类型
        /// </summary>
        protected bool isBoolean { get; set; }

        /// <summary>
        /// 获得/设置 是否显示 Checkbox 后置 label 文字
        /// </summary>
        protected bool IsShowAfterLabel { get; set; }

        /// <summary>
        /// 获得/设置 选择框状态
        /// </summary>
        [Parameter]
        public CheckboxState State { get; set; }

        /// <summary>
        /// State 状态改变回调方法
        /// </summary>
        /// <value></value>
        [Parameter]
        public EventCallback<CheckboxState> StateChanged { get; set; }

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

            // 内置到 表单中时 DisplayName 放置到组件前面
            IsShowAfterLabel = EditForm == null;

            isBoolean = (Nullable.GetUnderlyingType(typeof(TItem)) ?? typeof(TItem)) == typeof(Boolean);
        }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (isBoolean && Value != null)
            {
                if (BindConverter.TryConvertToBool(Value, CultureInfo.InvariantCulture, out var v))
                {
                    State = v ? CheckboxState.Checked : CheckboxState.UnChecked;
                }
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            _stateChanged = false;
        }

        /// <summary>
        /// 点击选择框方法
        /// </summary>
        protected virtual async Task OnToggleClick()
        {
            if (!IsDisabled)
            {
                await InternalStateChanged(State == CheckboxState.Checked ? CheckboxState.UnChecked : CheckboxState.Checked);
            }
        }

        /// <summary>
        /// 此变量为了提高性能，避免循环更新
        /// </summary>
        private bool _stateChanged;

        private async Task InternalStateChanged(CheckboxState state)
        {
            if (!_stateChanged)
            {
                _stateChanged = true;

                if (isBoolean)
                {
                    CurrentValue = (TItem)(object)(state == CheckboxState.Checked);
                }

                if (State != state)
                {
                    State = state;
                    if (StateChanged.HasDelegate) await StateChanged.InvokeAsync(State);
                    if (OnStateChanged != null) await OnStateChanged.Invoke(State, Value);
                }
            }
        }

        /// <summary>
        /// 设置 复选框状态方法
        /// </summary>
        /// <param name="state"></param>
        public virtual async Task SetState(CheckboxState state)
        {
            await InternalStateChanged(state);
            StateHasChanged();
        }
    }
}
