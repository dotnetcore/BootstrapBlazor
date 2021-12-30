// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using System;

namespace UnitTest.Extensions;

internal static class FooExtensions
{
    public static object GenerateValueExpression(this Foo model, string fieldName = nameof(Foo.Name), Type? fieldType = null) => Utility.GenerateValueExpression(model, fieldName, fieldType ?? typeof(string));
}
