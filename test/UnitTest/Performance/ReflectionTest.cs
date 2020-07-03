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
        private ITestOutputHelper Logger;
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
            for (int i = 0; i < count; i++)
            {
                pi.GetValue(s1);
            }
            sw.Stop();
            Logger.WriteLine($"Reflection: {sw.Elapsed}");

            var invoker = s1.GetPropertyValueLambda<Dummy, string>("Name").Compile();
            sw = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
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
            for (int i = 0; i < count; i++)
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
            for (int i = 0; i < count; i++)
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
            for (int i = 0; i < count; i++)
            {
                if (mi != null)
                {
                    mi.Invoke(s1, null);
                }
            }
            sw.Stop();
            Logger.WriteLine($"Reflection: {sw.Elapsed}");

            ParameterExpression target = Expression.Parameter(typeof(Dummy));
            Expression expression = Expression.Call(target, mi);
            var func = Expression.Lambda<Func<Dummy, string>>(expression, target).Compile();

            sw = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                func.Invoke(s1);
            }
            sw.Stop();
            Logger.WriteLine($"Expression: {sw.Elapsed}");
        }

        class Dummy
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
