// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Diagnostics.Runtime.Utilities;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnitTest.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
public class Benchmarks
{
    static readonly Foo _instance = new();

    static readonly FieldInfo _privateField = typeof(Foo).GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic)!;

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_name")]
    static extern ref string GetNameValue(Foo @this);

    [Benchmark]
    public object? Reflection()
    {
        var fieldInfo = typeof(Foo).GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic)!;
        return fieldInfo.GetValue(_instance);
    }

    [Benchmark]
    public object? ReflectionWithCache() => _privateField.GetValue(_instance);

    [Benchmark]
    public string UnsafeAccessor() => GetNameValue(_instance);

    [Benchmark]
    public string DirectAccess() => _instance.GetName();

    [Benchmark]
    public string Lambda() => GetFieldValue();

    [Benchmark]
    public string LambdaWithCache() => GetFooFieldFunc(_instance);

    public string GetFieldValue()
    {
        var method = GetFooFieldExpression().Compile();
        return method.Invoke(_instance);
    }

    private static Func<Foo, string> GetFooFieldFunc = GetFooFieldExpression().Compile();

    static Expression<Func<Foo, string>> GetFooFieldExpression()
    {
        var param_p1 = Expression.Parameter(typeof(Foo));
        var body = Expression.Field(param_p1, _privateField);
        return Expression.Lambda<Func<Foo, string>>(Expression.Convert(body, typeof(string)), param_p1);
    }
}
