// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Utils;

public class GroupTest(ITestOutputHelper logger)
{
    ITestOutputHelper Logger { get; set; } = logger;

    [Fact]
    public void Group_Order_Ok()
    {
        var results = new List<string>();

        var items = new List<InternalTableColumn>(50)
        {
            new("Test1", typeof(string)) { GroupName = "Test1", GroupOrder = 2, Order = 2  },
            new("Test2", typeof(string)) { GroupName = "Test1", GroupOrder = 2, Order = 1  },
            new("Test3", typeof(string)) { GroupName = "Test2", GroupOrder = 1, Order = 2  },
            new("Test4", typeof(string)) { GroupName = "Test2", GroupOrder = 1, Order = 1 }
        };
        var groups = items.GroupBy(i => i.GroupOrder).OrderBy(i => i.Key).Select(i => new { i.Key, Items = i.OrderBy(x => x.Order) });
        foreach (var g in groups)
        {
            foreach (var item in g.Items)
            {
                results.Add(item.GetFieldName());
                Logger.WriteLine($"{item.GetFieldName()} - {item.GroupName} - {item.GroupOrder}");
            }
        }

        var expected = string.Join(",", new List<string>() { "Test4", "Test3", "Test2", "Test1" });
        var actual = string.Join(",", results);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OrderBy_Ok()
    {
        var items = new List<int> { 0, -1, 1, -4 };
        var actual = items.OrderByDescending(i => i);
        Assert.Equal("1, 0, -1, -4", string.Join(", ", actual));

        items = [1, 3, 2, 4];
        actual = items.OrderBy(i => i);
        Assert.Equal("1, 2, 3, 4", string.Join(", ", actual));

        items = [-1, -3, -2, -4];
        actual = items.OrderBy(i => i);
        Assert.Equal("-4, -3, -2, -1", string.Join(", ", actual));

        items = [2, 1, 0, -1, -3, -2, -4];
        actual = items.OrderBy(i => i);
        Assert.Equal("-4, -3, -2, -1, 0, 1, 2", string.Join(", ", actual));
    }
}
