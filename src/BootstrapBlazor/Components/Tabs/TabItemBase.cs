using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TabItem 组件基类
    /// </summary>
    public abstract class TabItemBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 文本文字
        /// </summary>
        protected string? ClassName => CssBuilder.Default("tabs-item is-top")
            .AddClass("is-active", IsActive)
            .Build();

        /// <summary>
        /// 获得/设置 文本文字
        /// </summary>
        [Parameter] public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 当前状态是否激活
        /// </summary>
        [Parameter] public bool IsActive { get; set; }

        /// <summary>
        /// 获得/设置 点击时回调方法
        /// </summary>
        [Parameter] public Action<TabItemBase>? OnClick { get; set; }

        /// <summary>
        /// 设置是否被选中方法
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active) => IsActive = active;
    }
}
