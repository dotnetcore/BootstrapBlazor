// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest.Performance
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionTest
    {
        private readonly ITestOutputHelper Logger;
        /// <summary>
        /// 
        /// </summary>
        public ReflectionTest(ITestOutputHelper logger)
        {
            Logger = logger;
        }

        [Fact]
        public void GetProperty()
        {
            var count = 10000;
            var s1 = new Dummy()
            {
                Name = "Argo"
            };

            var pi = s1.GetType().GetProperty("Name");
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                pi.GetValue(s1);
            }
            sw.Stop();
            Logger.WriteLine($"Reflection: {sw.Elapsed}");

            var invoker = LambdaExtensions.GetPropertyValueLambda<Dummy, string>(s1, "Name").Compile();
            sw = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                invoker(s1);
            }
            sw.Stop();
            Logger.WriteLine($"Expression: {sw.Elapsed}");
        }

        [Fact]
        public void SetProperty()
        {
            var count = 10000;
            var s1 = new Dummy()
            {
                Name = "Argo"
            };

            var sw = Stopwatch.StartNew();
            var pi = s1.GetType().GetProperty("Name");
            for (var i = 0; i < count; i++)
            {
                if (pi != null && pi.CanWrite)
                {
                    if ((Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType) == typeof(string) && pi.CanWrite)
                    {
                        pi.SetValue(s1, "Dummy");
                    }
                }
            }
            sw.Stop();
            Logger.WriteLine($"Reflection: {sw.Elapsed}");

            sw = Stopwatch.StartNew();
            var invoker = LambdaExtensions.SetPropertyValueLambda<Dummy, object>(s1, "Name").Compile();
            for (var i = 0; i < count; i++)
            {
                invoker(s1, "Dummy");
            }
            sw.Stop();
            Logger.WriteLine($"Expression: {sw.Elapsed}");
        }

        [Fact]
        public void InvokeMethod()
        {
            var count = 10000;
            var s1 = new Dummy()
            {
                Name = "Argo"
            };
            var mi = s1.GetType().GetMethod("Method");

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                if (mi != null)
                {
                    mi.Invoke(s1, null);
                }
            }
            sw.Stop();
            Logger.WriteLine($"Reflection: {sw.Elapsed}");

            var target = Expression.Parameter(typeof(Dummy));
            Expression expression = Expression.Call(target, mi);
            var func = Expression.Lambda<Func<Dummy, string>>(expression, target).Compile();

            sw = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                func.Invoke(s1);
            }
            sw.Stop();
            Logger.WriteLine($"Expression: {sw.Elapsed}");
        }

        delegate string DummyCallback<TModel>(TModel dummy);
        [Fact]
        public void Delegate_Test()
        {
            var test = new Dummy { Name = "Test" };
            var count = 10000000;
            var obj = LambdaExtensions.GetPropertyValue(test, "Name");
            var stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                LambdaExtensions.GetPropertyValue(test, "Name");
            }
            stopWatch.Stop();
            var interval = stopWatch.ElapsedMilliseconds;

            var objectType = test.GetType();
            var method = objectType.GetProperty("Name")?.GetGetMethod(false);
            var proxy = (DummyCallback<Dummy>)Delegate.CreateDelegate(typeof(DummyCallback<Dummy>), method);
            obj = proxy(test);

            stopWatch.Restart();
            for (int i = 0; i < count; i++)
            {
                proxy(test);
            }
            stopWatch.Stop();
            var interval2 = stopWatch.ElapsedMilliseconds;
        }

        private class Dummy
        {
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string Method()
            {
                return Name;
            }
        }
    }
}
