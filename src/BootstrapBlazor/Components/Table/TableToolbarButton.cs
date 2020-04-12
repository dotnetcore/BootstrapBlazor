using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表格 Toolbar 按钮组件
    /// </summary>
    public class TableToolbarButton : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created <c>form</c> element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得/设置 Table Toolbar 实例
        /// </summary>
        [CascadingParameter]
        protected TableToolbar? Toolbar { get; set; }

        /// <summary>
        /// 获得/设置 按钮图标 fa fa-fa
        /// </summary>
        [Parameter]
        public string Icon { get; set; } = "";

        /// <summary>
        /// 获得/设置 按钮显示文字
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "未设置";

        /// <summary>
        /// 组件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Toolbar?.AddButtons(this);
        }

        /// <summary>
        /// 点击按钮回调方法
        /// </summary>
        [Parameter]
        public Action? OnClick { get; set; }
    }
}
