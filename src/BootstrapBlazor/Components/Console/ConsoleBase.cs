using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Console 组件
    /// </summary>
    public abstract class ConsoleBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 Console Body Style 字符串
        /// </summary>
        protected string? BodyStyleString => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height > 0)
            .Build();

        /// <summary>
        /// 获得 Footer 样式
        /// </summary>
        protected string? FooterClassString => CssBuilder.Default("card-footer text-right")
            .AddClass("d-none", OnClear == null)
            .Build();

        /// <summary>
        /// 获得/设置 组件绑定数据源
        /// </summary>
        [Parameter]
        public IEnumerable<string> Items { get; set; } = new string[0];

        /// <summary>
        /// 获得/设置 清空委托方法
        /// </summary>
        [Parameter]
        public Action? OnClear { get; set; }

        /// <summary>
        /// 获得/设置 组件高度 默认为 126px;
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 126;

        /// <summary>
        /// 清空控制台消息方法
        /// </summary>
        public void ClearConsole()
        {
            OnClear?.Invoke();
        }
    }
}
