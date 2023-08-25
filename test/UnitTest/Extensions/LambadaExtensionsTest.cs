// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq.Expressions;

namespace UnitTest.Extensions;

public class LambadaExtensionsTest
{
    [Fact]
    public void GetFilterFunc_Null()
    {
        var foos = new Foo[]
        {
            new() { Count = 1 },
            new() { Count = 2 },
            new() { Count = 10 },
            new() { Count = 11 }
        };
        var filter = Array.Empty<MockFilterActionBase>();
        var items = foos.Where(filter.GetFilterFunc<Foo>());
        Assert.Equal(4, items.Count());
    }

    [Fact]
    public void GetFilterLambda_Nullable()
    {
        var foos = new Foo[]
        {
            new() { Count = 1 },
            new() { Count = 2 },
            new() { Count = 10 },
            new() { Count = 11 }
        };
        var filter = new FilterKeyValueAction()
        {
            FieldKey = "DateTime",
            FilterAction = FilterAction.NotEqual,
            FieldValue = DateTime.MinValue
        };
        var items = foos.Where(filter.GetFilterFunc<Foo>());
        Assert.Empty(items);
    }

    [Fact]
    public void GetFilterLambda_Filter()
    {
        var foos = new Foo[]
        {
            new() { Count = 1 },
            new() { Count = 2 },
            new() { Count = 10 },
            new() { Count = 11 }
        };
        var filter = new FilterKeyValueAction()
        {
            Filters = new()
            {
                new FilterKeyValueAction()
                {
                    FilterLogic = FilterLogic.Or,
                    Filters = new List<FilterKeyValueAction>()
                    {
                        new FilterKeyValueAction() { FieldKey = "Count", FilterAction = FilterAction.Equal, FieldValue = 1 },
                        new FilterKeyValueAction() { FieldKey = "Count", FilterAction = FilterAction.Equal, FieldValue = 2 }
                    }
                },
                new FilterKeyValueAction() { FieldKey = "Count", FilterAction = FilterAction.GreaterThan, FieldValue = 1 },
                new FilterKeyValueAction() { FieldKey = "Count", FilterAction = FilterAction.LessThan, FieldValue = 10 }
            }
        };
        var items = foos.Where(filter.GetFilterFunc<Foo>());
        Assert.Single(items);
    }

    [Fact]
    public void GetFilterLambda_Enum()
    {
        var filters = new FilterKeyValueAction() { FieldKey = nameof(Dummy.Education), FieldValue = "Middle" };
        var exp = filters.GetFilterLambda<Dummy>();
        Assert.True(exp.Compile().Invoke(new Dummy() { Education = EnumEducation.Middle }));
    }

    [Fact]
    public void GetFilterLambda_And()
    {
        var foos = new Foo[]
        {
            new() { Count = 1 },
            new() { Count = 2 },
            new() { Count = 10 },
            new() { Count = 11 }
        };
        var filter = new MockFilterActionBase[]
        {
            new MockAndFilterAction1(),
            new MockAndFilterAction2()
        };
        var items = foos.Where(filter.GetFilterFunc<Foo>());
        Assert.Single(items);
    }

    [Fact]
    public void GetFilterLambda_Or()
    {
        var foos = new Foo[]
        {
            new() { Count = 1 },
            new() { Count = 2 },
            new() { Count = 10 },
            new() { Count = 11 }
        };
        var filter = new MockFilterActionBase[]
        {
            new MockOrFilterAction1(),
            new MockOrFilterAction2()
        };
        var items = foos.Where(filter.GetFilterFunc<Foo>(FilterLogic.Or));
        Assert.Equal(3, items.Count());
    }

    [Fact]
    public void FilterKeyValueAction_FieldName_Null()
    {
        // FieldValue 为 null 时 均返回 true
        var filter = new FilterKeyValueAction() { FieldKey = "", FieldValue = 1 };
        var invoker = filter.GetFilterLambda<Foo>().Compile();

        // 符合条件
        Assert.True(invoker.Invoke(new Foo()));
        Assert.True(invoker.Invoke(new Foo() { Name = "Test" }));
    }

    [Fact]
    public void FilterKeyValueAction_FieldValue_Null()
    {
        // FieldValue 为 null 时 均返回 true
        var filter = new FilterKeyValueAction() { FieldKey = "Name", FieldValue = null };
        var invoker = filter.GetFilterLambda<Foo>().Compile();

        // 符合条件
        Assert.True(invoker.Invoke(new Foo()));
        Assert.True(invoker.Invoke(new Foo() { Name = "Test" }));
    }

    [Fact]
    public void FilterKeyValueAction_SimpleFilterExpression()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Name", FieldValue = "Name" };
        var invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.True(invoker.Invoke(new Foo() { Name = "Name" }));
        Assert.False(invoker.Invoke(new Foo() { Name = "Name1" }));
    }

    [Fact]
    public void FilterKeyValueAction_SimpleFilterExpression_Exception()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Name", FieldValue = "Name" };
        Assert.Throws<InvalidOperationException>(() => filter.GetFilterLambda<Dummy>());
    }

    [Fact]
    public void FilterKeyValueAction_ComplexFilterExpression()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Foo.Name", FieldValue = "Name" };
        var invoker = filter.GetFilterLambda<Dummy>().Compile();
        Assert.True(invoker.Invoke(new Dummy() { Foo = new Foo() { Name = "Name" } }));
    }

    [Fact]
    public void FilterKeyValueAction_ComplexFilterExpression_Exception()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Foo.TestName", FieldValue = "Name" };
        Assert.Throws<InvalidOperationException>(() => filter.GetFilterLambda<Dummy>());

        filter = new FilterKeyValueAction() { FieldKey = "Foo1.TestName", FieldValue = "Name" };
        Assert.Throws<InvalidOperationException>(() => filter.GetFilterLambda<Dummy>());
    }

    [Fact]
    public void FilterKeyValueAction_ComplexFilterExpression_Nullable()
    {
        // 搜索条件为 DateTime.Now
        var filter = new FilterKeyValueAction() { FieldKey = "Foo.DateTime", FieldValue = DateTime.Now };
        var invoker = filter.GetFilterLambda<Dummy>().Compile();

        // 均不符合条件
        Assert.False(invoker.Invoke(new Dummy() { Foo = new Foo() { DateTime = DateTime.MinValue } }));
        Assert.False(invoker.Invoke(new Dummy() { Foo = new Foo() { DateTime = null } }));

        // 搜索条件为 Null
        filter = new FilterKeyValueAction() { FieldKey = "Foo.DateTime", FieldValue = null };
        invoker = filter.GetFilterLambda<Dummy>().Compile();

        // 均符合条件
        Assert.True(invoker.Invoke(new Dummy() { Foo = new Foo() { DateTime = DateTime.MinValue } }));
        Assert.True(invoker.Invoke(new Dummy() { Foo = new Foo() { DateTime = null } }));
    }

    [Fact]
    public void FilterKeyValueAction_ComplexFilterExpression_Enum()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Cat.Education", FieldValue = "Middle" };
        var invoker = filter.GetFilterLambda<Dummy>().Compile();
        Assert.True(invoker.Invoke(new Dummy() { Cat = new Cat() { Education = EnumEducation.Middle } }));
    }

    [Fact]
    public void GetExpression_NotEqual()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Count", FieldValue = 1, FilterAction = FilterAction.NotEqual };
        var invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.True(invoker.Invoke(new Foo() { Count = 2 }));
    }

    [Fact]
    public void GetExpression_GreaterThanOrEqual()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Count", FieldValue = 10, FilterAction = FilterAction.GreaterThanOrEqual };
        var invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.False(invoker.Invoke(new Foo() { Count = 9 }));
        Assert.True(invoker.Invoke(new Foo() { Count = 10 }));
        Assert.True(invoker.Invoke(new Foo() { Count = 11 }));
    }

    [Fact]
    public void GetExpression_LessThanOrEqual()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Count", FieldValue = 10, FilterAction = FilterAction.LessThanOrEqual };
        var invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.True(invoker.Invoke(new Foo() { Count = 9 }));
        Assert.True(invoker.Invoke(new Foo() { Count = 10 }));
        Assert.False(invoker.Invoke(new Foo() { Count = 11 }));
    }

    [Fact]
    public void GetExpression_Contains()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Name", FieldValue = "test", FilterAction = FilterAction.Contains };
        var invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.True(invoker.Invoke(new Foo() { Name = "1test1" }));
    }

    [Fact]
    public void GetExpression_NotContains()
    {
        var filter = new FilterKeyValueAction() { FieldKey = "Name", FieldValue = "test", FilterAction = FilterAction.NotContains };
        var invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.True(invoker.Invoke(new Foo() { Name = "11" }));
    }

    [Fact]
    public void GetExpression_CustomPredicate()
    {
        var foo = new Foo() { Name = "11" };
        var val = new Func<string, bool>(p1 => p1 == foo.Name);
        var filter = new FilterKeyValueAction() { FieldKey = "Name", FieldValue = val, FilterAction = FilterAction.CustomPredicate };
        var invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.True(invoker.Invoke(foo));

        // Expression
        Expression<Func<string, bool>> p1 = p => p == foo.Name;
        filter.FieldValue = p1;
        invoker = filter.GetFilterLambda<Foo>().Compile();
        Assert.True(invoker.Invoke(foo));

        filter.FieldValue = new object();
        Assert.Throws<InvalidOperationException>(() => filter.GetFilterLambda<Foo>());
    }

    [Fact]
    public void Sort_Queryable()
    {
        var foos = new List<Foo>
        {
            new Foo { Name = "10", Count = 10 },
            new Foo { Name = "10", Count = 20 },
            new Foo { Name = "20", Count = 20 },
        }.AsQueryable();
        var orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Count desc", "Name" });
        Assert.Equal(20, orderFoos.ElementAt(0).Count);
        Assert.Equal("20", orderFoos.ElementAt(1).Name);

        orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Unset);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Desc);
        Assert.Equal(20, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Test", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);
    }

    [Fact]
    public void Sort_Enumerable()
    {
        var foos = new List<Foo>
        {
            new Foo { Name = "10", Count = 10 },
            new Foo { Name = "10", Count = 20 },
            new Foo { Name = "20", Count = 20 },
        };
        var orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Unset);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Desc);
        Assert.Equal(20, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Test", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Count desc", "Name" });
        Assert.Equal(20, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Count", "Name desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Count", "Test desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Count);
    }

    [Fact]
    public void Sort_Queryable_Enumerable()
    {
        var foos = new List<Foo>
        {
            new Foo { Name = "10", Count = 10 },
            new Foo { Name = "10", Count = 20 },
            new Foo { Name = "20", Count = 20 },
        }.AsQueryable();
        var orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Unset);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Desc);
        Assert.Equal(20, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Count", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, "Test", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Count desc", "Name" });
        Assert.Equal(20, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Count", "Name desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Count", "Test desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Count);
    }

    [Fact]
    public void Sort_Complex()
    {
        var foos = new List<Dummy>
        {
            new() { Foo = new() { Name = "10", Count = 10 } },
            new() { Foo = new() { Name = "10", Count = 20 } },
            new() { Foo = new() { Name = "20", Count = 20 } }
        };
        var orderFoos = LambdaExtensions.Sort(foos, "Foo.Count", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, "Foo.Count", SortOrder.Desc);
        Assert.Equal(20, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, "Foo1.Count", SortOrder.Desc);
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Foo.Count desc", "Foo.Name" });
        Assert.Equal(20, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Foo.Count", "Foo.Name Desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Foo.Count", "Foo.Test Desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);
    }

    [Fact]
    public void Sort_Queryable_Complex()
    {
        var foos = new List<Dummy>
        {
            new() { Foo = new() { Name = "10", Count = 10 } },
            new() { Foo = new() { Name = "10", Count = 20 } },
            new() { Foo = new() { Name = "20", Count = 20 } }
        }.AsQueryable();
        var orderFoos = LambdaExtensions.Sort(foos, "Foo.Count", SortOrder.Asc);
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, "Foo.Count", SortOrder.Desc);
        Assert.Equal(20, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, "Foo1.Count", SortOrder.Desc);
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Foo.Count desc", "Foo.Name" });
        Assert.Equal(20, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Foo.Count", "Foo.Name Desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);

        orderFoos = LambdaExtensions.Sort(foos, new List<string>() { "Foo.Count", "Foo.Test Desc" });
        Assert.Equal(10, orderFoos.ElementAt(0).Foo!.Count);
    }

    [Fact]
    public void GetPropertyValueLambda_Null()
    {
        Foo? foo = null;
        Assert.Throws<ArgumentNullException>(() => LambdaExtensions.GetPropertyValueLambda<object?, string>(foo, "Name"));
    }

    [Fact]
    public void GetPropertyValueLambda_Dynamic()
    {
        var foo = new CustomDynamicData(new Dictionary<string, string>() { ["Name"] = "Test1" });
        var invoker = LambdaExtensions.GetPropertyValueLambda<CustomDynamicData, string>(foo, "Name").Compile();
        var t = invoker(foo);
        Assert.Equal("Test1", t);
    }

    [Fact]
    public void SetPropertyValueLambda_Null()
    {
        Foo? foo = null;
        Assert.Throws<ArgumentNullException>(() => LambdaExtensions.SetPropertyValueLambda<object?, string>(foo, "Name"));

        foo = new Foo() { Name = "Test1" };
        Assert.Throws<InvalidOperationException>(() => LambdaExtensions.SetPropertyValueLambda<Foo, string>(foo, "Test1"));

        var dummy = new Dummy() { Foo = foo };
        var invoker1 = LambdaExtensions.SetPropertyValueLambda<Dummy, string>(dummy, "Foo.Name").Compile();
        Assert.Throws<InvalidOperationException>(() => LambdaExtensions.SetPropertyValueLambda<Dummy, string>(dummy, "Foo.Test1"));
    }

    [Fact]
    public void GetPropertyValueLambda_Ok()
    {
        var foo = new Foo() { Name = "Test1" };
        var invoker = LambdaExtensions.GetPropertyValueLambda<Foo, string>(foo, "Name").Compile();
        Assert.Equal("Test1", invoker(foo));
        Assert.Throws<InvalidOperationException>(() => LambdaExtensions.GetPropertyValueLambda<Foo, string>(foo, "Test1"));

        var dummy = new Dummy() { Foo = foo };
        var invoker1 = LambdaExtensions.GetPropertyValueLambda<Dummy, string>(dummy, "Foo.Name").Compile();
        Assert.Equal("Test1", invoker1(dummy));
        Assert.Throws<InvalidOperationException>(() => LambdaExtensions.GetPropertyValueLambda<Dummy, string>(dummy, "Foo.Test1"));
    }

    [Fact]
    public void GetKeyValue_Ok()
    {
        var foo1 = new Dog() { Id = 123, Name = "Test", Age = 20 };
        var foo2 = new Dog() { Id = 234, Name = "Test", Age = 20 };

        var invoker1 = LambdaExtensions.GetKeyValue<Dog?, int>().Compile();
        Assert.Equal(123, invoker1(foo1));

        var invoker2 = LambdaExtensions.GetKeyValue<Dog?, object>().Compile();
        Assert.Equal(123, invoker2(foo1));

        Assert.Throws<InvalidOperationException>(() => LambdaExtensions.GetKeyValue<Dog?, DateTime>());

        foo2.Id = 123;
        var invoker3 = LambdaExtensions.GetKeyValue<Dog?, object>(typeof(DogKeyAttribute)).Compile();
        Assert.Equal(invoker3(foo1), invoker3(foo2));

        foo1.Age = 40;
        Assert.NotEqual(invoker3(foo1), invoker3(foo2));
    }

    [Fact]
    public void GetKeyValue_Tuple()
    {
        var val = new Tuple<int, string, int>(1, "Test1", 2);
        var dog1 = new Dog()
        {
            Id = 1,
            Age = 2,
            Name = "Test1"
        };
        var invoker1 = LambdaExtensions.GetKeyValue<Dog, object>(typeof(DogKeyAttribute)).Compile();
        var val1 = invoker1(dog1);
        Assert.Equal(val, val1);

        var invoker2 = LambdaExtensions.GetKeyValue<Dog, Tuple<int, string, int>>(typeof(DogKeyAttribute)).Compile();
        var val2 = invoker1(dog1);
        Assert.Equal(val, val2);
    }

    [Fact]
    public void TryParse_Ok()
    {
        Func<int?, bool> func = _ => false;
        var exp = Expression.Parameter(typeof(int?));
        var pi = typeof(int?).GetProperty("HasValue");

        if (pi != null)
        {
            var exp_p = Expression.Property(exp, pi);
            func = Expression.Lambda<Func<int?, bool>>(exp_p, exp).Compile();
        }
        Assert.True(func.Invoke(10));
        Assert.False(func.Invoke(null));
    }

    private abstract class MockFilterActionBase : IFilterAction
    {
        public abstract FilterKeyValueAction GetFilterConditions();

        public virtual void Reset()
        {
        }

        public virtual Task SetFilterConditionsAsync(FilterKeyValueAction conditions)
        {
            return Task.CompletedTask;
        }
    }

    private class MockAndFilterAction1 : MockFilterActionBase
    {
        public override FilterKeyValueAction GetFilterConditions()
        {
            var filters = new FilterKeyValueAction()
            {
                Filters = new()
                {
                    new()
                    {
                         FieldKey = "Count",
                         FieldValue = 1,
                         FilterAction = FilterAction.GreaterThan,
                         FilterLogic = FilterLogic.And
                    },
                    new()
                    {
                         FieldKey = "Count",
                         FieldValue = 10,
                         FilterAction = FilterAction.LessThan
                    }
                }
            };
            return filters;
        }
    }

    private class MockAndFilterAction2 : MockFilterActionBase
    {
        public override FilterKeyValueAction GetFilterConditions()
        {
            var filters = new FilterKeyValueAction()
            {
                Filters = new()
                {
                    new()
                    {
                         FieldKey = "Count",
                         FieldValue = 2,
                         FilterAction = FilterAction.Equal,
                         FilterLogic = FilterLogic.And
                    }
                }
            };
            return filters;
        }
    }

    private class MockOrFilterAction1 : MockFilterActionBase
    {
        public override FilterKeyValueAction GetFilterConditions()
        {
            var filters = new FilterKeyValueAction()
            {
                FilterLogic = FilterLogic.Or,
                Filters = new()
                {
                    new()
                    {
                         FieldKey = "Count",
                         FieldValue = 1,
                         FilterAction = FilterAction.Equal
                    },
                    new()
                    {
                         FieldKey = "Count",
                         FieldValue = 2,
                         FilterAction = FilterAction.Equal
                    }
                }
            };
            return filters;
        }
    }

    private class MockOrFilterAction2 : MockFilterActionBase
    {
        public override FilterKeyValueAction GetFilterConditions()
        {
            var filters = new FilterKeyValueAction()
            {
                Filters = new()
                {
                    new()
                    {
                         FieldKey = "Count",
                         FieldValue = 10,
                         FilterAction = FilterAction.Equal
                    }
                }
            };
            return filters;
        }
    }

    private class CustomDynamicData : System.Dynamic.DynamicObject
    {
        /// <summary>
        /// 存储每列值信息 Key 列名 Value 为列值
        /// </summary>
        public Dictionary<string, string> Dynamic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fix"></param>
        /// <param name="data"></param>
        public CustomDynamicData(Dictionary<string, string> data)
        {
            Dynamic = data;
        }

        /// <summary>
        /// 
        /// </summary>
        public CustomDynamicData() : this(new()) { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (Dynamic.ContainsKey(binder.Name))
            {
                result = Dynamic[binder.Name];
            }
            else
            {
                // When property name not found, return empty
                result = "";
            }
            return true;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            var ret = false;
            var v = value?.ToString() ?? string.Empty;
            if (Dynamic.ContainsKey(binder.Name))
            {
                Dynamic[binder.Name] = v;
                ret = true;
            }
            return ret;
        }
    }

    private class Dummy
    {
        public Foo? Foo { get; set; }

        public EnumEducation Education { get; set; }

        public Cat? Cat { get; set; }
    }

    private class Cat
    {
        public EnumEducation Education { get; set; }
    }

    private class Dog
    {
        [DogKey]
        [Key]
        public int Id { get; set; }

        [DogKey]
        public string? Name { get; set; }

        [DogKey]
        public int Age { get; set; }
    }

    private class DogKeyAttribute : Attribute
    {

    }
}
