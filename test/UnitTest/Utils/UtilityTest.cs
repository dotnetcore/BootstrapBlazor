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
    public void GetKeyValue_Null()
    {
        Foo? foo = null;
        Assert.Throws<ArgumentNullException>(() => Utility.GetKeyValue<object?, int>(foo));
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
            new Foo { Count = 10 },
            new Foo { Count = 20 }
        };
        var invoker = Utility.GetSortFunc<Foo>();
        var orderFoos = invoker.Invoke(foos, nameof(Foo.Count), SortOrder.Asc).ToList();
        Assert.True(orderFoos[0].Count < orderFoos[1].Count);
        orderFoos = invoker.Invoke(foos, nameof(Foo.Count), SortOrder.Desc).ToList();
        Assert.True(orderFoos[0].Count > orderFoos[1].Count);
    }

    [Fact]
    public void ElementCount_Ok()
    {
        var p1 = new List<string>() { "1", "2" };
        Assert.Equal(2, LambdaExtensions.ElementCount(p1));

        var p2 = new string[] { "1", "2" };
        Assert.Equal(2, LambdaExtensions.ElementCount(p2));
    }

    [Fact]
    public void GetSortListFunc_Ok()
    {
        var p1 = Utility.GetSortListFunc<Foo>();
        var foos = new Foo[]
        {
            new() { Count = 2, Name = "1" },
            new() { Count = 1, Name = "1" },
            new() { Count = 4, Name = "2" },
            new() { Count = 3, Name = "2" }
        };
        var sortedFoos = p1(foos, new List<string>() { "Name desc", "Count" });
        Assert.Equal(3, sortedFoos.ElementAt(0).Count);
        Assert.Equal(4, sortedFoos.ElementAt(1).Count);
        Assert.Equal(1, sortedFoos.ElementAt(2).Count);
        Assert.Equal(2, sortedFoos.ElementAt(3).Count);
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

    [Fact]
    public void GetNullableBoolItems_Ok()
    {
        var dummy = new Dummy();
        var items = Utility.GetNullableBoolItems(dummy, nameof(Dummy.Complete));
        Assert.Equal("请选择 ...", items.ElementAt(0).Text);
        Assert.Equal("True", items.ElementAt(1).Text);
        Assert.Equal("False", items.ElementAt(2).Text);

        items = Utility.GetNullableBoolItems(typeof(Dummy), nameof(Dummy.Complete));
        Assert.Equal("请选择 ...", items.ElementAt(0).Text);
        Assert.Equal("True", items.ElementAt(1).Text);
        Assert.Equal("False", items.ElementAt(2).Text);
    }

    [Fact]
    public void GenerateColumns_Ok()
    {
        var cols = Utility.GenerateColumns<Foo>(col => col.GetFieldName() == "Name");
        Assert.Single(cols);
    }

    [Fact]
    public void CreateDisplayByFieldType_Ok()
    {
        var editor = new MockNullDisplayNameColumn("Name", typeof(string));
        var fragment = new RenderFragment(builder => builder.CreateDisplayByFieldType(editor, new Foo() { Name = "Test-Display" }));
        var cut = Context.Render(builder => builder.AddContent(0, fragment));
        Assert.Equal("<div class=\"form-control is-display\">Test-Display</div>", cut.Markup);
    }

    [Fact]
    public void CreateComponentByFieldType_Ok()
    {
        var editor = new MockNullDisplayNameColumn("Name", typeof(string));
        var fragment = new RenderFragment(builder => builder.CreateComponentByFieldType(new BootstrapBlazorRoot(), editor, new Foo() { Name = "Test-Component" }));
        var cut = Context.Render(builder => builder.AddContent(0, fragment));
        Assert.Contains("class=\"form-control\" disabled=\"disabled\" value=\"Test-Component\"", cut.Markup);
    }

    [Fact]
    public void CreateComponentByFieldType_Customer()
    {
        var editor = new MockNullDisplayNameColumn("TimeSpan", typeof(TimeSpan));
        var fragment = new RenderFragment(builder => builder.CreateComponentByFieldType(new BootstrapBlazorRoot(), editor, new Dummy() { TimeSpan = TimeSpan.FromMinutes(1) }));
        var cut = Context.Render(builder => builder.AddContent(0, fragment));
        Assert.Contains("input type=\"text\"", cut.Markup);
    }

    private class Dummy
    {
        public string? Name { get; set; }

        public bool? Complete { get; set; }

        public string Field = "";

        public TimeSpan TimeSpan { get; set; }
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

    [Fact]
    public void GenerateValueExpression_Exception()
    {
        Assert.Throws<InvalidOperationException>(() => Utility.GenerateValueExpression(new Cat(), "Test", typeof(string)));
        Assert.Throws<InvalidOperationException>(() => Utility.GenerateValueExpression(new Cat(), "Foo.Test", typeof(string)));
    }

    [Fact]
    public void ConvertValueToString_Ok()
    {
        var v = new List<int>() { 1, 2, 3 };
        var actual = Utility.ConvertValueToString(v);
        Assert.Equal("1,2,3", actual);

        var val = new double[] { 1.1, 2.0, 3.1 };
        var actual1 = Utility.ConvertValueToString(val);
        Assert.Equal("1.1,2,3.1", actual1);
    }

    [Fact]
    public void GenerateEditorItems_Ok()
    {
        var cols = Utility.GenerateEditorItems<Foo>();
        Assert.Equal(7, cols.Count());

        cols = Utility.GenerateEditorItems<Foo>(new MockTableColumn[]
        {
            new("Name", typeof(string)) { Text = "test-Name" }
        });
        Assert.Equal("test-Name", cols.First(i => i.GetFieldName() == "Name").Text);
    }

    private class MockNullDisplayNameColumn : MockTableColumn, IEditorItem
    {
        public MockNullDisplayNameColumn(string fieldName, Type propertyType) : base(fieldName, propertyType)
        {

        }

        string IEditorItem.GetDisplayName() => null!;
    }

    private class Cat
    {
        public Foo Foo { get; set; } = new Foo();
    }
}
