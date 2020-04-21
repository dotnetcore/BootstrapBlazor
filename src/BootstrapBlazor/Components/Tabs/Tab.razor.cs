using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Tab 组件
    /// </summary>
    partial class Tab : TabBase
    {
        /// <summary>
        /// 获得/设置 Tab 组件 DOM 实例
        /// </summary>
        private ElementReference TabElement { get; set; }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            JSRuntime.Invoke(TabElement, "tab");
        }
    }
}
