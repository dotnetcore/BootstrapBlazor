// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TabItem 组件基类
    /// </summary>
    public class TabItem : ComponentBase
    {
        /// <summary>
        /// 获得/设置 文本文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 请求地址
        /// </summary>
        [Parameter]
        public string? Url { get; set; }

        /// <summary>
        /// 获得/设置 当前状态是否激活
        /// </summary>
        [Parameter]
        public bool IsActive { get; set; }

        /// <summary>
        /// 获得/设置 当前 TabItem 是否可关闭 默认为 true 可关闭
        /// </summary>
        [Parameter]
        public bool Closable { get; set; } = true;

        /// <summary>
        /// 获得/设置 图标字符串 如 "fa fa"
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 组件内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 相关键值
        /// </summary>
        [Parameter]
        public object? Key { get; set; }

        /// <summary>
        /// 获得/设置 所属 Tab 实例
        /// </summary>
        [CascadingParameter]
        protected Tab? TabSet { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TabSet?.AddItem(this);
        }

        /// <summary>
        /// 设置是否被选中方法
        /// </summary>
        /// <param name="active"></param>
        public virtual void SetActive(bool active) => IsActive = active;

        /// <summary>
        /// 通过指定参数集合获取 TabItem 实例
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static TabItem Create(Dictionary<string, object> parameters)
        {
            var item = new TabItem();
            if (parameters.TryGetValue(nameof(Url), out var url))
            {
                parameters[nameof(Url)] = ((string)url).TrimStart('/');
            }
            var _ = item.SetParametersAsync(ParameterView.FromDictionary(parameters));
            return item;
        }
    }
}
