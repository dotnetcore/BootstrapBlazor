using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Editor 组件基类
    /// </summary>
    public abstract class EditorBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 组件 DOM 实例
        /// </summary>
        protected ElementReference EditorElement { get; set; }

        /// <summary>
        /// 获得/设置 JSInterop 实例
        /// </summary>
        protected JSInterop<EditorBase>? Interope { get; set; }

        /// <summary>
        /// 获得 Editor 样式
        /// </summary>
        protected string? EditClassString => CssBuilder.Default("editor-body form-control")
            .AddClass("open", IsEditor)
            .Build();

        /// <summary>
        /// 获得/设置 Placeholder 提示消息
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = "点击后进行编辑";

        /// <summary>
        /// 获得/设置 是否直接显示为富文本编辑框
        /// </summary>
        [Parameter]
        public bool IsEditor { get; set; }

        /// <summary>
        /// 获得/设置 设置组件高度
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        private string? _value;
        private bool _renderValue;
        /// <summary>
        /// 获得/设置 组件值
        /// </summary>
        [Parameter]
        public string? Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _renderValue = true;
                }
            }
        }

        /// <summary>
        /// 获得/设置 组件值变化后的回调委托
        /// </summary>
        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Interope = new JSInterop<EditorBase>(JSRuntime);
                await Interope.Invoke(this, EditorElement, "editor", nameof(Update), Height, Value ?? "");
            }
            if (_renderValue)
            {
                _renderValue = false;
                await JSRuntime.Invoke(EditorElement, "editor", "code", "", "", Value ?? "");
            }
        }

        /// <summary>
        /// Update 方法
        /// </summary>
        /// <param name="value"></param>
        [JSInvokable]
        public async Task Update(string value)
        {
            Value = value;
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            _renderValue = false;
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
                Interope?.Dispose();
            }
        }
    }
}
