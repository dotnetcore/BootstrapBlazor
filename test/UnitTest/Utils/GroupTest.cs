// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Utils;

public class GroupTest
{
    ITestOutputHelper Logger { get; set; }

    public GroupTest(ITestOutputHelper logger) => Logger = logger;

    [Fact]
    public void Group_Order_Ok()
    {
        var results = new List<string>();

        var items = new List<MockTableColumn>(50)
        {
            new MockTableColumn("Test1", typeof(string)) { GroupName = "Test1", GroupOrder = 2, Order = 2  },
            new MockTableColumn("Test2", typeof(string)) { GroupName = "Test1", GroupOrder = 2, Order = 1  },
            new MockTableColumn("Test3", typeof(string)) { GroupName = "Test2", GroupOrder = 1, Order = 2  },
            new MockTableColumn("Test4", typeof(string)) { GroupName = "Test2", GroupOrder = 1, Order = 1 }
        };
        var groups = items.GroupBy(i => i.GroupOrder).OrderBy(i => i.Key).Select(i => new { i.Key, Items = i.OrderBy(x => x.Order) });
        foreach (var g in groups)
        {
            foreach (var item in g.Items)
            {
                results.Add(item.FieldName);
                Logger.WriteLine($"{item.FieldName} - {item.GroupName} - {item.GroupOrder}");
            }
        }

        var expected = string.Join(",", new List<string>() { "Test4", "Test3", "Test2", "Test1" });
        var actual = string.Join(",", results);
        Assert.Equal(expected, actual);
    }
}
