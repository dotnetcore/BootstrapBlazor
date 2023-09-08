// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        new()
        {
            Name = "OnResult",
            Description = "签名结果回调方法",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnAlert",
            Description = "手写签名警告信息回调",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnError",
            Description = "错误回调方法",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClose",
            Description = "手写签名关闭信息回调",
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SignAboveLabel",
            Description = "在框内签名标签文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "在框内签名"
        },
        new()
        {
            Name = "ClearBtnTitle",
            Description = "清除按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "清除"
        },
        new()
        {
            Name = "SignatureAlertText",
            Description = "请先签名提示文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "请先签名"
        },
        new()
        {
            Name = "ChangeColorBtnTitle",
            Description = "换颜色按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "换颜色"
        },
        new()
        {
            Name = "UndoBtnTitle",
            Description = "撤消按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "撤消"
        },
        new()
        {
            Name = "SaveBase64BtnTitle",
            Description = "保存为 base64 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "确定"
        },
        new()
        {
            Name = "SavePNGBtnTitle",
            Description = "保存为 PNG 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "PNG"
        },
        new()
        {
            Name = "SaveJPGBtnTitle",
            Description = "保存为 JPG 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "JPG"
        },
        new()
        {
            Name = "SaveSVGBtnTitle",
            Description = "保存为 SVG 按钮文本",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "SVG"
        },
        new()
        {
            Name = "EnableChangeColorBtn",
            Description = "启用换颜色按钮",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "EnableAlertJS",
            Description = "启用 JS 错误弹窗",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "EnableSaveBase64Btn",
            Description = "启用保存为 base64",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "EnableSavePNGBtn",
            Description = "启用保存为 PNG",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "EnableSaveJPGBtn",
            Description = "启用保存为 JPG",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "EnableAlertJS",
            Description = "启用保存为 SVG",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "CssClass",
            Description = "组件 CSS 式样",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "signature-pad-body"
        },
        new()
        {
            Name = "BtnCssClass",
            Description = "按钮 CSS 式样",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn btn-light"
        },
        new()
        {
            Name = "Responsive",
            Description = "启用响应式 css 界面",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "BackgroundColor",
            Description = "组件背景,设置 #0000000 为透明",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "rgb(255, 255, 255)"
        }
    };
}
