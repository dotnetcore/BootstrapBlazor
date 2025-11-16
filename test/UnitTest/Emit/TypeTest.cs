// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
