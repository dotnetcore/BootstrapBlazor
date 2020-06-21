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
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("card console")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

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
        /// 获得 按钮样式
        /// </summary>
        protected string? ClearButtonClassString => CssBuilder.Default("btn btn-secondary")
            .AddClass($"btn-{ClearButtonColor.ToDescriptionString()}", ClearButtonColor != Color.None)
            .Build();

        /// <summary>
        /// 获得/设置 组件绑定数据源
        /// </summary>
        [Parameter]
        public IEnumerable<string> Items { get; set; } = new string[0];

        /// <summary>
        /// 获得/设置 Header 显示文字 默认值为 系统监控
        /// </summary>
        [Parameter]
        public string HeaderText { get; set; } = "系统监控";

        /// <summary>
        /// 获得/设置 指示灯 Title 显示文字
        /// </summary>
        [Parameter]
        public string LightTitle { get; set; } = "通讯指示灯";

        /// <summary>
        /// 获得/设置 按钮 显示文字 默认值为 清屏
        /// </summary>
        [Parameter]
        public string ClearButtonText { get; set; } = "清屏";

        /// <summary>
        /// 获得/设置 按钮 显示图标 默认值为 fa-times
        /// </summary>
        [Parameter]
        public string ClearButtonIcon { get; set; } = "fa fa-fw fa-times";

        /// <summary>
        /// 获得/设置 按钮 显示图标 默认值为 fa-times
        /// </summary>
        [Parameter]
        public Color ClearButtonColor { get; set; } = Color.Secondary;

        /// <summary>
        /// 获得/设置 清空委托方法
        /// </summary>
        [Parameter]
        public Action? OnClear { get; set; }

        /// <summary>
        /// 获得/设置 组件高度 默认为 126px;
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 清空控制台消息方法
        /// </summary>
        public void ClearConsole()
        {
            OnClear?.Invoke();
        }
    }
}
