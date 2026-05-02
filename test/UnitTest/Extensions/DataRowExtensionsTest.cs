// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Data;
using System.Reflection;

namespace UnitTest.Extensions;

public class DataRowExtensionsTest
{
    [Fact]
    public void IsDeletedOrDetached_Ok()
    {
        var type = Type.GetType("BootstrapBlazor.Components.DataRowExtensions, BootstrapBlazor");
        Assert.NotNull(type);

        var method = type.GetMethod("IsDeletedOrDetached", BindingFlags.Static | BindingFlags.Public);
        Assert.NotNull(method);

        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));

        var row = table.Rows.Add(1);
        Assert.False(IsDeletedOrDetachedInvoke(row));
        table.AcceptChanges();

        row.Delete();
        Assert.True(IsDeletedOrDetachedInvoke(row));

        var detachedRow = table.NewRow();
        Assert.True(IsDeletedOrDetachedInvoke(detachedRow));

        table.Rows.Add(detachedRow);
        Assert.False(IsDeletedOrDetachedInvoke(detachedRow));

        bool IsDeletedOrDetachedInvoke(DataRow row)
        {
            return (bool)method.Invoke(null, new object[] { row })!;
        }
    }
}
