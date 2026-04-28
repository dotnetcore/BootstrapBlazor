// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace UnitTest.Utils;

public class ChangeDetectionTaskTest
{
    [Fact]
    public void Stop_Ok()
    {
        var type = typeof(Table<>).Assembly.GetType("BootstrapBlazor.Components.ChangeDetectionCleanTask");
        Assert.NotNull(type);

        var methodInfo = type.GetMethod("Stop", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(methodInfo);

        methodInfo.Invoke(null, null);
    }

    private sealed class MockTable : ITable
    {
        public List<ITableColumn> Columns { get; } = [];

        public Dictionary<string, IFilterAction> Filters { get; } = [];

        public Func<Task>? OnFilterAsync { get; }

        public IEnumerable<ITableColumn> GetVisibleColumns() => Columns;
    }
}
