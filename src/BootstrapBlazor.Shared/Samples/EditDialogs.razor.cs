// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class EditDialogs
{
    private Foo Model { get; set; } = new Foo()
    {
        Name = "Name 1234",
        Address = "Address 1234"
    };

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private async Task ShowDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbys(Localizer);

        var option = new EditDialogOption<Foo>()
        {
            Title = "编辑对话框",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                Trace.Log("关闭按钮被点击");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                Trace.Log("保存按钮被点击");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }

    private async Task ShowAlignDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbys(Localizer);

        var option = new EditDialogOption<Foo>()
        {
            Title = "编辑对话框",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            LabelAlign = Alignment.Right,
            OnCloseAsync = () =>
            {
                Trace.Log("关闭按钮被点击");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                Trace.Log("保存按钮被点击");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }

    private async Task ShowEditDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbys(Localizer);

        // 设置 地址与数量 不可编辑
        item = items.First(i => i.GetFieldName() == nameof(Foo.Address));
        item.Editable = false;
        item = items.First(i => i.GetFieldName() == nameof(Foo.Count));
        item.Editable = false;

        var option = new EditDialogOption<Foo>()
        {
            Title = "编辑对话框",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                Trace.Log("关闭按钮被点击");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                Trace.Log("保存按钮被点击");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
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
                Description = "编辑项集合",
                Type = "IEnumerable<IEditorItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DialogBodyTemplate",
                Description = "EditDialog Body 模板",
                Type = "RenderFragment<TModel>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CloseButtonText",
                Description = "关闭按钮文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "关闭"
            },
            new AttributeItem() {
                Name = "SaveButtonText",
                Description = "保存按钮文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "保存"
            },
            new AttributeItem() {
                Name = "OnSaveAsync",
                Description = "保存回调委托",
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
