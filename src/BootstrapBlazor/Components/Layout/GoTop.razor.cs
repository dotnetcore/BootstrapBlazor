using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GoTop
    {
        private ElementReference GoTopElement { get; set; }

        /// <summary>
        /// 获得/设置 滚动条所在组件
        /// </summary>
        [Parameter]
        public string? Target { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null) await JSRuntime.Invoke(GoTopElement, "footer", Target);
        }
    }
}
