// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 组件 ID 生成器接口
    /// </summary>
    public interface IComponentIdGenerator
    {
        /// <summary>
        /// 生成组件 Id 方法
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        string Generate(ComponentBase component);
    }
}
