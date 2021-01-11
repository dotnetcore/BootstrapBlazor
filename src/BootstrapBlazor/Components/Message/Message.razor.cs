// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Message
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("message")
            .AddClass("is-bottom", Placement != Placement.Top)
            .Build();

        /// <summary>
        /// 获得 Toast 组件样式设置
        /// </summary>
        private string? StyleName => CssBuilder.Default()
            .AddClass("top: 1rem;", Placement != Placement.Bottom)
            .AddClass("bottom: 1rem;", Placement == Placement.Bottom)
            .Build();

        /// <summary>
        /// 获得 弹出窗集合
        /// </summary>
        private List<MessageOption> _messages { get; } = new List<MessageOption>();

        /// <summary>
        /// 获得 弹出窗集合
        /// </summary>
        private IEnumerable<MessageOption> Messages => _messages;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        protected override async Task Show(MessageOption option)
        {
            _messages.Add(option);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 清除 ToastBox 方法
        /// </summary>
        [JSInvokable]
        public async Task Clear()
        {
            _messages.Clear();
            await InvokeAsync(StateHasChanged);
        }
    }
}
