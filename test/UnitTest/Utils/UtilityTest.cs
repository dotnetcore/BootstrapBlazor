// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Utils
{
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
        public void GetPropertyValue_Ok()
        {
            var v = Utility.GetPropertyValue(Model, nameof(Foo.Name));
            Assert.Equal("Test", v);
        }

        [Fact]
        public void GetSortFunc_Ok()
        {
            var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
            var foos = Foo.GenerateFoo(localizer, 2);
            var invoker = Utility.GetSortFunc<Foo>();
            var data = invoker.Invoke(foos, nameof(Foo.Count), SortOrder.Asc).ToList();
            Assert.True(data[0].Count < data[1].Count);

            data = invoker.Invoke(foos, nameof(Foo.Count), SortOrder.Desc).ToList();
            Assert.True(data[0].Count > data[1].Count);
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

            public string? Id;
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
    }
}
