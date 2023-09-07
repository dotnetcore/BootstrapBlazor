// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// TablesCell
/// </summary>
public partial class TablesCell
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        //获取随机数据
        //Get random data
        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private static void OnCellRenderHandler(TableCellArgs args)
    {
        if (args.Row is Foo foo && args.ColumnName == "Name")
        {
            if (foo.Name == "张三 0002" || foo.Name == "ZhangSan 0002")
            {
                args.Colspan = 2;
                args.Class = "cell-demo";
                args.Value = $"{foo.Name} -- {foo.Address} -- {foo.Count}";
            }
        }
    }

    private async Task OnDoubleClickCellCallback(string columnName, object row, object value)
    {
        var displayName = Utility.GetDisplayName<Foo>(columnName);
        await ToastService.Show(new ToastOption()
        {
            Title = Localizer["TableCellOnDoubleClickCellToastTitle"],
            Content = $"{Localizer["TableCellOnDoubleClickCellCurrentCellName"]}{displayName} {Localizer["TableCellOnDoubleClickCellCurrentValue"]}{value}"
        });
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Row",
            Description = Localizer["RowAttr"],
            Type = "object",
            ValueList = " — ",
            DefaultValue = "<TModel>"
        },
        new()
        {
            Name = "ColumnName",
            Description = Localizer["ColumnNameAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Colspan",
            Description = Localizer["ColspanAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "Class",
            Description = Localizer["ClassAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Value",
            Description = Localizer["ValueAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
