// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
partial class AutoFills
{
    [NotNull]
    private Foo Model { get; set; } = new();

    [NotNull]
    private IEnumerable<Foo>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private Task OnSelectedItemChanged(Foo foo)
    {
        Model = Utility.Clone(foo);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static string OnGetDisplayText(Foo foo) => foo.Name ?? "";

    private Task<IEnumerable<Foo>> OnCustomFilter(string searchText)
    {
        var items = string.IsNullOrEmpty(searchText) ? Items : Items.Where(i => i.Count > 50 && i.Name!.Contains(searchText));
        return Task.FromResult(items);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "DisplayCount",
            Description = "匹配数据时显示的数量",
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "NoDataTip",
            Description = "无匹配数据时显示提示信息",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "无匹配数据"
        },
        new AttributeItem() {
            Name = "IgnoreCase",
            Description = "匹配时是否忽略大小写",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsLikeMatch",
            Description = "是否开启模糊查询",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Items",
            Description = "组件数据集合",
            Type = "IEnumerable<TValue>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnCustomFilter",
            Description = "自定义集合过滤规则",
            Type = "Func<string, Task<IEnumerable<TValue>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnGetDisplayText",
            Description = "通过模型获得显示文本方法",
            Type = "Func<TValue, string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnSelectedItemChanged",
            Description = "选项改变回调方法",
            Type = "Func<TValue, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Template",
            Description = "候选项模板",
            Type = "RenderFragment<TValue>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(AutoFill<string>.SkipEnter),
            Description = "是否跳过 Enter 按键处理",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(AutoFill<string>.SkipEsc),
            Description = "是否跳过 Esc 按键处理",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    };
}
