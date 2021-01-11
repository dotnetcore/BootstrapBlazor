// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Editors
    {
        private string EditorValue { get; set; } = "初始值 <b>Test</b>";

        private Task OnValueChanged(string val)
        {
            EditorValue = val;
            return Task.CompletedTask;
        }

        private void SetValue()
        {
            EditorValue = "更改后的值";
        }

        private List<EditorToolbarButton> EditorPluginItems = new List<EditorToolbarButton>()
        {
            new EditorToolbarButton()
            {
                IconClass = "fa fa-pencil",
                ButtonName = "plugin1",
                Tooltip = "这是 plugin1 的提示"
            },
            new EditorToolbarButton()
            {
                IconClass = "fa fa-home",
                ButtonName = "plugin2",
                Tooltip = "这是 plugin2 提示"
            }
        };

        private async Task<string> PluginClick(string pluginItemName)
        {
            var ret = "";
            if (pluginItemName == "plugin1")
            {
                var op = new SwalOption()
                {
                    Title = "点击plugin1按钮后弹窗",
                    Content = "点击插件按钮后弹窗并确认后才进行下一步处理",
                    IsConfirm = true
                };
                if (await SwalService.ShowModal(op))
                {
                    ret = "<div class='text-danger'>从plugin1返回的数据</div>";
                }
            }
            if (pluginItemName == "plugin2")
            {
                var op = new SwalOption()
                {
                    Title = "点击plugin2按钮后弹窗",
                    Content = "点击插件按钮后弹窗并确认后才进行下一步处理",
                    IsConfirm = true
                };
                if (await SwalService.ShowModal(op))
                {
                    ret = "从plugin2返回的数据";
                }
            }
            return ret;
        }

        private List<object> ToolbarItems = new List<object>
        {
            new List<object> {"style", new List<string>() {"style"}},
            new List<object> {"font", new List<string>() {"bold", "underline", "clear"}}
        };

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Placeholder",
                Description = "空值时的提示信息",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "点击后进行编辑"
            },
            new AttributeItem() {
                Name = "IsEditor",
                Description = "是否直接显示为富文本编辑框",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "组件高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "ToolbarItems",
                Description = "富文本框工具栏工具",
                Type = "IEnumerable<object>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "CustomerToolbarButtons",
                Description = "自定义按钮",
                Type = "IEnumerable<EditorToolbarButton>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
