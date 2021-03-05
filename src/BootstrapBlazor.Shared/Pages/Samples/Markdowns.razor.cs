// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [NotNull]
        private Markdown? MarkdownElement { get; set; }

        private string ShowHideButtonString { get; set; } = "隐藏 Editor";

        /// <summary>
        /// 获得/设置 版本号字符串
        /// </summary>
        private string Version { get; set; } = "fetching";

        private string? Language { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            Language = CultureInfo.CurrentUICulture.Name;
            Version = await VersionManager.GetVersionAsync("bootstrapblazor.markdown");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await MarkdownElement.SetMarkdown("# 这里是初始化的 Markdown");
            }
        }

        private async Task GetMarkdownString()
        {
            MarkdownString = await MarkdownElement.GetMarkdownString();
        }

        private async Task GetHTMLString()
        {
            HtmlString = await MarkdownElement.GetMarkdownHtmlString();
        }

        private async Task GetSelectedString()
        {
            MarkdownString = await MarkdownElement.GetSelectedText();
        }

        private void InsertText()
        {
            var _ = MarkdownElement.InsertText("# 这是插入的内容");
        }

        private async Task ShowHideEditor()
        {
            if (ShowHideButtonString == "隐藏 Editor")
            {
                ShowHideButtonString = "显示 Editor";
                await MarkdownElement.Hide();
            }
            else
            {
                ShowHideButtonString = "隐藏 Editor";
                await MarkdownElement.Show();
            }
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
            }
        };
    }
}
