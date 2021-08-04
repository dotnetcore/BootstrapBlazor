// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// Markdown 示例代码
    /// </summary>
    public partial class Markdowns
    {
        private string? MarkdownString { get; set; }

        private string? HtmlString { get; set; }

        /// <summary>
        /// 获得/设置 版本号字符串
        /// </summary>
        private string Version { get; set; } = "fetching";

        private string? Language { get; set; }

        private string? AsyncValue { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            Language = CultureInfo.CurrentUICulture.Name;
            MarkdownString = "### 测试";
            Version = await VersionManager.GetVersionAsync("bootstrapblazor.markdown");
        }

        private async Task GetAsyncString()
        {
            await Task.Delay(600);
            AsyncValue = $"### {DateTime.Now}";
        }

        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem(){
                Name = "Height",
                Description = "控件高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "300"
            },
            new AttributeItem(){
                Name = "MinHeight",
                Description = "控件最小高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "200"
            },
            new AttributeItem(){
                Name = "InitialEditType",
                Description = "初始化时显示的界面",
                Type = "InitialEditType",
                ValueList = "Markdown/Wysiwyg",
                DefaultValue = "Markdown"
            },
            new AttributeItem(){
                Name = "PreviewStyle",
                Description = "预览模式",
                Type = "PreviewStyle",
                ValueList = "Tab/Vertical",
                DefaultValue = "Vertical"
            },
            new AttributeItem(){
                Name = "Language",
                Description = "UI 语言",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "Placeholder",
                Description = "提示信息",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "IsViewer",
                Description = "是否为纯浏览模式",
                Type = "bool",
                ValueList = " true/false ",
                DefaultValue = " false "
            }
        };
    }
}
