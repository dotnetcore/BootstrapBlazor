// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Utils;

public class LinqTest
{
    [Fact]
    public void Union_Ok()
    {
        var foo1 = new Dummy[]
        {
            new()
            {
                Name = "Test1",
                Items = ["Test_Item_1"]
            },
            new()
            {
                Name = "Test2",
                Items = ["Test_Item_4"]
            }
        };
        var foo2 = new Dummy[]
        {
            new()
            {
                Name = "Test1",
                Items = ["Test_Item_2", "Test_Item_3"]
            },
            new()
            {
                Name = "Test2",
                Items = ["Test_Item_5", "Test_Item_6"]
            }
        };

        var foo3 = foo1.Zip(foo2, (f1, f2) =>
        {
            var items = new List<string>();
            items.AddRange(f1.Items);
            items.AddRange(f2.Items);
            return new Dummy()
            {
                Name = f1.Name,
                Items = items
            };
        });
        Assert.Equal(2, foo3.Count());
        Assert.Equal(["Test_Item_1", "Test_Item_2", "Test_Item_3"], foo3.First().Items);
    }

    private class Dummy
    {
        public string? Name { get; set; }

        public List<string> Items { get; set; } = [];
    }
}
