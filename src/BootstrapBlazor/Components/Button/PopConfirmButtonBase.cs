using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 确认弹窗按钮组件
    /// </summary>
    public abstract class PopConfirmButtonBase : ButtonBase
    {
        private JSInterop<PopConfirmButtonBase>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 PopoverConfirm 服务实例
        /// </summary>
        [Inject] PopoverService? PopoverService { get; set; }

        /// <summary>
        /// 获得/设置 弹窗显示位置
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.Auto;

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter] public string Content { get; set; } = "确认删除吗？";

        /// <summary>
        /// 获得/设置 点击确认时回调方法
        /// </summary>
        [Parameter] public Action? OnConfirm { get; set; }

        /// <summary>
        /// 获得/设置 点击关闭时回调方法
        /// </summary>
        [Parameter] public Action? OnClose { get; set; }

        /// <summary>
        /// 获得/设置 点击确认弹窗前回调方法 返回真时弹出弹窗 返回假时不弹出
        /// </summary>
        [Parameter] public Func<bool> OnBeforeClick { get; set; } = new Func<bool>(() => true);

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        [Parameter] public string? Title { get; set; }
        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        [Parameter] public string DisplayText { get; set; } = "删除";

        /// <summary>
        /// 获得/设置 关闭按钮显示文字
        /// </summary>
        [Parameter] public string CloseButtonText { get; set; } = "关闭";

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        [Parameter] public Color CloseButtonColor { get; set; } = Color.Secondary;

        /// <summary>
        /// 获得/设置 确认按钮显示文字
        /// </summary>
        [Parameter] public string ConfirmButtonText { get; set; } = "确定";

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        [Parameter] public Color ConfirmButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 确认框图标
        /// </summary>
        [Parameter] public string? Icon { get; set; } = "fa fa-remove";

        /// <summary>
        /// 获得/设置 确认框图标
        /// </summary>
        [Parameter] public string? ConfirmIcon { get; set; } = "fa fa-exclamation-circle text-info";

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (AdditionalAttributes == null)
            {
                AdditionalAttributes = new Dictionary<string, object>();
            }
            AdditionalAttributes["data-placement"] = Placement.ToDescriptionString();
            AdditionalAttributes["data-toggle"] = "popover";

            // 进行弹窗拦截，点击确认按钮后回调原有 OnClick
            OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, e => Show());
        }

        /// <summary>
        /// 显示确认弹窗方法
        /// </summary>
        protected void Show()
        {
            // 回调消费者逻辑 判断是否需要弹出确认框
            if (OnBeforeClick())
            {
                // 生成客户端弹窗
                PopoverService?.Show(new PopoverConfirmOption()
                {
                    ButtonId = Id,
                    Title = Title,
                    Content = Content,
                    CloseButtonText = CloseButtonText,
                    CloseButtonColor = CloseButtonColor,
                    ConfirmButtonText = ConfirmButtonText,
                    ConfirmButtonColor = ConfirmButtonColor,
                    Icon = ConfirmIcon,
                    OnConfirm = OnConfirm,
                    OnClose = OnClose,
                    Callback = new Action(() =>
                    {
                        // 调用 JS 进行弹窗 等待 弹窗点击确认回调
                        if (JSRuntime != null && !string.IsNullOrEmpty(Id))
                        {
                            if (Interop == null) Interop = new JSInterop<PopConfirmButtonBase>(JSRuntime);
                            Interop.Invoke(this, Id, "confirm", nameof(Hide));
                        }
                    })
                });
            }
        }

        /// <summary>
        /// 清除 PopConfirm 方法
        /// </summary>
        [JSInvokable]
        public void Hide()
        {
            // 通知服务删除元素
            PopoverService?.Hide();
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && Interop != null) Interop.Dispose();

            base.Dispose(disposing);
        }
    }
}
