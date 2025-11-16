// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Dynamic;

public class DynamicColumnsObjectTest
{
    [Fact]
    public void DynamicColumnsObject_Ok()
    {
        var data = new DynamicColumnsObject();
        Assert.Empty(data.Columns);
    }

    [Fact]
    public void DynamicColumnsObject_Columns()
    {
        var data = new DynamicColumnsObject(new Dictionary<string, object?>()
        {
            ["A"] = "1",
            ["B"] = 1
        });

        Assert.Equal("1", data.Columns["A"]);
        Assert.Equal(1, data.Columns["B"]);
    }

    [Fact]
    public void DynamicColumnsObject_Key()
    {
        var guid = Guid.NewGuid();
        var data = new DynamicColumnsObject()
        {
            DynamicObjectPrimaryKey = guid
        };

        Assert.Equal(guid, data.DynamicObjectPrimaryKey);
    }

    [Fact]
    public void DynamicColumnsObject_Value()
    {
        var data = new DynamicColumnsObject();
        data.Columns["A"] = "1";
        Assert.Equal("1", data.Columns["A"]);

        data.SetValue("A", 2);
        Assert.Equal(2, data.GetValue("A"));
        Assert.Equal(2, data.Columns["A"]);

        Assert.Null(data.GetValue("B"));
        Assert.Throws<KeyNotFoundException>(() => data.Columns["B"]);
    }
}
