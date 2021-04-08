// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace UnitTest.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class LambdaTest
    {
        [Fact]
        public void TryParse_Ok()
        {
            var exp = Expression.Parameter(typeof(int?));
            var pi = typeof(int?).GetProperty("HasValue");
            var exp_p = Expression.Property(exp, pi);

            var func = Expression.Lambda<Func<int?, bool>>(exp_p, exp).Compile();
            var b = func.Invoke(10);
        }

        [Fact]
        public void NullContains_Ok()
        {
            var dummy = new Dummy();
            var filter = new FilterKeyValueAction
            {
                FieldKey = "Foo",
                FilterAction = FilterAction.Contains,
                FieldValue = ""
            };
            var invoker = filter.GetFilterLambda<Dummy>().Compile();
            var ret = invoker.Invoke(dummy);
            Assert.False(ret);
        }

        [Fact]
        public void GetPropertyValue_Test()
        {
            var dog = new Dog() { Foo = "test" };
            var v1 = LambdaExtensions.GetPropertyValue(dog, nameof(dog.Foo));
            Assert.Equal(dog.Foo, v1);

            var cat = new Cat() { Foo = 1 };
            var v2 = LambdaExtensions.GetPropertyValue(cat, nameof(cat.Foo));
            Assert.Equal(cat.Foo, v2);

            var fish = new Fish() { Foo = "test" };
            var v3 = LambdaExtensions.GetPropertyValue(fish, nameof(fish.Foo));
            Assert.Equal(fish.Foo, v3);

            var persian = new Persian() { Foo = 1 };
            var v4 = LambdaExtensions.GetPropertyValue(persian, nameof(persian.Foo));
            Assert.Equal(persian.Foo, v4);
        }

        [Fact]
        public void SetPropertyValue_Test()
        {
            var dog = new Dog() { Foo = "test" };
            var v1 = LambdaExtensions.SetPropertyValueLambda<Dog, string>(dog, nameof(dog.Foo)).Compile();
            v1.Invoke(dog, "test1");
            Assert.Equal("test1", dog.Foo);

            var cat = new Cat() { Foo = 1 };
            var v2 = LambdaExtensions.SetPropertyValueLambda<Cat, int>(cat, nameof(cat.Foo)).Compile();
            v2.Invoke(cat, 2);
            Assert.Equal(2, cat.Foo);

            var fish = new Fish() { Foo = "test" };
            var v3 = LambdaExtensions.SetPropertyValueLambda<Fish, string>(fish, nameof(fish.Foo)).Compile();
            v3.Invoke(fish, "test1");
            Assert.Equal("test1", fish.Foo);

            var persian = new Persian() { Foo = 1 };
            var v4 = LambdaExtensions.SetPropertyValueLambda<Persian, int>(persian, nameof(persian.Foo)).Compile();
            v4.Invoke(persian, 2);
            Assert.Equal(2, persian.Foo);
        }

        private class Dummy
        {
            public virtual string Foo { get; set; }
        }

        private class Dog : Dummy
        {
            public override string Foo { get; set; }
        }

        private class Cat : Dummy
        {
            public new int Foo { get; set; }
        }

        private class Fish : Dummy
        {
        }

        private class Persian : Cat
        {
        }
    }
}
