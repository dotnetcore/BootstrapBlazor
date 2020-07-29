using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectItem : ComponentBase
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
        /// 
        /// </summary>
        [CascadingParameter]
        private ISelectContainer? Container { get; set; }

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
