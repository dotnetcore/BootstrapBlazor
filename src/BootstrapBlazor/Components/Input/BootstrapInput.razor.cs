// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BootstrapInputTextBase 组件
    /// </summary>
    public partial class BootstrapInput<TValue>
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得 input 组件类型 默认 text
        /// </summary>
        protected string? Type { get; set; }

        /// <summary>
        /// 获得/设置 input 类型 placeholder 属性
        /// </summary>
        protected string? PlaceHolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected ElementReference FocusElement { get; set; }

        /// <summary>
        /// 获得/设置 是否为 Input-Group 组合
        /// </summary>
        [Parameter]
        public bool IsGroup { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串
        /// </summary>
        [Parameter]
        public Func<TValue, string>? Formatter { get; set; }

        /// <summary>
        /// 获得/设置 文本框 Enter 键回调委托方法 默认为 null
        /// </summary>
        [Parameter]
        public Func<TValue, Task>? OnEnterAsync { get; set; }

        /// <summary>
        /// 获得/设置 文本框 Esc 键回调委托方法 默认为 null
        /// </summary>
        [Parameter]
        public Func<TValue, Task>? OnEscAsync { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        [Parameter]
        public string? FormatString { get; set; }

        /// <summary>
        /// 获得/设置 是否自动获取焦点 默认 false 不自动获取焦点
        /// </summary>
        [Parameter]
        public bool IsAutoFocus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("type", out var t))
            {
                Type = t?.ToString();
            }

            if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var ph))
            {
                PlaceHolder = ph?.ToString();
            }
            if (string.IsNullOrEmpty(PlaceHolder) && FieldIdentifier.HasValue)
            {
                PlaceHolder = FieldIdentifier.Value.GetPlaceHolder();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && IsAutoFocus)
            {
                await FocusAsync();
            }
        }

        /// <summary>
        /// 自动获得焦点方法
        /// </summary>
        /// <returns></returns>
        public ValueTask FocusAsync() => FocusElement.FocusAsync();

        /// <summary>
        /// 数值格式化委托方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override string? FormatValueAsString(TValue value) => Formatter != null
            ? Formatter.Invoke(value)
            : (!string.IsNullOrEmpty(FormatString) && value != null
                ? Utility.Format(value, FormatString)
                : base.FormatValueAsString(value));

        /// <summary>
        /// OnKeyUp 方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual async Task OnKeyUp(KeyboardEventArgs args)
        {
            if (args.Key == "Escape" && OnEscAsync != null)
            {
                await OnEscAsync(Value);
            }

            if (args.Key == "Enter" && OnEnterAsync != null)
            {
                await OnEnterAsync(Value);
            }
        }

        private RenderFragment RenderInput => builder =>
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);

            if (!string.IsNullOrEmpty(Type))
            {
                builder.AddAttribute(2, "type", Type);
            }

            if (!string.IsNullOrEmpty(PlaceHolder))
            {
                builder.AddAttribute(2, "placeholder", PlaceHolder);
            }

            if (!string.IsNullOrEmpty(Id))
            {
                builder.AddAttribute(3, "id", Id);
            }

            if (!string.IsNullOrEmpty(ClassName))
            {
                builder.AddAttribute(4, "class", ClassName);
            }

            if (!string.IsNullOrEmpty(Disabled))
            {
                builder.AddAttribute(5, "disabled", Disabled);
            }

            builder.AddAttribute(6, "value", CurrentValueAsString);
            builder.AddAttribute(7, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

            if (OnEnterAsync != null && OnEscAsync != null)
            {
                builder.AddAttribute(8, "onkeyup", EventCallback.Factory.Create<KeyboardEventArgs>(this, OnKeyUp));
            }

            builder.AddElementReferenceCapture(9, e => FocusElement = e);
            builder.CloseElement();
        };
    }
}
