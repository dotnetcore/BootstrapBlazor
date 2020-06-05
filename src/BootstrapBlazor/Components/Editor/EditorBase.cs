using Microsoft.AspNetCore.Components;
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

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                await JSRuntime.Invoke(EditorElement, "editor", IsEditor, Height);
            }
        }
    }
}
