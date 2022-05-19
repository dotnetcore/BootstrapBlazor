// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public partial class TablesCell
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);
    }

    private static void OnCellRenderHandler(TableCellArgs args)
    {
        if (args.Row is Foo foo && args.ColumnName == "Name")
        {
            if (foo.Name == "张三 0002" || foo.Name == "Zhangsan 0002")
            {
                args.Colspan = 2;
                args.Class = "cell-demo";
                args.Value = $"{foo.Name} -- {foo.Address} -- {foo.Count}";
            }
        }
    }

    private async Task OnDoubleClickCellCallback(string columnName, object row, object value)
    {
        var displayName = Utility.GetDisplayName(typeof(Foo), columnName);
        await ToastService.Show(new ToastOption() { Title = CellLocalizer["ToastTitle"], Content = $"{CellLocalizer["CurrentCellName"]}{displayName} {CellLocalizer["CurrentValue"]}{value}" });
    }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
            new AttributeItem() {
                Name = "Row",
                Description = CellLocalizer["RowAttr"],
                Type = "object",
                ValueList = " — ",
                DefaultValue = "<TModel>"
            },
            new AttributeItem() {
                Name = "ColumnName",
                Description = CellLocalizer["ColumnNameAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Colspan",
                Description = CellLocalizer["ColspanAttr"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "Class",
                Description = CellLocalizer["ClassAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Value",
                Description = CellLocalizer["ValueAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
}
