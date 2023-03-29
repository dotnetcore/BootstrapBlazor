// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Consoles
/// </summary>
public sealed partial class Consoles
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Consoles>? Localizer { get; set; }

    /// <summary>
    /// GetItemAttributes
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetItemAttributes() => new AttributeItem[]
    {
        new AttributeItem(){
            Name = "Message",
            Description = "控制台输出消息",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "Color",
            Description = "消息颜色",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "None"
        }
    };

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem(){
            Name = nameof(BootstrapBlazor.Components.Console.Items),
            Description = "组件数据源",
            Type = "IEnumerable<ConsoleMessageItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "Height",
            Description = "组件高度",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new AttributeItem(){
            Name = nameof(BootstrapBlazor.Components.Console.IsAutoScroll),
            Description = "是否自动滚屏",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem(){
            Name = "ShowAutoScroll",
            Description = "是否显示自动滚屏选项",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem(){
            Name = "OnClear",
            Description = "组件清屏回调方法",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new AttributeItem(){
            Name = "HeaderText",
            Description = "Header 显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "系统监控"
        },
        new AttributeItem(){
            Name = "HeaderTemplate",
            Description = "Header 模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "LightTitle",
            Description = "指示灯 Title",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "通讯指示灯"
        },
        new AttributeItem(){
            Name = "IsFlashLight",
            Description = "指示灯是否闪烁",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem(){
            Name = "LightColor",
            Description = "指示灯颜色",
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Color.Success"
        },
        new AttributeItem(){
            Name = "ShowLight",
            Description = "是否显示指示灯",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem(){
            Name = "ClearButtonText",
            Description = "按钮显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "清屏"
        },
        new AttributeItem(){
            Name = "ClearButtonIcon",
            Description = "按钮显示图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-fw fa-solid fa-xmark"
        },
        new AttributeItem(){
            Name = "ClearButtonColor",
            Description = "按钮颜色",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "Secondary"
        },
        new AttributeItem(){
            Name = "FooterTemplate",
            Description = "Footer 模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
