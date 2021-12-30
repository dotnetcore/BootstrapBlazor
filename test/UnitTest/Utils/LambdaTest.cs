// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace UnitTest.Utils;

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

        if (pi != null)
        {
            var exp_p = Expression.Property(exp, pi);

            var func = Expression.Lambda<Func<int?, bool>>(exp_p, exp).Compile();
            var b = func.Invoke(10);
        }
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

    private class Dummy
    {
        public virtual string? Foo { get; set; }
    }

    private class Dog : Dummy
    {
        public override string? Foo { get; set; }
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
