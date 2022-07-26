// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace UnitTest.Utils;

public class UtilityTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetKeyValue_Ok()
    {
        var foo = new Foo() { Id = 1 };
        var v = Utility.GetKeyValue<Foo, int>(foo);
        Assert.Equal(1, v);
    }

    [Fact]
    public void GetKeyValue_CustomKeyAttribute()
    {
        var foo = new Cat() { Id = 1 };
        var v = Utility.GetKeyValue<Cat, int>(foo, typeof(CatKeyAttribute));
        Assert.Equal(1, v);
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
        var model = new Foo() { Name = "Test" };
        var v = Utility.GetPropertyValue(model, nameof(Foo.Name));
        Assert.Equal("Test", v);

        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var foo = Foo.Generate(localizer);

        var v1 = Utility.GetPropertyValue<Foo, string>(foo, nameof(Foo.Name));
        Assert.Contains("张三", v1);

        var v2 = Utility.GetPropertyValue<object, object>(foo, nameof(Foo.Name));
        Assert.Contains("张三", v2.ToString());

        var v3 = Utility.GetPropertyValue(foo, nameof(Foo.Name));
        Assert.NotNull(v3);
        Assert.Contains("张三", v3!.ToString());
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
        Assert.Equal("请选择 ...", items[0].Text);
        Assert.Equal("True", items[1].Text);
        Assert.Equal("False", items[2].Text);

        items = Utility.GetNullableBoolItems(typeof(Dummy), nameof(Dummy.Complete));
        Assert.Equal("请选择 ...", items[0].Text);
        Assert.Equal("True", items[1].Text);
        Assert.Equal("False", items[2].Text);

        // 动态类型
        var dynamicType = EmitHelper.CreateTypeByName("test_type", new MockTableColumn[] { new("Name", typeof(string)) });
        items = Utility.GetNullableBoolItems(dynamicType!, "Name");
        Assert.Equal("请选择 ...", items[0].Text);
        Assert.Equal("True", items[1].Text);
        Assert.Equal("False", items[2].Text);

        // 读取资源文件中的配置值
        var cat = new Cat();
        items = Utility.GetNullableBoolItems(cat, nameof(cat.Name));
        Assert.Equal("test-Name-NullValue", items[0].Text);
        Assert.Equal("True", items[1].Text);
        Assert.Equal("False", items[2].Text);
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

    [Fact]
    public void GetDisplayName_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = new DataTable();
        fooData.Columns.Add(new DataColumn(nameof(Foo.DateTime), typeof(DateTime)) { DefaultValue = DateTime.Now });
        fooData.Columns.Add(nameof(Foo.Name), typeof(string));
        fooData.Columns.Add(nameof(Foo.Complete), typeof(bool));
        fooData.Columns.Add(nameof(Foo.Education), typeof(string));
        fooData.Columns.Add(nameof(Foo.Count), typeof(int));
        Foo.GenerateFoo(localizer, 10).ForEach(f =>
        {
            fooData.Rows.Add(f.DateTime, f.Name, f.Complete, f.Education, f.Count);
        });

        Assert.Equal("日期", localizer[nameof(Foo.DateTime)]);

        var context = new DataTableDynamicContext(fooData, (context, col) =>
        {
            var propertyName = col.GetFieldName();
            if (propertyName == nameof(Foo.DateTime))
            {
                context.AddRequiredAttribute(nameof(Foo.DateTime));
                // 使用 AutoGenerateColumnAttribute 设置显示名称示例
                context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[] {
                        new(nameof(AutoGenerateColumnAttribute.Text), localizer[nameof(Foo.DateTime)].Value)
                });
            }
            else if (propertyName == nameof(Foo.Name))
            {
                context.AddRequiredAttribute(nameof(Foo.Name), localizer["Name.Required"].Value);
                // 使用 Text 设置显示名称示例
                col.Text = localizer[nameof(Foo.Name)];
            }
            else if (propertyName == nameof(Foo.Count))
            {
                context.AddRequiredAttribute(nameof(Foo.Count));
                // 使用 DisplayNameAttribute 设置显示名称示例
                context.AddDisplayNameAttribute(nameof(Foo.Count), localizer[nameof(Foo.Count)].Value);
            }
            else if (propertyName == nameof(Foo.Complete))
            {
                col.Filterable = true;
                // 使用 DisplayAttribute 设置显示名称示例
                context.AddDisplayAttribute(nameof(Foo.Complete), new KeyValuePair<string, object?>[] {
                        new(nameof(DisplayAttribute.Name), localizer[nameof(Foo.Complete)].Value)
                });
                context.AddDescriptionAttribute(nameof(Foo.Complete), "Test-Desc");
            }
        });

        // 静态类
        var dn = Utility.GetDisplayName(typeof(Foo), nameof(Foo.Count));
        Assert.Equal("数量", dn);

        // 动态类
        dn = Utility.GetDisplayName(context.GetItems().First(), nameof(Foo.Count));
        Assert.Equal("数量", dn);

        // 静态类
        dn = Utility.GetDisplayName(typeof(Foo), nameof(Foo.Education));
        Assert.Equal("学历", dn);

        // 静态类
        dn = Utility.GetDisplayName(new Foo() { Education = EnumEducation.Middle }, nameof(Foo.Education));
        Assert.Equal("学历", dn);

        // 动态类
        dn = Utility.GetDisplayName(context.GetItems().First(), nameof(Foo.Education));
        Assert.Equal("Education", dn);

        var pi = context.GetItems().First().GetType().GetProperties().FirstOrDefault(i => i.CustomAttributes.Any(a => a.AttributeType == typeof(DescriptionAttribute)));
        var attr = pi!.GetCustomAttribute<DescriptionAttribute>();
        Assert.Equal("Test-Desc", attr!.Description);

        // 类 Desc
        dn = Utility.GetDisplayName(new Cat(), nameof(Cat.Name));
        Assert.Equal("Cat-Desc", dn);

        dn = Utility.GetDisplayName(typeof(TestEnum), nameof(TestEnum.Name));
        Assert.Equal("Test-Enum-Name", dn);

        dn = Utility.GetDisplayName(typeof(TestEnum), nameof(TestEnum.Address));
        Assert.Equal("Test-Enum-Address", dn);

        dn = Utility.GetDisplayName(typeof(Nullable<TestEnum>), nameof(TestEnum.Name));
        Assert.Equal("Test-Enum-Name", dn);
    }

    [Fact]
    public void SetValue_Null()
    {
        var model = new MockDynamicObject();
        var context = new MockDynamicObjectContext();

        // 内部走 null 逻辑
        _ = context.SetValue(model);
    }

    [Fact]
    public void GetJsonStringFromAssembly_Ok()
    {
        var option = Context.Services.GetRequiredService<IOptions<JsonLocalizationOptions>>().Value;
        var sections = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "UnitTest.Utils.UtilityTest+Cat", null, true);

        // 加载 UnitTest.Locals.en-US.json
        // 加载 BootstrapBlazor.Locals.en.json
        Assert.NotEmpty(sections);

        // dynamic
        var dynamicType = EmitHelper.CreateTypeByName("test_type", new MockTableColumn[] { new("Name", typeof(string)) });
        Utility.GetJsonStringByTypeName(option, dynamicType!.Assembly, "Test");
    }

    private class MockDynamicObject : IDynamicObject
    {
        public Guid DynamicObjectPrimaryKey { get; set; }

        public object? GetValue(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string propertyName, object? value)
        {
            throw new NotImplementedException();
        }
    }

    private class MockDynamicObjectContext : DynamicObjectContext
    {
        public override Task AddAsync(IEnumerable<IDynamicObject> selectedItems)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ITableColumn> GetColumns()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IDynamicObject> GetItems() => new MockDynamicObject[] { new() { DynamicObjectPrimaryKey = Guid.NewGuid() } };
    }

    [Fact]
    public void GetPropertyValue_Null()
    {
        Foo? foo = null;
        Assert.Throws<ArgumentNullException>(() => Utility.GetPropertyValue<object?, string>(foo, nameof(Foo.Name)));
    }

    [Fact]
    public void SetPropertyValue_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var foo = Foo.Generate(localizer);
        var v1 = "张三";
        var val = "李四";
        Utility.SetPropertyValue<Foo, string>(foo, nameof(Foo.Name), val);
        Assert.Equal(foo.Name, val);

        foo.Name = v1;
        Utility.SetPropertyValue<Foo, object>(foo, nameof(Foo.Name), val);
        Assert.Equal(foo.Name, val);

        foo.Name = v1;
        Utility.SetPropertyValue<object, string>(foo, nameof(Foo.Name), val);
        Assert.Equal(foo.Name, val);

        foo.Name = v1;
        Utility.SetPropertyValue<object, object>(foo, nameof(Foo.Name), val);
        Assert.Equal(foo.Name, val);
    }

    [Fact]
    public void SetPropertyValue_Null()
    {
        Foo? foo = null;
        Assert.Throws<ArgumentNullException>(() => Utility.SetPropertyValue<object?, object>(foo, nameof(Foo.Name), "1"));
    }

    [Fact]
    public void TryGetProperty_Ok()
    {
        var condition = Utility.TryGetProperty(typeof(Foo), nameof(Foo.Name), out _);
        Assert.True(condition);

        condition = Utility.TryGetProperty(typeof(Foo), "Test1", out _);
        Assert.False(condition);
    }

    private class Dummy : IFormattable
    {
        public string? Name { get; set; }

        public bool? Complete { get; set; }

        public string Field = "";

        public TimeSpan TimeSpan { get; set; }

        public int Id { get; set; }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return Id.ToString(format, formatProvider);
        }
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

        var items = new List<SelectedItem>() { new SelectedItem("Test1", "Test1"), new SelectedItem("Test2", "Test2") };
        var actual2 = Utility.ConvertValueToString(items);
        Assert.Equal("Test1,Test2", actual2);

        var objs = new List<object?>() { 1, null, "Test" };
        var actual3 = Utility.ConvertValueToString(objs);
        Assert.Equal("1,,Test", actual3);
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

    [Fact]
    public void GetPlaceholder_Ok()
    {
        var text = Utility.GetPlaceHolder<Cat>("Name");
        Assert.Equal("Test-PlaceHolder", text);

        // from resource file
        text = Utility.GetPlaceHolder<Cat>("PlaceHolder");
        Assert.Equal("test-PlaceHolder", text);
    }

    [Fact]
    public void CreateLocalizer_Ok()
    {
        var localizer = Utility.CreateLocalizer<Foo>();
        Assert.NotNull(localizer);
        if (localizer != null)
        {
            Assert.Equal("姓名", localizer["Name"]);
        }

        localizer = Utility.CreateLocalizer<Cat>();
        Assert.NotNull(localizer);
        if (localizer != null)
        {
            Assert.Equal("Name", localizer["Name"]);
            Assert.True(localizer["Name"].ResourceNotFound);
        }

        // dynamic assembly
        var dynamicType = EmitHelper.CreateTypeByName("test_type", new MockTableColumn[] { new("Name", typeof(string)) });
        localizer = Utility.CreateLocalizer(dynamicType!);
        Assert.Null(localizer);
    }

    [Fact]
    public void GetStringLocalizerFromService_Ok()
    {
        // 动态程序集
        var dynamicType = EmitHelper.CreateTypeByName("test-Type", new MockTableColumn[] { new("Name", typeof(string)) });
        var localizer = Utility.GetStringLocalizerFromService(dynamicType!.Assembly, dynamicType.Name);
        Assert.Null(localizer);
    }

    [Fact]
    public void GetJsonStringConfig_Ok()
    {
        var option = new JsonLocalizationOptions
        {
            AdditionalJsonFiles = new string[]
            {
                "zh-CN.json"
            }
        };
        var localizedStrings = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Shared.Foo", "zh-CN", true);
        Assert.Equal("Test-Name", localizedStrings.First(i => i.Name == "Name").Value);
    }

    [Fact]
    public void IgnoreLocalizerMissing_Ok()
    {
        var option = new JsonLocalizationOptions
        {
            IgnoreLocalizerMissing = true
        };
        Assert.True(option.IgnoreLocalizerMissing);
    }

    [Fact]
    public void GetJsonStringConfig_Fallback()
    {
        // 回落默认语言为 en 测试用例为 zh 找不到资源文件
        var option = new JsonLocalizationOptions();
        var configs = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Shared.Foo", "it-it", true);
        Assert.Empty(configs);
    }

    [Fact]
    public void GetJsonStringConfig_Culture()
    {
        // 回落默认语音为 en 测试用例为 en-US 找不到资源文件
        var option = new JsonLocalizationOptions();
        var configs = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Shared.Foo", "en-US", true);
        Assert.NotEmpty(configs);

        var pi = option.GetType().GetProperty("EnableFallbackCulture", BindingFlags.NonPublic | BindingFlags.Instance);
        pi!.SetValue(option, false);
        configs = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Shared.Foo", "en", true);

        // 禁用回落机制
        // UniTest 未提供 en 资源文件 断言为 Empty
        Assert.Empty(configs);
    }

    [Fact]
    public void Format_Ok()
    {
        var actual = Utility.Format(new Cat() { Name = "test" }, CultureInfo.CurrentCulture);
        Assert.Equal("test", actual);
    }

    [Fact]
    public void Format_Format()
    {
        var actual = Utility.Format(1, "D2");
        Assert.Equal("01", actual);

        actual = Utility.Format(new Cat(), "D2");
        Assert.Equal("", actual);

        actual = Utility.Format(new Dummy() { Id = 1 }, "D2");
        Assert.Equal("01", actual);
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

        [PlaceHolder("Test-PlaceHolder")]
        [Description("Cat-Desc")]
        public string? Name { get; set; }

        public string? PlaceHolder { get; set; }

        [CatKey]
        public int Id { get; set; }

        public override string ToString() => Name ?? "";
    }

    private enum TestEnum
    {
        [Description("Test-Enum-Name")]
        Name,

        [Display(Name = "Test-Enum-Address")]
        Address
    }

    private class CatKeyAttribute : Attribute
    {

    }
}
