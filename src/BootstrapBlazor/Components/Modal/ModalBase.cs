using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Modal 弹窗组件
    /// </summary>
    public abstract class ModalBase : IdComponentBase
    {
        /// <summary>
        /// 获得 后台关闭弹窗设置
        /// </summary>
        protected string? Backdrop => IsBackdrop ? null : "static";

        /// <summary>
        /// 获得 弹窗组件样式
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("modal-dialog")
            .AddClass("modal-dialog-centered", IsCentered)
            .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("modal-dialog-scrollable", IsScrolling)
            .Build();

        /// <summary>
        /// 获得/设置 弹窗标题
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 弹窗大小
        /// </summary>
        [Parameter]
        public Size Size { get; set; }

        /// <summary>
        /// 获得/设置 是否垂直居中 默认为 true
        /// </summary>
        [Parameter]
        public bool IsCentered { get; set; }

        /// <summary>
        /// 获得/设置 是否后台关闭弹窗
        /// </summary>
        [Parameter] public bool IsBackdrop { get; set; }

        /// <summary>
        /// 获得/设置 是否弹窗正文超长时滚动
        /// </summary>
        [Parameter] public bool IsScrolling { get; set; }

        /// <summary>
        /// 获得/设置 是否显示关闭按钮
        /// </summary>
        [Parameter] public bool ShowCloseButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示 Footer 默认为 true
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; } = true;

        /// <summary>
        /// 获得/设置 ModalBody 代码块
        /// </summary>
        [Parameter]
        public RenderFragment? ModalBody { get; set; }

        /// <summary>
        /// 获得/设置 弹窗 Footer 代码块
        /// </summary>
        [Parameter]
        public RenderFragment? ModalFooter { get; set; }

        /// <summary>
        /// 弹窗状态切换方法
        /// </summary>
        public void Toggle()
        {
            if (!string.IsNullOrEmpty(Id)) JSRuntime.InvokeRun(Id, "modal", "toggle");
        }
    }
}
