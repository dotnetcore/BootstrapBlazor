// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System.Linq;
using Xunit;

namespace UnitTest.Emit;

public class TypeTest
{
    [Fact]
    public void CreateType_Ok()
    {
        // 创建动态类型基类是 DynamicObject
        var instanceType = DynamicObjectHelper.CreateDynamicType();
        Assert.NotNull(instanceType);
        Assert.Equal(typeof(DynamicObject), instanceType.BaseType);

        // 创建动态类型实例
        var instance = DynamicObjectHelper.CreateDynamicObject();
        Assert.NotNull(instance);

        var properties = instance.GetType().GetProperties().Select(p => p.Name);
        Assert.Contains(nameof(DynamicObject.DynamicObjectPrimaryKey), properties);

        // Utility
        var v = Utility.GetPropertyValue(instance, "Name");
        Assert.Null(v);
    }
}
