// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Progress 进度条组件
    /// </summary>
    public abstract class ProgressBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("progress-bar")
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("progress-bar-striped", IsStriped)
            .AddClass("progress-bar-animated", IsAnimated)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Style 集合
        /// </summary>
        protected string? StyleName => CssBuilder.Default()
            .AddClass($"width: {Value}%;")
            .Build();

        /// <summary>
        /// 获得 ProgressStyle 集合
        /// </summary>
        protected string? ProgressStyle => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height.HasValue)
            .Build();

        /// <summary>
        /// 获得/设置 控件高度默认 20px
        /// </summary>
        [Parameter] public int? Height { get; set; }

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示进度条值
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsShowValue { get; set; }

        /// <summary>
        /// 获得/设置 是否显示为条纹
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsStriped { get; set; }

        /// <summary>
        /// 获得/设置 是否动画
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsAnimated { get; set; }

        private int _value;
        /// <summary>
        /// 获得/设置 组件进度值
        /// </summary>
        [Parameter]
        public int Value
        {
            set
            {
                _value = value;
            }
            get
            {
                return Math.Min(100, Math.Max(0, _value));
            }
        }

        /// <summary>
        /// 获得 当前值
        /// </summary>
        protected string ValueString => Value.ToString();

        /// <summary>
        /// 获得 当前值百分比标签文字
        /// </summary>
        protected string? ValueLabelString => IsShowValue ? $"{Value}%" : null;
    }
}
