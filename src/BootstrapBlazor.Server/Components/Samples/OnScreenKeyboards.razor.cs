﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 屏幕键盘
/// </summary>
public sealed partial class OnScreenKeyboards
{
    private static readonly Dictionary<string, string> Keys1 = new() { { "0", "L" }, { "1", "O" } };
    private static readonly Dictionary<string, string> Keys2 = new() { { "0", "V" }, { "1", "E" } };

    static KeyboardOption CustomerOption => new()
    {
        KeyboardSpecialcharacters = KeyboardSpecialcharacters.europe
    };

    static KeyboardOption SpecialcharactersOption => new()
    {
        CustomerKeyboardSpecialcharacters = new string[] { "中", "国", "女", "足", "牛啊" }
    };

    static KeyboardOption Option => new()
    {
        keysFontFamily = "Barlow",
        keysFontWeight = "500",
        Theme = KeyboardTheme.dark,
    };

    string BindValue { get; set; } = "virtualkeyboard";

    static KeyboardOption AppOption => new()
    {
        autoScroll = true
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ClassName",
            Description = "获得/设置 组件 class 名称",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "virtualkeyboard"
        },
        new()
        {
            Name = "KeyboardKeys",
            Description = "获得/设置 键盘语言布局",
            Type = "KeyboardKeysType?",
            ValueList = "arabic|english|french|german|hungarian|persian|russian|spanish|turkish",
            DefaultValue = "english"
        },
        new()
        {
            Name = "Keyboard",
            Description = "获得/设置 键盘类型",
            Type = "KeyboardType",
            ValueList = "全键盘 all | 字母 keyboard || 小数字键盘 numpad",
            DefaultValue = "all"
        },
        new()
        {
            Name = "Placement",
            Description = "获得/设置 对齐",
            Type = "KeyboardPlacement",
            ValueList = "顶端 top | 底部 bottom",
            DefaultValue = "bottom"
        },
        new()
        {
            Name = "Placeholder",
            Description = "获得/设置 占位符",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " - "
        },
        new()
        {
            Name = "Specialcharacters",
            Description = "获得/设置 显示特殊字符切换按钮",
            Type = "bool",
            ValueList = " - ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Option",
            Description = "获得/设置 键盘配置",
            Type = "KeyboardOption?",
            ValueList = " - ",
            DefaultValue = " - "
        }
    ];

    /// <summary>
    /// 获得KeyboardOption属性
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetKeyboardOptionAttributes() =>
    [
        new()
        {
            Name = "keysArrayOfObjects",
            Description = "键盘布局数组",
            Type = "List<Dictionary<string, string>>?",
            ValueList = " — ",
            DefaultValue = "英文键盘布局数组"
        },
        new()
        {
            Name = "KeyboardKeysType",
            Description = "键盘语言布局",
            Type = "KeyboardKeysType",
            ValueList = "arabic|english|french|german|hungarian|persian|russian|spanish|turkish",
            DefaultValue = "english"
        },
        new()
        {
            Name = "keysJsonUrl",
            Description = "键盘布局.json文件的路径",
            Type = "string",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = "KeyboardSpecialcharacters",
            Description = "特殊符号键盘类型, 默认|欧洲|自定义",
            Type = "KeyboardSpecialcharacters",
            ValueList = "all|europe|customer",
            DefaultValue = "all"
        },
        new()
        {
            Name = "CustomerKeyboardSpecialcharacters",
            Description = "自定义特殊符号键盘",
            Type = "string[]?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "keysNumpadArrayOfNumbers",
            Description = "自定义数字小键盘",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "language",
            Description = "自定义键的语言代码",
            Type = "string",
            ValueList = "例如 de || en || fr || hu || tr 等...",
            DefaultValue = "en"
        },
        new()
        {
            Name = "Theme",
            Description = "键盘主题",
            Type = "KeyboardTheme",
            ValueList = "light|dark|flat|material|oldschool",
            DefaultValue = " — "
        },
        new()
        {
            Name = "allowRealKeyboard",
            Description = "允许或阻止物理键盘",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "allowMobileKeyboard",
            Description = "允许或阻止使用移动键盘",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "cssAnimations",
            Description = "打开或关闭键盘的 CSS 动画",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "cssAnimationsDuration",
            Description = "CSS 动画持续时间(毫秒)",
            Type = "int",
            ValueList = " - ",
            DefaultValue = "360"
        },
        new()
        {
            Name = "KeyboardCssAnimationsStyle",
            Description = "打开或关闭键盘的 CSS 动画样式",
            Type = "KeyboardCssAnimationsStyle",
            ValueList = "slide|fade|flat|material|oldschool",
            DefaultValue = "slide"
        },
        new()
        {
            Name = "keysAllowSpacebar",
            Description = "启用或禁用空格键",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "keysSpacebarText",
            Description = "空格键文本自定义",
            Type = "string",
            ValueList = " - ",
            DefaultValue = "Space"
        },
        new()
        {
            Name = "keysFontFamily",
            Description = "按键字体名称",
            Type = "string",
            ValueList = " - ",
            DefaultValue = "sans-serif"
        },
        new()
        {
            Name = "keysFontSize",
            Description = "按键文字尺寸",
            Type = "string",
            ValueList = " - ",
            DefaultValue = "22px"
        },
        new()
        {
            Name = "keysFontWeight",
            Description = "按键文字粗细",
            Type = "string",
            ValueList = " - ",
            DefaultValue = "normal"
        },
        new()
        {
            Name = "keysIconSize",
            Description = "按键图标大小",
            Type = "string",
            ValueList = " - ",
            DefaultValue = "25px"
        },
        new()
        {
            Name = "autoScroll",
            Description = "自动滚动",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
    ];

    /// <summary>
    /// 获得KeyboardEnum属性
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetKeyboardEnumAttributes() =>
    [
        new()
        {
            Name = "KeyboardKeysType",
            Description = "键盘语言布局",
            Type = "enum",
            ValueList = "arabic|english|french|german|hungarian|persian|russian|spanish|turkish",
            DefaultValue = "english"
        },
        new()
        {
            Name = "KeyboardType",
            Description = "键盘类型, 全键盘 || 字母 || 小数字键盘",
            Type = "enum",
            ValueList = "all|keyboard|numpad",
            DefaultValue = "all"
        },
        new()
        {
            Name = "KeyboardPlacement",
            Description = "对齐, 顶端 || 底部",
            Type = "enum",
            ValueList = "bottom|top",
            DefaultValue = "bottom"
        },
        new()
        {
            Name = "KeyboardSpecialcharacters",
            Description = "特殊符号键盘类型, 默认 || 欧洲 || 自定义",
            Type = "enum",
            ValueList = "all|europe|customer",
            DefaultValue = "all"
        },
        new()
        {
            Name = "KeyboardTheme",
            Description = "键盘主题, 浅色 || 暗黑 || 平板 || material ||oldschool",
            Type = "enum",
            ValueList = "light|dark|flat|material|oldschool",
            DefaultValue = "light"
        },
        new()
        {
            Name = "KeyboardCssAnimationsStyle",
            Description = "打开或关闭键盘的 CSS 动画样式",
            Type = "enum",
            ValueList = "slide|fade|flat|material|oldschool",
            DefaultValue = "slide"
        }
    ];
}
