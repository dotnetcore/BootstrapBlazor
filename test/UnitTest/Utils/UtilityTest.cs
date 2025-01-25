// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Reflection;
using UnitTest.Components;

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
        Assert.Equal(0, Utility.GetKeyValue<object?, int>(foo));
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
    public void GetRange_Ok()
    {
        var attribute = Utility.GetRange<SliderTest.SliderModel>("Value");
        Assert.NotNull(attribute);
    }

    [Fact]
    public void GetSortFunc_Ok()
    {
        var foos = new List<Foo>
        {
            new() { Count = 10 },
            new() { Count = 20 }
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
        var sortedFoos = p1(foos, ["Name desc", "Count"]);
        Assert.Equal(3, sortedFoos.ElementAt(0).Count);
        Assert.Equal(4, sortedFoos.ElementAt(1).Count);
        Assert.Equal(1, sortedFoos.ElementAt(2).Count);
        Assert.Equal(2, sortedFoos.ElementAt(3).Count);
    }

    [Fact]
    public void GetPlaceHolder_Ok()
    {
        var ph = Utility.GetPlaceHolder<Foo>("Name");
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
        var dynamicType = EmitHelper.CreateTypeByName("test_type", new InternalTableColumn[] { new("Name", typeof(string)) });
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
    public void CreateDisplayByFieldType_Parameter()
    {
        var editor = new MockNullDisplayNameColumn("Name", typeof(string))
        {
            ComponentType = typeof(Textarea),
            ComponentParameters = new Dictionary<string, object>()
            {
                { "rows", "3" }
            }
        };
        var fragment = new RenderFragment(builder => builder.CreateDisplayByFieldType(editor, new Foo() { Name = "Test-Display" }));
        var cut = Context.Render(builder => builder.AddContent(0, fragment));
        Assert.Contains("<textarea readonly rows=\"3\"", cut.Markup);
    }

    [Fact]
    public void CreateDisplayByFieldType_FormatString()
    {
        var dt = DateTime.Now;
        var editor = new InternalTableColumn("DateTime", typeof(DateTime?)) { FormatString = "yyyy" };
        var fragment = new RenderFragment(builder => builder.CreateDisplayByFieldType(editor, new Foo() { DateTime = dt }));
        var cut = Context.Render(builder => builder.AddContent(0, fragment));
        Assert.Equal($"<div class=\"form-control is-display\">{dt:yyyy}</div>", cut.Markup);
    }

    [Fact]
    public void CreateDisplayByFieldType_Formatter()
    {
        var dt = DateTime.Now;
        var editor = new InternalTableColumn("DateTime", typeof(DateTime?))
        {
            Formatter = new Func<object?, Task<string?>>(async v =>
            {
                var ret = "";
                await Task.Delay(1);
                if (v is DateTime d)
                {
                    ret = $"{d:yyyyMMddhhmmss}";
                }
                return ret;
            })
        };
        var fragment = new RenderFragment(builder => builder.CreateDisplayByFieldType(editor, new Foo() { DateTime = dt }));
        var cut = Context.Render(builder => builder.AddContent(0, fragment));
        cut.WaitForAssertion(() =>
        {
            Assert.Equal($"<div class=\"form-control is-display\">{dt:yyyyMMddhhmmss}</div>", cut.Markup);
        });
    }

    [Fact]
    public void CreateComponentByFieldType_Ok()
    {
        var editor = new MockNullDisplayNameColumn("Name", typeof(string)) { Readonly = true };
        var fragment = new RenderFragment(builder => builder.CreateComponentByFieldType(new BootstrapBlazorRoot(), editor, new Foo() { Name = "Test-Component" }));
        var cut = Context.Render(builder => builder.AddContent(0, fragment));
        Assert.Contains("value=\"Test-Component\"", cut.Markup);
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
        var dn = Utility.GetDisplayName<Foo>(nameof(Foo.Count));
        Assert.Equal("数量", dn);

        // 动态类
        dn = Utility.GetDisplayName(context.GetItems().First(), nameof(Foo.Count));
        Assert.Equal("数量", dn);

        // 静态类
        dn = Utility.GetDisplayName<Foo>(nameof(Foo.Education));
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

        dn = Utility.GetDisplayName<TestEnum>(nameof(TestEnum.Name));
        Assert.Equal("Test-Enum-Name", dn);

        dn = Utility.GetDisplayName<TestEnum>(nameof(TestEnum.Address));
        Assert.Equal("Test-Enum-Address", dn);

        dn = Utility.GetDisplayName<TestEnum?>(nameof(TestEnum.Name));
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
    public void GetJsonStringByTypeName_Ok()
    {
        // improve code coverage
        var option = Context.Services.GetRequiredService<IOptions<JsonLocalizationOptions>>().Value;
        Assert.Empty(Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "UnitTest.Utils.UtilityTest+Cat1", null, true));

        // dynamic
        var dynamicType = EmitHelper.CreateTypeByName("test_type", new InternalTableColumn[] { new("Name", typeof(string)) });
        Assert.Empty(Utility.GetJsonStringByTypeName(option, dynamicType!.Assembly, "Test"));

        // empty cultureName
        Assert.Empty(Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "UnitTest.Utils.UtilityTest+Cat1", "", false));
    }

    [Fact]
    public void GetJsonStringByTypeName_UseKeyWhenValueIsNull()
    {
        // improve code coverage
        var option = Context.Services.GetRequiredService<IOptions<JsonLocalizationOptions>>().Value;
        option.UseKeyWhenValueIsNull = true;
        var items = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "UnitTest.Utils.UtilityTest", "en-US", true);

        var test1 = items.FirstOrDefault(i => i.Name == "Test-Null");
        Assert.NotNull(test1);
        Assert.Equal("", test1.Value);

        var test2 = items.FirstOrDefault(i => i.Name == "Test-Key");
        Assert.NotNull(test2);
        Assert.Equal("Test-Key", test2.Value);

        option.UseKeyWhenValueIsNull = false;
        items = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "UnitTest.Utils.UtilityTest", "en-US", true);

        test1 = items.FirstOrDefault(i => i.Name == "Test-Null");
        Assert.NotNull(test1);
        Assert.Equal("", test1.Value);

        test2 = items.FirstOrDefault(i => i.Name == "Test-Key");
        Assert.NotNull(test2);
        Assert.Equal("", test2.Value);
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

        var model = new DynamicColumnsObject();
        Utility.SetPropertyValue<object, object>(model, "Name", "Test-Value");
        Assert.Equal("Test-Value", Utility.GetPropertyValue(model, "Name"));
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

        var items = new List<SelectedItem>() { new("Test1", "Test1"), new("Test2", "Test2") };
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

        cols = Utility.GenerateEditorItems<Foo>(new InternalTableColumn[]
        {
            new("Name", typeof(string)) { Text = "test-Name" }
        });
        Assert.Equal("test-Name", cols.First(i => i.GetFieldName() == "Name").Text);
    }

    [Fact]
    public void GenerateTableColumns_Ok()
    {
        var cols = Utility.GetTableColumns<Cat>(new InternalTableColumn[]
        {
            new(nameof(Cat.Name), typeof(string)) { Text = "test-Name", LookupServiceData = true, IsVisibleWhenAdd = false, IsVisibleWhenEdit = false }
        });
        Assert.Equal(2, cols.Count());
        Assert.Equal(true, cols.First().LookupServiceData);
        Assert.False(cols.First().IsVisibleWhenAdd);
        Assert.False(cols.First().IsVisibleWhenEdit);
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
        var dynamicType = EmitHelper.CreateTypeByName("test_type", new InternalTableColumn[] { new("Name", typeof(string)) });
        localizer = Utility.CreateLocalizer(dynamicType!);
        Assert.Null(localizer);
    }

    [Fact]
    public void GetStringLocalizerFromService_Ok()
    {
        // 动态程序集
        var dynamicType = EmitHelper.CreateTypeByName("test-Type", new InternalTableColumn[] { new("Name", typeof(string)) });
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
        var localizedStrings = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Server.Data.Foo", "zh-CN", true);
        var localizer = localizedStrings.First(i => i.Name == "Name");
        Assert.Equal("Test-Name", localizer.Value);
        Assert.False(localizer.ResourceNotFound);

        // Value is null
        localizer = localizedStrings.First(i => i.Name == "NullName");
        Assert.Equal("", localizer.Value);
        Assert.False(localizer.ResourceNotFound);

        localizer = localizedStrings.First(i => i.Name == "EmptyName");
        Assert.Equal("", localizer.Value);
        Assert.False(localizer.ResourceNotFound);
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
        var configs = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Server.Data.Foo", "it-it", true);
        Assert.Empty(configs);
    }

    [Fact]
    public void GetJsonStringConfig_Culture()
    {
        // 回落默认语音为 en 测试用例为 en-US 找不到资源文件
        var option = new JsonLocalizationOptions();
        var configs = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Server.Data.Foo", "en-US", true);
        Assert.NotEmpty(configs);

        var pi = option.GetType().GetProperty("EnableFallbackCulture", BindingFlags.NonPublic | BindingFlags.Instance);
        pi!.SetValue(option, false);
        configs = Utility.GetJsonStringByTypeName(option, this.GetType().Assembly, "BootstrapBlazor.Server.Data.Foo", "en", true);

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

    [Fact]
    public void GetTableColumns_Ok()
    {
        var columns = new InternalTableColumn[]
        {
            new("Name3", typeof(string)),
        };
        var cols = Utility.GetTableColumns<Dog>(columns).ToList();
        Assert.Equal(3, cols.Count);
    }

    [Fact]
    public void GetTableColumnsWithMetadata_Ok()
    {
        TableMetadataTypeService.RegisterMetadataTypes(typeof(Pig).Assembly);
        TableMetadataTypeService.RegisterMetadataType(typeof(PigMetadata), typeof(Pig));
        var cols = Utility.GetTableColumns<Pig>().ToList();
        Assert.Single(cols);
    }

    [Fact]
    public void FormatIp_Test()
    {
        var result = "192.168.1.192".MaskIpString();
        Assert.Equal("192.168.1.###", result);
    }

    [AutoGenerateClass(Align = Alignment.Center)]
    private class Dog
    {
        public string? Name1 { get; set; }

        [AutoGenerateColumn(Align = Alignment.Center, Order = -2)]
        public string? Name2 { get; set; }
    }

    [TableMetadataFor(typeof(Pig))]
    [AutoGenerateClass(Align = Alignment.Center)]
    private class PigMetadata
    {
        [AutoGenerateColumn(Ignore = true)]
        public string? Name1 { get; set; }

        [AutoGenerateColumn(Align = Alignment.Center, Order = -2)]
        public string? Name2 { get; set; }

        /// <summary>
        /// for test
        /// </summary>
        public static string? StaticProperty1 { get; set; }
    }

    private class Pig
    {
        public string? Name1 { get; set; }

        public string? Name2 { get; set; }
    }

    private class MockNullDisplayNameColumn(string fieldName, Type propertyType) : InternalTableColumn(fieldName, propertyType), IEditorItem
    {
        string IEditorItem.GetDisplayName() => null!;
    }

    private class Cat
    {
        [PlaceHolder("Test-PlaceHolder")]
        [Description("Cat-Desc")]
        public string? Name { get; set; }

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

    [AttributeUsage(AttributeTargets.Property)]
    private class CatKeyAttribute : Attribute
    {

    }
}
