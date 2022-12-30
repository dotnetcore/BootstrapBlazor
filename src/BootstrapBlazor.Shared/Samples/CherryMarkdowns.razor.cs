// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// CherryMarkdowns
/// </summary>
public partial class CherryMarkdowns
{
    [Inject]
    [NotNull]
    private IStringLocalizer<CherryMarkdowns>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem(){
            Name = "EditorSettings",
            Description = "编辑器设置",
            Type = "EditorSettings",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "ToolbarSettings",
            Description = "工具栏设置",
            Type = "ToolbarSettings",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "Value",
            Description = "组件值",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "Html",
            Description = "组件 Html 代码",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "OnFileUpload",
            Description = "文件上传回调方法",
            Type = "Func<CherryMarkdownUploadFile, Task<string>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "IsViewer",
            Description = "组件是否为浏览器模式",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    };
}
