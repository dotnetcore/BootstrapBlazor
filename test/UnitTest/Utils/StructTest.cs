// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Utils;

/// <summary>
/// 
/// </summary>
public class StructTest
{
    [Fact]
    public void KeyValue_Test()
    {
        var cache = new List<KeyValuePair<string, object>>
            {
                new("1", 12)
            };
        var c = cache.FirstOrDefault(i => i.Key == "12");
        Assert.Null(c.Key);
    }

    [Fact]
    public void Struct_Test()
    {
        var cache = new List<(string, object)>
            {
                ("1", 12)
            };
        var c = cache.FirstOrDefault(i => i.Item1 == "12");
        Assert.Null(c.Item1);
    }
}
