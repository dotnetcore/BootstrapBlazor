// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 签名演示
/// </summary>
public partial class SignaturePads
{
    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result2 { get; set; }

    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result3 { get; set; }

    private Task OnResult(string result)
    {
        Result = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnResult2(string result)
    {
        Result2 = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnResult3(string result)
    {
        Result3 = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem()
        {
            Name = "OnResult",
            Description = "签名结果回调方法",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "OnAlert",
            Description = "手写签名警告信息回调",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "OnError",
            Description = "错误回调方法",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "OnClose",
            Description = "手写签名关闭信息回调",
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "SignAboveLabel",
            Description = "在框内签名标签文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "在框内签名"
        },
        new AttributeItem()
        {
            Name = "ClearBtnTitle",
            Description = "清除按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "清除"
        },
        new AttributeItem()
        {
            Name = "SignatureAlertText",
            Description = "请先签名提示文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "请先签名"
        },
        new AttributeItem()
        {
            Name = "ChangeColorBtnTitle",
            Description = "换颜色按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "换颜色"
        },
        new AttributeItem()
        {
            Name = "UndoBtnTitle",
            Description = "撤消按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "撤消"
        },
        new AttributeItem()
        {
            Name = "SaveBase64BtnTitle",
            Description = "保存为 base64 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "确定"
        },
        new AttributeItem()
        {
            Name = "SavePNGBtnTitle",
            Description = "保存为 PNG 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "PNG"
        },
        new AttributeItem()
        {
            Name = "SaveJPGBtnTitle",
            Description = "保存为 JPG 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "JPG"
        },
        new AttributeItem()
        {
            Name = "SaveSVGBtnTitle",
            Description = "保存为 SVG 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "SVG"
        },
        new AttributeItem()
        {
            Name = "EnableChangeColorBtn",
            Description = "启用换颜色按钮",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "EnableAlertJS",
            Description = "启用 JS 错误弹窗",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "EnableSaveBase64Btn",
            Description = "启用保存为 base64",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "EnableSavePNGBtn",
            Description = "启用保存为 PNG",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "EnableSaveJPGBtn",
            Description = "启用保存为 JPG",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "EnableAlertJS",
            Description = "启用保存为 SVG",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "CssClass",
            Description = "组件 CSS 式样",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "signature-pad-body"
        },
        new AttributeItem()
        {
            Name = "BtnCssClass",
            Description = "按钮 CSS 式样",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn btn-light"
        },
        new AttributeItem()
        {
            Name = "Responsive",
            Description = "启用响应式 css 界面",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "BackgroundColor",
            Description = "组件背景,设置 rgba(0,0,0,0)为透明",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "rgb(255, 255, 255)"
        }
    };
}
