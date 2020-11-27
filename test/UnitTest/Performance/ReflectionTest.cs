// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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

            var invoker = s1.GetPropertyValueLambda<Dummy, string>("Name").Compile();
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
            var invoker = s1.SetPropertyValueLambda<Dummy, object>("Name").Compile();
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
