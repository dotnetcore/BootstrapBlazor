// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Card组件基类
    /// </summary>
    public abstract class CardBase : BootstrapComponentBase
    {
        /// <summary>
        ///  Card组件样式
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("card")
            .AddClass("text-center", IsCenter)
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass(Class)
            .Build();

        /// <summary>
        ///  设置Body Class组件样式
        /// </summary>
        protected virtual string? BodyClassName => CssBuilder.Default("card-body")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass(Class)
            .Build();

        /// <summary>
        /// 设置Footer Class样式
        /// </summary>
        protected virtual string? FooterClassName => CssBuilder.Default("card-footer")
            .AddClass("text-muted", IsCenter)
            .Build();

        /// <summary>
        /// 设置Class样式
        /// </summary>
        [Parameter]
        public string? Class { get; set; }


        /// <summary>
        /// 获得/设置 CardHeard
        /// </summary>
        [Parameter]
        public RenderFragment? CardHeader { get; set; }

        /// <summary>
        /// 获得/设置 CardBody
        /// </summary>
        [Parameter]
        public RenderFragment? CardBody { get; set; }

        /// <summary>
        /// 获得/设置 CardFooter
        /// </summary>
        [Parameter]
        public RenderFragment? CardFooter { get; set; }

        /// <summary>
        /// 获得/设置Card颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; }

        /// <summary>
        /// 设置是否居中
        /// </summary>
        [Parameter]
        public bool IsCenter { get; set; }
    }
}
