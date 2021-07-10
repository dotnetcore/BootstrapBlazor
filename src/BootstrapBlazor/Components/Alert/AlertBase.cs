// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Alert 警告框组件
    /// </summary>
    public abstract class AlertBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected virtual string? ClassName => CssBuilder.Default("alert fade show")
            .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("is-bar", ShowBar)
            .AddClass("is-close", ShowDismiss)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示关闭按钮
        /// </summary>
        [Parameter]
        public bool ShowDismiss { get; set; }

        /// <summary>
        /// 获得/设置 显示图标
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 是否显示左侧 Bar
        /// </summary>
        [Parameter]
        public bool ShowBar { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 关闭警告框回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnDismiss { get; set; }
    }
}
