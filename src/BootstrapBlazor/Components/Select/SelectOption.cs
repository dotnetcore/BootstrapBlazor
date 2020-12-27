// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
