// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Utils;

public class UtilityTest : BootstrapBlazorTestBase
{
    public Foo Model { get; set; }

    public UtilityTest()
    {
        Model = new Foo()
        {
            Name = "Test"
        };
    }

    [Fact]
    public void GetKeyValue_Ok()
    {
        var foo = new Foo() { Id = 1 };
        var v = Utility.GetKeyValue<Foo, int>(foo);
        Assert.Equal(1, v);

        object foo1 = new Foo() { Id = 2 };
        v = Utility.GetKeyValue<object, int>(foo1);
        Assert.Equal(2, v);
    }

    [Fact]
    public void GetPropertyValue_Ok()
    {
        var v = Utility.GetPropertyValue(Model, nameof(Foo.Name));
        Assert.Equal("Test", v);
    }

    [Fact]
    public void GetSortFunc_Ok()
    {
        var foos = new List<Foo>
        {
            new Foo { Count = 10},
            new Foo { Count = 20 }
        };
        var invoker = Utility.GetSortFunc<Foo>();
        var orderFoos = invoker.Invoke(foos, nameof(Foo.Count), SortOrder.Asc).ToList();
        Assert.True(orderFoos[0].Count < orderFoos[1].Count);
        orderFoos = invoker.Invoke(foos, nameof(Foo.Count), SortOrder.Desc).ToList();
        Assert.True(orderFoos[0].Count > orderFoos[1].Count);
    }

    [Fact]
    public void GetPlaceHolder_Ok()
    {
        var ph = Utility.GetPlaceHolder(typeof(Foo), "Name");
        Assert.Equal("不可为空", ph);

        // 动态类型
        ph = Utility.GetPlaceHolder(DynamicObjectHelper.CreateDynamicType(), "Name");
        Assert.Null(ph);
    }

    [Fact]
    public void Reset_Ok()
    {
        var foo = new Foo()
        {
            Name = "张三"
        };
        Utility.Reset(foo);
        Assert.Null(foo.Name);
    }

    [Fact]
    public void Clone_Ok()
    {
        var dummy = new Dummy()
        {
            Name = "Test"
        };
        var d = Utility.Clone(dummy);
        Assert.NotEqual(d, dummy);
        Assert.Equal(d.Name, dummy.Name);

        // ICloneable
        var o = new MockClone()
        {
            Name = "Test"
        };
        var mo = Utility.Clone(o);
        Assert.NotEqual(o, mo);
        Assert.Equal(o.Name, mo.Name);
    }

    private class Dummy
    {
        public string? Name { get; set; }
    }

    private class MockClone : ICloneable
    {
        public string? Name { get; set; }

        public object Clone()
        {
            return new MockClone()
            {
                Name = Name
            };
        }
    }

    [Fact]
    public void Copy_Ok()
    {
        var d1 = new Dummy() { Name = "Test" };
        var d2 = new Dummy();
        Utility.Copy(d1, d2);
        Assert.Equal("Test", d2.Name);
    }

    [Fact]
    public void CascadingTree_Ok()
    {
        var items = new List<TreeItem>
        {
            new TreeItem() { Text = "001_系统管理", Id = "001" },
            new TreeItem() { Text = "001_01_基础数据管理", Id = "001_01", ParentId = "001" },
            new TreeItem() { Text = "001_01_01_教师", Id = "001_01_01", ParentId = "001_01" },
            new TreeItem() { Text = "001_01_02_职工", Id = "001_01_02", ParentId = "001_01" },

            new TreeItem() { Text = "001_02_餐厅数据管理", Id = "001_02", ParentId = "001" },
            new TreeItem() { Text = "001_02_01_厨师", Id = "001_02_01", ParentId = "001_02" },
            new TreeItem() { Text = "001_02_02_服务员", Id = "001_02_02", ParentId = "001_02" },

        };
        var GetTreeItems = items.CascadingTree();
        Assert.NotNull(GetTreeItems);
        Assert.Equal(2, GetTreeItems.First().Items.Count);
    }


    [Fact]
    public void CascadingMenu_Ok()
    {
        var items2 = new List<MenuItem>
        {
            new MenuItem() { Text = "001_系统管理", Id = "001" },
            new MenuItem() { Text = "001_01_基础数据管理", Id = "001_01", ParentId = "001" },
            new MenuItem() { Text = "001_01_01_教师", Id = "001_01_01", ParentId = "001_01" },
            new MenuItem() { Text = "001_01_02_职工", Id = "001_01_02", ParentId = "001_01" },

            new MenuItem() { Text = "001_02_餐厅数据管理", Id = "001_02", ParentId = "001" },
            new MenuItem() { Text = "001_02_01_厨师", Id = "001_02_01", ParentId = "001_02" },
            new MenuItem() { Text = "001_02_02_服务员", Id = "001_02_02", ParentId = "001_02" },

        };
        var GetMenuItems = items2.CascadingMenu();
        Assert.NotNull(GetMenuItems);
        Assert.Equal(2, GetMenuItems.First().Items.Count());
    }
}
