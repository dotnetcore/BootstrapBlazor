using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Block
    {
        /// <summary>
        /// 获得/设置 组件 Title 属性
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 组件说明信息
        /// </summary>
        [Parameter]
        public string Introduction { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 组件内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 组件示例代码文件名
        /// </summary>
        [Parameter]
        public string? CodeFile { get; set; }        
    }
}
