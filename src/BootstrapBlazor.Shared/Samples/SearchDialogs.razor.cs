// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class SearchDialogs
{
    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private async Task ShowDialog()
    {
        var option = new SearchDialogOption<Foo>()
        {
            Title = "搜索弹出框",
            Model = new Foo(),
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                Trace.Log("关闭按钮被点击");
                return Task.CompletedTask;
            },
            OnResetSearchClick = () =>
            {
                Trace.Log("重置按钮被点击");
                return Task.CompletedTask;
            },
            OnSearchClick = () =>
            {
                Trace.Log("搜索按钮被点击");
                return Task.CompletedTask;
            }
        };

        await DialogService.ShowSearchDialog(option);
    }

    private async Task ShowColumnsDialog()
    {
        var model = new Foo();
        var option = new SearchDialogOption<Foo>()
        {
            Title = "搜索弹出框",
            Model = model,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            Items = Utility.GenerateColumns<Foo>(p => p.GetFieldName() == nameof(Foo.Name) || p.GetFieldName() == nameof(Foo.Address))
        };
        await DialogService.ShowSearchDialog(option);
    }

    private async Task ShowInlineDialog()
    {
        var model = new Foo();
        var option = new SearchDialogOption<Foo>()
        {
            Title = "搜索弹出框",
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            Model = model,
            Items = Utility.GenerateColumns<Foo>(p => p.GetFieldName() == nameof(Foo.Name) || p.GetFieldName() == nameof(Foo.Address))
        };
        await DialogService.ShowSearchDialog(option);
    }

    private async Task ShowInlineAlignDialog()
    {
        var model = new Foo();
        var option = new SearchDialogOption<Foo>()
        {
            Title = "搜索弹出框",
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            LabelAlign = Alignment.Right,
            Model = model,
            Items = Utility.GenerateColumns<Foo>(p => p.GetFieldName() == nameof(Foo.Name) || p.GetFieldName() == nameof(Foo.Address))
        };
        await DialogService.ShowSearchDialog(option);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示标签",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Model",
                Description = "泛型参数用于呈现 UI",
                Type = "TModel",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "搜索条件集合",
                Type = "IEnumerable<IEditorItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DialogBodyTemplate",
                Description = "SearchDialog Body 模板",
                Type = "RenderFragment<TModel>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ResetButtonText",
                Description = "重置按钮文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "重置"
            },
            new AttributeItem() {
                Name = "QueryButtonText",
                Description = "查询按钮文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "查询"
            },
            new AttributeItem() {
                Name = "OnResetSearchClick",
                Description = "重置回调委托",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSearchClick",
                Description = "搜索回调委托",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ItemsPerRow",
                Description = "每行显示组件数量",
                Type = "int?",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RowType",
                Description = "设置组件布局方式",
                Type = "RowType",
                ValueList = "Row|Inline",
                DefaultValue = "Row"
            },
            new AttributeItem() {
                Name = "LabelAlign",
                Description = "Inline 布局模式下标签对齐方式",
                Type = "Alignment",
                ValueList = "None|Left|Center|Right",
                DefaultValue = "None"
            }
    };
}
