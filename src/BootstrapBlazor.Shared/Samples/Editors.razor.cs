// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Editors
{
    private string? EditorValue { get; set; }

    private Task OnValueChanged(string val)
    {
        EditorValue = val;
        return Task.CompletedTask;
    }

    private void SetValue()
    {
        EditorValue = Localizer["UpdateValue"];
    }

    private List<EditorToolbarButton>? EditorPluginItems { get; set; }

    private async Task<string?> PluginClick(string pluginItemName)
    {
        var ret = "";
        if (pluginItemName == "plugin1")
        {
            var op = new SwalOption()
            {
                Title = Localizer["SwalTitle"],
                Content = Localizer["SwalContent"],
                IsConfirm = true
            };
            if (await SwalService.ShowModal(op))
            {
                ret = Localizer["Ret1"];
            }
        }
        if (pluginItemName == "plugin2")
        {
            var op = new SwalOption()
            {
                Title = Localizer["Swal2Title"],
                Content = Localizer["Swal2Content"],
                IsConfirm = true
            };
            if (await SwalService.ShowModal(op))
            {
                ret = Localizer["Ret2"];
            }
        }
        return ret;
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditorValue = Localizer["InitValue"];
        EditorPluginItems = new List<EditorToolbarButton>()
        {
            new EditorToolbarButton()
            {
                IconClass = "fa fa-pencil",
                ButtonName = "plugin1",
                Tooltip = Localizer["ToolTip1"]
            },
            new EditorToolbarButton()
            {
                IconClass = "fa fa-home",
                ButtonName = "plugin2",
                Tooltip = Localizer["ToolTip2"]
            }
        };
    }

    private List<object> ToolbarItems { get; } = new List<object>
        {
            new List<object> {"style", new List<string>() {"style"}},
            new List<object> {"font", new List<string>() {"bold", "underline", "clear"}}
        };

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Placeholder",
                Description = Localizer["Att1"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["Att1DefaultValue"]!
            },
            new AttributeItem() {
                Name = "IsEditor",
                Description = Localizer["Att2"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Height",
                Description = Localizer["Att3"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "ToolbarItems",
                Description = Localizer["Att4"],
                Type = "IEnumerable<object>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "CustomerToolbarButtons",
                Description = Localizer["Att5"],
                Type = "IEnumerable<EditorToolbarButton>",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
