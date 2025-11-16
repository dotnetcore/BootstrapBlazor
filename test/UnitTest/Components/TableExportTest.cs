// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

        actual = await exporter.ExportPdfAsync(items, cols, null);
        Assert.True(actual);

        actual = await exporter.ExportPdfAsync(items, cols, new TableExportOptions(), null);
        Assert.True(actual);

        actual = await exporter.ExportPdfAsync(items, cols, new TableExportOptions(), null, null);
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
