// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET11_0_OR_GREATER
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace UnitTest.Components;

public class VirtualizeNet11Test : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Parameters_Ok()
    {
        var cut = Context.Render<AutoFill<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.Items, ["Item 1", "Item 2"]);
            pb.Add(a => a.InitialItemIndex, 1);
            pb.Add(a => a.AnchorMode, VirtualizeAnchorMode.End);
        });

        var virtualize = cut.FindComponent<Virtualize<string>>().Instance;
        Assert.Equal(1, virtualize.InitialItemIndex);
        Assert.Equal(VirtualizeAnchorMode.End, virtualize.AnchorMode);
    }

    [Fact]
    public void ItemsProvider_Parameters_Ok()
    {
        var cut = Context.Render<AutoFill<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.InitialItemIndex, 2);
            pb.Add(a => a.AnchorMode, VirtualizeAnchorMode.None);
            pb.Add(a => a.OnQueryAsync, option => Task.FromResult(new QueryData<string>
            {
                Items = ["Item 1", "Item 2", "Item 3"],
                TotalCount = 3
            }));
        });

        var virtualize = cut.FindComponent<Virtualize<string>>().Instance;
        Assert.Equal(2, virtualize.InitialItemIndex);
        Assert.Equal(VirtualizeAnchorMode.None, virtualize.AnchorMode);
    }

    [Fact]
    public void ComponentApi_Ok()
    {
        Type[] types =
        [
            typeof(AutoFill<string>),
            typeof(Select<string>),
            typeof(MultiSelect<string>),
            typeof(SelectGeneric<string>),
            typeof(MultiSelectGeneric<string>),
            typeof(Table<Foo>),
            typeof(TreeView<Foo>)
        ];

        Assert.All(types, type =>
        {
            Assert.NotNull(type.GetProperty(nameof(AutoFill<string>.InitialItemIndex)));
            Assert.NotNull(type.GetProperty(nameof(AutoFill<string>.AnchorMode)));
            Assert.NotNull(type.GetMethod(
                nameof(AutoFill<string>.ScrollToIndexAsync),
                [typeof(int), typeof(CancellationToken)]));
        });
    }

    [Fact]
    public async Task AutoFill_ScrollToIndexAsync_Ok()
    {
        var cut = Context.Render<AutoFill<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.Items, ["Item 1", "Item 2"]);
        });

        await cut.InvokeAsync(() => cut.Instance.ScrollToIndexAsync(1));
    }

    [Fact]
    public async Task Select_ScrollToIndexAsync_Ok()
    {
        var cut = Context.Render<Select<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.Items,
            [
                new SelectedItem("1", "Item 1"),
                new SelectedItem("2", "Item 2")
            ]);
        });

        await cut.InvokeAsync(() => cut.Instance.ScrollToIndexAsync(1));
    }

    [Fact]
    public async Task MultiSelect_ScrollToIndexAsync_Ok()
    {
        var cut = Context.Render<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.Items,
            [
                new SelectedItem("1", "Item 1"),
                new SelectedItem("2", "Item 2")
            ]);
        });

        await cut.InvokeAsync(() => cut.Instance.ScrollToIndexAsync(1));
    }

    [Fact]
    public async Task SelectGeneric_ScrollToIndexAsync_Ok()
    {
        var cut = Context.Render<SelectGeneric<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.Items,
            [
                new SelectedItem<string>("1", "Item 1"),
                new SelectedItem<string>("2", "Item 2")
            ]);
        });

        await cut.InvokeAsync(() => cut.Instance.ScrollToIndexAsync(1));
    }

    [Fact]
    public async Task MultiSelectGeneric_ScrollToIndexAsync_Ok()
    {
        var cut = Context.Render<MultiSelectGeneric<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.Items,
            [
                new SelectedItem<string>("1", "Item 1"),
                new SelectedItem<string>("2", "Item 2")
            ]);
        });

        await cut.InvokeAsync(() => cut.Instance.ScrollToIndexAsync(1));
    }

    [Fact]
    public async Task Table_ScrollToIndexAsync_Ok()
    {
        var cut = Context.Render<Table<Foo>>(pb =>
        {
            pb.Add(a => a.ScrollMode, ScrollMode.Virtual);
            pb.Add(a => a.Items, [new Foo(), new Foo()]);
        });

        await cut.InvokeAsync(() => cut.Instance.ScrollToIndexAsync(1));
    }

    [Fact]
    public async Task TreeView_ScrollToIndexAsync_Ok()
    {
        var cut = Context.Render<TreeView<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.Items,
            [
                new TreeViewItem<string>("1") { Text = "Item 1" },
                new TreeViewItem<string>("2") { Text = "Item 2" }
            ]);
        });

        await cut.InvokeAsync(() => cut.Instance.ScrollToIndexAsync(1));
    }
}
#endif
