// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Step 组件
    /// </summary>
    public sealed partial class Step
    {
        private string? ClassString => CssBuilder.Default("step is-horizontal")
            .AddClass("is-flex", IsLast && !((Steps?.IsCenter ?? false) || IsCenter))
            .AddClass("is-center", (Steps?.IsCenter ?? false) || IsCenter)
            .Build();

        private string? StyleString => CssBuilder.Default("margin-right: 0px;")
            .AddClass($"flex-basis: {Space};", !string.IsNullOrEmpty(Space))
            .Build();

        private string? HeadClassString => CssBuilder.Default("step-head")
            .AddClass($"is-{Status.ToDescriptionString()}")
            .Build();

        private string? LineStyleString => CssBuilder.Default()
            .AddClass("transition-delay: 150ms; border-width: 1px; width: 100%;", Status == StepStatus.Finish || Status == StepStatus.Success)
            .Build();

        private string? StepIconClassString => CssBuilder.Default("step-icon")
            .AddClass("is-text", !IsIcon)
            .AddClass("is-icon", IsIcon)
            .Build();

        private string? IconClassString => CssBuilder.Default("step-icon-inner")
            .AddClass(Icon, IsIcon || Status == StepStatus.Finish || Status == StepStatus.Success)
            .AddClass("fa fa-times", IsIcon || Status == StepStatus.Error)
            .AddClass("is-status", !IsIcon && (Status == StepStatus.Finish || Status == StepStatus.Success || Status == StepStatus.Error))
            .Build();

        private string? TitleClassString => CssBuilder.Default("step-title")
            .AddClass($"is-{Status.ToDescriptionString()}")
            .Build();

        private string? DescClassString => CssBuilder.Default("step-description")
            .AddClass($"is-{Status.ToDescriptionString()}")
            .Build();

        private string? StepString => (Status == StepStatus.Process || Status == StepStatus.Wait) && !IsIcon ? (StepIndex + 1).ToString() : null;

        /// <summary>
        /// 获得/设置 步骤显示文字
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 步骤显示图标
        /// </summary>
        [Parameter]
        public string Icon { get; set; } = "fa fa-check";

        /// <summary>
        /// 获得/设置 步骤状态
        /// </summary>
        [Parameter]
        public StepStatus Status { get; set; }

        /// <summary>
        /// 获得/设置 描述信息
        /// </summary>
        [Parameter]
        public string? Description { get; set; }

        /// <summary>
        /// 获得/设置 step 的间距不填写将自适应间距支持百分比
        /// </summary>
        [Parameter]
        public string? Space { get; set; }

        /// <summary>
        /// 获得/设置 是否为图标
        /// </summary>
        [Parameter]
        public bool IsIcon { get; set; }

        /// <summary>
        /// 获得/设置 是否为最后一个 Step
        /// </summary>
        [Parameter]
        public bool IsLast { get; set; }

        /// <summary>
        /// 获得/设置 是否居中对齐
        /// </summary>
        [Parameter]
        public bool IsCenter { get; set; }

        /// <summary>
        /// 获得/设置 Step 顺序
        /// </summary>
        [Parameter]
        public int StepIndex { get; set; }

        /// <summary>
        /// 获得/设置 父级组件 Steps 实例
        /// </summary>
        [CascadingParameter]
        private StepsBase? Steps { get; set; }

        /// <summary>
        /// 获得/设置 步骤组件状态改变时回调委托
        /// </summary>
        [Parameter]
        public Action<StepStatus>? OnStatusChanged { get; set; }
    }
}
