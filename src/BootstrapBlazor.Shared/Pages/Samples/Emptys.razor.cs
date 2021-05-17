// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Emptys
    {

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? subTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private IStringLocalizer<Emptys>? localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Title = localizer[nameof(Title)];
            subTitle = localizer[nameof(subTitle)];
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Image",
                Description = "自定义图片路径",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Description",
                Description = "自定义描述信息",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " 暂无描述 "
            },
            new AttributeItem() {
                Name = "Width",
                Description = "自定义图片宽度",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " 100 "
            },
            new AttributeItem() {
                Name = "Height",
                Description = "自定义图片高度",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " 100 "
            },
            new AttributeItem() {
                Name = "Telemplate",
                Description = "自定义模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
