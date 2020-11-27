// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DataAnnotationsValidator 验证组件
    /// </summary>
    public class BootstrapBlazorDataAnnotationsValidator : ComponentBase
    {
        /// <summary>
        /// 获得/设置 当前编辑数据上下文
        /// </summary>
        [CascadingParameter]
        private EditContext? CurrentEditContext { get; set; }

        /// <summary>
        /// 获得/设置 当前编辑窗体上下文
        /// </summary>
        [CascadingParameter]
        public ValidateFormBase? EditForm { get; set; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            CurrentEditContext!.AddEditContextDataAnnotationsValidation(EditForm!);
        }
    }
}
