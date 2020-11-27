// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SelectOption 组件
    /// </summary>
    public partial class SelectOption : ComponentBase
    {
        /// <summary>
        /// 获得/设置 显示名称
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// 获得/设置 选项值
        /// </summary>
        [Parameter]
        public string Value { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否选中
        /// </summary>
        [Parameter]
        public bool Active { get; set; }

        /// <summary>
        /// 获得/设置 分组名称
        /// </summary>
        [Parameter]
        public string GroupName { get; set; } = "";

        /// <summary>
        /// 父组件通过级联参数获得
        /// </summary>
        [CascadingParameter]
        private ISelect? Container { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Container?.Add(ToSelectedItem());
        }

        private SelectedItem ToSelectedItem() => new SelectedItem
        {
            Active = Active,
            GroupName = GroupName,
            Text = Text,
            Value = Value
        };
    }
}
