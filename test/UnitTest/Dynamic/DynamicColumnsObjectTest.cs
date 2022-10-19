// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
