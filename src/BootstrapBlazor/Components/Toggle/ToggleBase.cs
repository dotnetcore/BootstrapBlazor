// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toggle 开关组件
    /// </summary>
    public class ToggleBase : ValidateBase<bool>
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("toggle btn")
            .AddClass("btn-default off", !Value)
            .AddClass("disabled", IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 ToggleOn 样式
        /// </summary>
        protected string? ToggleOnClassString => CssBuilder.Default("toggle-on")
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

        /// <summary>
        /// 获得 Style 集合
        /// </summary>
        protected virtual string? StyleName => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width > 0)
            .Build();

        /// <summary>
        /// 获得/设置 组件宽度
        /// </summary>
        [Parameter]
        public virtual int Width { get; set; } = 120;

        /// <summary>
        /// 获得/设置 组件 On 时显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public virtual string? OnText { get; set; }

        /// <summary>
        /// 获得/设置 组件 Off 时显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public virtual string? OffText { get; set; }

        /// <summary>
        /// 获得/设置 组件颜色 默认为 Success 颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Success;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<bool, Task>? OnValueChanged { get; set; }

        /// <summary>
        /// 点击控件时触发此方法
        /// </summary>
        protected virtual async Task OnClick()
        {
            if (!IsDisabled)
            {
                Value = !Value;
                if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
                OnValueChanged?.Invoke(Value);
            }
        }
    }
}
