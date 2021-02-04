// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Button 按钮组件
    /// </summary>
    public abstract class ButtonBase : TooltipComponentBase
    {
        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("btn")
            .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
            .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("btn-block", IsBlock)
            .AddClass("disabled", IsDisabled)
            .AddClass("is-round", ButtonStyle == ButtonStyle.Round)
            .AddClass("is-circle", ButtonStyle == ButtonStyle.Circle)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 按钮 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得 按钮 tabindex 属性
        /// </summary>
        protected string? Tab => IsDisabled ? "-1" : null;

        /// <summary>
        /// 获得/设置 按钮风格枚举
        /// </summary>
        [Parameter]
        public ButtonStyle ButtonStyle { get; set; }

        /// <summary>
        /// 获得/设置 按钮类型 Submit 为表单提交按钮 Reset 为表单重置按钮 默认为 Button
        /// </summary>
        [Parameter]
        public ButtonType ButtonType { get; set; } = ButtonType.Button;

        /// <summary>
        /// 获得/设置 OnClick 事件
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// 获得/设置 OnClick 事件不刷新父组件
        /// </summary>
        [Parameter]
        public Func<Task>? OnClickWithoutRender { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 显示图标
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 Outline 样式
        /// </summary>
        [Parameter]
        public bool IsOutline { get; set; }

        /// <summary>
        /// 获得/设置 Size 大小
        /// </summary>
        [Parameter]
        public Size Size { get; set; } = Size.None;

        /// <summary>
        /// 获得/设置 Block 模式
        /// </summary>
        [Parameter]
        public bool IsBlock { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 RenderFragment 实例
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得 EditContext 实例
        /// </summary>
        [CascadingParameter]
        protected EditContext? EditContext { get; set; }

        /// <summary>
        /// 获得 ValidateFormBase 实例
        /// </summary>
        [CascadingParameter]
        public ValidateFormBase? ValidateForm { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (AdditionalAttributes == null) AdditionalAttributes = new Dictionary<string, object>();

            if (!AdditionalAttributes.TryGetValue("type", out var _))
            {
                AdditionalAttributes["type"] = "button";
            }

            var onClick = OnClick;
            OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, async e =>
            {
                if (!IsDisabled)
                {
                    if (OnClickWithoutRender != null) await OnClickWithoutRender.Invoke();
                    if (onClick.HasDelegate) await onClick.InvokeAsync(e);
                }
            });
        }

        private bool _prevDisable;
        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender && Tooltip != null)
            {
                var id = RetrieveId();
                if (!string.IsNullOrEmpty(id) && _prevDisable != IsDisabled)
                {
                    _prevDisable = IsDisabled;
                    if (IsDisabled)
                    {
                        if (Tooltip.PopoverType == PopoverType.Tooltip)
                            await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", id, "dispose");
                        else
                            await JSRuntime.InvokeVoidAsync(null, "bb_popover", id, "dispose");
                    }
                    else
                    {
                        if (Tooltip.PopoverType == PopoverType.Tooltip)
                            await ShowTooltip();
                        else
                            await ShowPopover();
                    }
                }
            }
        }

        /// <summary>
        /// 设置按钮是否可用状态
        /// </summary>
        /// <param name="disable"></param>
        public void SetDisable(bool disable)
        {
            IsDisabled = disable;
            StateHasChanged();
        }
    }
}
