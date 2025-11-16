// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

internal static class FooExtensions
{
    public static object GenerateValueExpression(this Foo model, string fieldName = nameof(Foo.Name), Type? fieldType = null) => Utility.GenerateValueExpression(model, fieldName, fieldType ?? typeof(string));
}
