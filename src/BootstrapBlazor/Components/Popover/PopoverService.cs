using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Popover 服务类
    /// </summary>
    public class PopoverService
    {
        private JSInterop<PopConfirmButton>? Interop { get; set; }

        private PopoverConfirmBase? PopoverConfirm { get; set; }

        private Action? Callback { get; set; }

        internal (PopoverConfirmOption Option, RenderFragment RenderFragment)? ConfirmBox { get; set; }

        /// <summary>
        /// 显示确认弹窗方法
        /// </summary>
        /// <param name="option"></param>
        public void Show(PopoverConfirmOption option)
        {
            ConfirmBox = (option, new RenderFragment(builder =>
            {
                var index = 0;
                builder.OpenComponent<PopoverConfirmBox>(index++);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.SourceId), option.ButtonId);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.OnConfirm), option.OnConfirm);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.OnClose), option.OnClose);

                builder.AddAttribute(index++, nameof(PopoverConfirmBox.Title), option.Title);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.Content), option.Content);

                builder.AddAttribute(index++, nameof(PopoverConfirmBox.CloseButtonText), option.CloseButtonText);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.CloseButtonColor), option.CloseButtonColor);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.ConfirmButtonText), option.ConfirmButtonText);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.ConfirmButtonColor), option.ConfirmButtonColor);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.Icon), option.Icon);

                builder.CloseComponent();
            }));

            Callback?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void InvokeRun()
        {
            if (ConfirmBox.HasValue)
            {
                ConfirmBox.Value.Option.Callback?.Invoke();
            }
        }

        /// <summary>
        /// 关闭确认弹窗方法
        /// </summary>
        public void Hide()
        {
            ConfirmBox = null;
            Callback?.Invoke();
        }

        /// <summary>
        /// 订阅弹窗事件
        /// </summary>
        /// <param name="confirm"></param>
        /// <param name="callback"></param>
        internal void Register(PopoverConfirmBase confirm, Action callback) => (PopoverConfirm, Callback) = (confirm, callback);
    }
}
