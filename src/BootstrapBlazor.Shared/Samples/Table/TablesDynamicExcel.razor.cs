// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Data;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
partial class TablesDynamicExcel
{
    [NotNull]
    private DataTableDynamicContext? DataTableDynamicContext { get; set; }

    [NotNull]
    private DataTableDynamicContext? DataTableKeyboardDynamicContext { get; set; }

    private DataTable UserData { get; } = new DataTable();

    private DataTable KeyboardData { get; } = new DataTable();

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Tables>? TablesLocalizer { get; set; }

    private string? ButtonAddColumnText { get; set; }

    private string? ButtonRemoveColumnText { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ButtonAddColumnText ??= TablesLocalizer[nameof(ButtonAddColumnText)];
        ButtonRemoveColumnText ??= TablesLocalizer[nameof(ButtonRemoveColumnText)];

        // 初始化 DataTable
        InitDataTable();

        // 初始化 DataTableContext 绑定 Table 组件
        InitDataTableContext();

        // 键盘支持示例
        InitDataTableKeyboard();
    }

    private void InitDataTable()
    {
        UserData.Columns.Add(new DataColumn(nameof(Foo.DateTime), typeof(DateTime)) { DefaultValue = DateTime.Now });
        UserData.Columns.Add(nameof(Foo.Name), typeof(string));
        UserData.Columns.Add(nameof(Foo.Complete), typeof(bool));
        UserData.Columns.Add(nameof(Foo.Education), typeof(string));
        UserData.Columns.Add(nameof(Foo.Count), typeof(int));

        Foo.GenerateFoo(Localizer, 10).ForEach(f =>
        {
            UserData.Rows.Add(f.DateTime, f.Name, f.Complete, f.Education, f.Count);
        });
    }

    private void InitDataTableContext()
    {
        DataTableDynamicContext = new(UserData, (context, col) =>
        {
                // 设置 Enum 类型渲染成 Select
                if (col.GetFieldName() == nameof(Foo.Education))
            {
                col.ComponentType = typeof(Select<string>);
                col.Items = typeof(EnumEducation).ToSelectList(new SelectedItem("", Localizer["NullItemText"].Value));
            }
        });

        var method = DataTableDynamicContext.OnValueChanged;
        DataTableDynamicContext.OnValueChanged = async (model, col, val) =>
        {
                // 调用内部提供的方法
                if (method != null)
            {
                    // 内部方法会更新原始数据源 DataTable
                    await method(model, col, val);
            }

                // 输出日志信息
                Trace.Log($"单元格变化通知 列: {col.GetFieldName()} - 值: {val?.ToString()}");
        };
        DataTableDynamicContext.OnChanged = args =>
        {
                // 输出日志信息
                Trace.Log($"集合值变化通知 行: {args.Items.Count()} - 类型: {args.ChangedType}");
            return Task.CompletedTask;
        };
    }

    private void InitDataTableKeyboard()
    {
        KeyboardData.Columns.Add("Column 1", typeof(string));
        KeyboardData.Columns.Add("Column 2", typeof(string));
        KeyboardData.Columns.Add("Column 3", typeof(string));
        KeyboardData.Columns.Add("Column 4", typeof(string));
        KeyboardData.Columns.Add("Column 5", typeof(string));

        var index = 0;
        Foo.GenerateFoo(Localizer, 9).ForEach(f =>
        {
            index++;
            KeyboardData.Rows.Add($"Cell {index}1", $"Cell {index}2", $"Cell {index}3", $"Cell {index}4", $"Cell {index}5");
        });

        DataTableKeyboardDynamicContext = new(KeyboardData);
    }
}
