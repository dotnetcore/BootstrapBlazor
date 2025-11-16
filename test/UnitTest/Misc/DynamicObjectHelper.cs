// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest;

internal static class DynamicObjectHelper
{
    public static Type CreateDynamicType(string typeName = "Test")
    {
        var cols = new InternalTableColumn[]
        {
            new("Id", typeof(int)),
            new("Name", typeof(string))
        };

        // 创建动态类型基类是 DynamicObject
        var instanceType = EmitHelper.CreateTypeByName(typeName, cols, typeof(DynamicObject));
        return instanceType!;
    }

    public static object CreateDynamicObject(string typeName = "Test")
    {
        var instanceType = CreateDynamicType(typeName);

        // 创建动态类型实例
        return Activator.CreateInstance(instanceType)!;
    }
}
