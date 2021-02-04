// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Step 组件基类
    /// </summary>
    public abstract class StepsBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件样式字符串
        /// </summary>
        protected string? ClassString => CssBuilder.Default("steps")
            .AddClass("steps-horizontal", !IsVertical)
            .AddClass("steps-vertical", IsVertical)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 步骤集合
        /// </summary>
        [Parameter]
        public IEnumerable<StepItem> Items { get; set; } = new StepItem[0];

        /// <summary>
        /// 获得/设置 是否垂直渲染 默认 false 水平渲染
        /// </summary>
        [Parameter]
        public bool IsVertical { get; set; }

        /// <summary>
        /// 获得/设置 是否居中对齐
        /// </summary>
        [Parameter]
        public bool IsCenter { get; set; }

        /// <summary>
        /// 获得/设置 设置当前激活步骤
        /// </summary>
        [Parameter]
        public StepStatus Status { get; set; }

        /// <summary>
        /// 获得/设置 组件内容实例
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 步骤组件状态改变时回调委托
        /// </summary>
        [Parameter]
        public Action<StepStatus>? OnStatusChanged { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var origiContent = ChildContent;
            if (Items.Any())
            {
                ChildContent = new RenderFragment(builder =>
                {
                    var index = 0;
                    foreach (var item in Items)
                    {
                        builder.AddContent(index++, RenderStep(item));
                    }
                    builder.AddContent(index++, origiContent);
                });
            }
        }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Items.Any())
            {
                var status = Items.Where(i => i.Status != StepStatus.Wait).LastOrDefault()?.Status ?? StepStatus.Wait;
                if (Status != status)
                {
                    Status = status;
                    OnStatusChanged?.Invoke(Status);
                }
            }
        }

        /// <summary>
        /// 渲染 Step 组件方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual RenderFragment RenderStep(StepItem item) => new RenderFragment(builder =>
        {
            item.Space = ParseSpace(item.Space);
            var index = 0;
            builder.OpenComponent<Step>(index++);
            builder.SetKey(item);
            builder.AddAttribute(index++, nameof(Step.Title), item.Title);
            builder.AddAttribute(index++, nameof(Step.Icon), item.Icon);
            builder.AddAttribute(index++, nameof(Step.Description), item.Description);
            builder.AddAttribute(index++, nameof(Step.Space), item.Space);
            builder.AddAttribute(index++, nameof(Step.Status), item.Status);
            builder.AddAttribute(index++, nameof(Step.IsLast), item == Items.Last());
            builder.AddAttribute(index++, nameof(Step.IsCenter), IsCenter);
            builder.AddAttribute(index++, nameof(Step.StepIndex), Items.ToList().IndexOf(item));
            builder.CloseComponent();
        });

        private string ParseSpace(string? space)
        {
            if (!string.IsNullOrEmpty(space) && !double.TryParse(space.TrimEnd('%'), out var d)) space = null;

            if (string.IsNullOrEmpty(space)) space = $"{Math.Round(100 * 1.0d / Math.Max(1, Items.Count() - 1), 2)}%";
            return space;
        }
    }
}
