// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Dialog 组件配置类
    /// </summary>
    public class DialogOption
    {
        /// <summary>
        /// 获得/设置 相关弹窗实例
        /// </summary>
        [NotNull]
        public Modal? Dialog { get; internal set; }

        /// <summary>
        /// 获得/设置 弹窗标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 弹窗大小
        /// </summary>
        public Size Size { get; set; } = Size.Large;

        /// <summary>
        /// 获得/设置 是否垂直居中 默认为 true
        /// </summary>
        public bool IsCentered { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否弹窗正文超长时滚动 默认为 false
        /// </summary>
        public bool IsScrolling { get; set; } = false;

        /// <summary>
        /// 获得/设置 是否显示关闭按钮 默认为 true
        /// </summary>
        public bool ShowCloseButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示 Header 关闭按钮 默认为 true
        /// </summary>
        public bool ShowHeaderCloseButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否支持键盘 ESC 关闭当前弹窗 默认 true 支持
        /// </summary>
        public bool IsKeyboard { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示 Footer 默认为 true
        /// </summary>
        public bool ShowFooter { get; set; } = true;

        /// <summary>
        /// 获得/设置 相关连数据，多用于传值使用
        /// </summary>
        public object? BodyContext { get; set; }

        /// <summary>
        /// 获得/设置 ModalBody 组件
        /// </summary>
        public RenderFragment? BodyTemplate { get; set; }

        /// <summary>
        /// 获得/设置 ModalFooter 组件
        /// </summary>
        public RenderFragment? FooterTemplate { get; set; }

        /// <summary>
        /// 获得/设置 自定义组件
        /// </summary>
        public BootstrapDynamicComponent? Component { get; set; }

        /// <summary>
        /// 获得/设置 关闭弹窗回调方法
        /// </summary>
        public Func<Task>? OnCloseAsync { get; set; }

        /// <summary>
        /// 将参数转换为组件属性方法
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, object>> ToAttributes()
        {
            var ret = new List<KeyValuePair<string, object>>
            {
                new(nameof(Size), Size),
                new(nameof(IsCentered), IsCentered),
                new(nameof(IsScrolling), IsScrolling),
                new(nameof(ShowCloseButton), ShowCloseButton),
                new(nameof(ShowHeaderCloseButton), ShowHeaderCloseButton),
                new(nameof(ShowFooter), ShowFooter),
                new(nameof(IsKeyboard), IsKeyboard)
            };
            if (!string.IsNullOrEmpty(Title))
            {
                ret.Add(new(nameof(Title), Title));
            }
            if (BodyContext != null)
            {
                ret.Add(new(nameof(BodyContext), BodyContext));
            }
            return ret;
        }
    }
}
