// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics;

namespace UnitTest.Performance;

public class ReflectionTest
{
    private ITestOutputHelper Logger { get; }

    private int Count { get; } = 1000;

    public ReflectionTest(ITestOutputHelper logger)
    {
        Logger = logger;
    }

    [Fact]
    public void GetProperty()
    {
        var s1 = new Dummy()
        {
            Name = "Argo"
        };

        var pi = s1.GetType().GetProperty("Name");
        Assert.NotNull(pi);

        if (pi != null)
        {
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < Count; i++)
            {
                pi.GetValue(s1);
            }
            sw.Stop();
            Logger.WriteLine($"Reflection: {sw.Elapsed}");

            var invoker = LambdaExtensions.GetPropertyValueLambda<Dummy, string>(s1, "Name").Compile();
            sw = Stopwatch.StartNew();
            for (var i = 0; i < Count; i++)
            {
                invoker(s1);
            }
            sw.Stop();
            Logger.WriteLine($"Expression: {sw.Elapsed}");
        }
    }

    [Fact]
    public void SetProperty()
    {
        var s1 = new Dummy()
        {
            Name = "Argo"
        };

        var sw = Stopwatch.StartNew();
        var pi = s1.GetType().GetProperty("Name");
        Assert.NotNull(pi);

        for (var i = 0; i < Count; i++)
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
        for (var i = 0; i < Count; i++)
        {
            invoker(s1, "Dummy");
        }
        sw.Stop();
        Logger.WriteLine($"Expression: {sw.Elapsed}");
    }

    [Fact]
    public void InvokeMethod()
    {
        var s1 = new Dummy()
        {
            Name = "Argo"
        };
        var mi = s1.GetType().GetMethod("Method");
        Assert.NotNull(mi);

        var sw = Stopwatch.StartNew();
        for (var i = 0; i < Count; i++)
        {
            if (mi != null)
            {
                mi.Invoke(s1, null);
            }
        }
        sw.Stop();
        Logger.WriteLine($"Reflection: {sw.Elapsed}");

        var invoker = LambdaExtensions.GetPropertyValueLambda<Dummy, string>(s1, "Name").Compile();
        sw = Stopwatch.StartNew();
        for (var i = 0; i < Count; i++)
        {
            invoker.Invoke(s1);
        }
        sw.Stop();
        Logger.WriteLine($"Expression: {sw.Elapsed}");
    }

    delegate string DummyCallback<TModel>(TModel dummy);
    [Fact]
    public void Delegate_Test()
    {
        var test = new Dummy { Name = "Test" };
        var invoker = LambdaExtensions.GetPropertyValueLambda<Dummy, string>(test, "Name").Compile();
        var stopWatch = Stopwatch.StartNew();
        for (int i = 0; i < Count; i++)
        {
            invoker(test);
        }
        stopWatch.Stop();
        Logger.WriteLine($"Expression: {stopWatch.Elapsed}");

        var objectType = test.GetType();
        var method = objectType.GetProperty("Name")?.GetGetMethod(false);
        if (method != null)
        {
            var proxy = (DummyCallback<Dummy>)Delegate.CreateDelegate(typeof(DummyCallback<Dummy>), method);
            stopWatch.Restart();
            for (int i = 0; i < Count; i++)
            {
                proxy(test);
            }
        }
        stopWatch.Stop();
        Logger.WriteLine($"Delegate: {stopWatch.Elapsed}");
    }

    private class Dummy
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? Method()
        {
            return Name;
        }
    }
}
