// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;
public class TableExportTest
{
    [Fact]
    public async Task Export_Ok()
    {
        ITableExport exporter = new MockTableExport();
        IEnumerable<Foo> items = Enumerable.Range(1, 10).Select(i => new Foo { Id = i, Name = $"Name {i}" });
        IEnumerable<ITableColumn> cols = new ITableColumn[]
        {
            new TableColumn<Foo, string>()
        };
        var actual = await exporter.ExportAsync(items, cols, new TableExportOptions(), null);
        Assert.True(actual);

        actual = await exporter.ExportExcelAsync(items, cols, new TableExportOptions(), null);
        Assert.True(actual);

        actual = await exporter.ExportCsvAsync(items, cols, new TableExportOptions(), null);
        Assert.True(actual);

        actual = await exporter.ExportPdfAsync(items, cols, new TableExportOptions(), null);
        Assert.True(actual);
    }

    class MockTableExport : ITableExport
    {
        public Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null) => Task.FromResult(true);

        public Task<bool> ExportCsvAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null) => Task.FromResult(true);

        public Task<bool> ExportExcelAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null) => Task.FromResult(true);

        public Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null) => Task.FromResult(true);
    }
}
