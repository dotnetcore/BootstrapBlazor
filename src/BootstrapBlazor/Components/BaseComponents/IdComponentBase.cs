// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 自动生成客户端 ID 组件基类
    /// </summary>
    public abstract class IdComponentBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 组件 id 属性
        /// </summary>
        [Parameter]
        public virtual string? Id { get; set; }

        [Inject]
        [NotNull]
        private IComponentIdGenerator? ComponentIdGenerator { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Id ??= ComponentIdGenerator.Generate(this);
        }
    }
}
