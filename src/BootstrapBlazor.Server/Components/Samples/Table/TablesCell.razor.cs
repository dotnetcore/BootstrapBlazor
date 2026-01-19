// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// TablesCell
/// </summary>
public partial class TablesCell
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
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
}
