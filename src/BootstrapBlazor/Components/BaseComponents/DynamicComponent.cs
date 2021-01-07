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
    /// 动态组件类
    /// </summary>
    public class DynamicComponent
    {
        /// <summary>
        /// 获得/设置 组件参数集合
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> Parameters { get; private set; }

        /// <summary>
        /// 获得/设置 组件类型
        /// </summary>
        public Type ComponentType { get; set; }

        /// <summary>
        /// 获得/设置 是否保持弹窗内组件状态 默认为 false 不保持
        /// </summary>
        public bool KeepChildrenState { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="parameters">TCom 组件所需要的参数集合</param>
        /// <param name="keepState">连续两次弹出相同弹窗时是否保持弹窗内组件内容 默认 false 不保持</param>
        public DynamicComponent(Type componentType, IEnumerable<KeyValuePair<string, object>> parameters, bool keepState = false)
        {
            ComponentType = componentType;
            Parameters = parameters;
            KeepChildrenState = keepState;
        }

        /// <summary>
        /// 创建自定义组件方法
        /// </summary>
        /// <typeparam name="TCom"></typeparam>
        /// <param name="parameters">TCom 组件所需要的参数集合</param>
        /// <param name="keepState">连续两次弹出相同弹窗时是否保持弹窗内组件内容 默认 false 不保持</param>
        /// <returns></returns>
        public static DynamicComponent CreateComponent<TCom>(IEnumerable<KeyValuePair<string, object>> parameters, bool keepState = false) where TCom : IComponent
        {
            return new DynamicComponent(typeof(TCom), parameters, keepState);
        }

        /// <summary>
        /// 创建自定义组件方法
        /// </summary>
        /// <typeparam name="TCom"></typeparam>
        /// <param name="keepState">连续两次弹出相同弹窗时是否保持弹窗内组件内容 默认 false 不保持</param>
        /// <returns></returns>
        public static DynamicComponent CreateComponent<TCom>(bool keepState = false) where TCom : IComponent => CreateComponent<TCom>(Enumerable.Empty<KeyValuePair<string, object>>(), keepState);

        /// <summary>
        /// 创建组件实例并渲染
        /// </summary>
        /// <returns></returns>
        public RenderFragment Render() => builder =>
        {
            var index = 0;
            builder.OpenComponent(index++, ComponentType);
            if (!KeepChildrenState) builder.SetKey(GetHashCode());
            builder.AddMultipleAttributes(index++, Parameters);
            builder.CloseComponent();
        };
    }
}
