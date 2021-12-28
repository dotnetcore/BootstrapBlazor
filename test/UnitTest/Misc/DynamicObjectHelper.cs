// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System;

namespace UnitTest;

internal static class DynamicObjectHelper
{
    public static Type CreateDynamicType(string typeName = "Test")
    {
        var cols = new MockTableColumn[]
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
