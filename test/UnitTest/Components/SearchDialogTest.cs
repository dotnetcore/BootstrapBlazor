// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SearchDialogTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task UseSearchForm_Ok()
    {
        FilterKeyValueAction? filter = null;
        bool reset = false;
        bool search = false;
        var foo = new Foo();
        var cut = Context.Render<SearchDialog<Foo>>(pb =>
        {
            pb.Add(a => a.UseSearchForm, true);
            pb.Add(a => a.SearchItems, new List<ISearchItem>()
            {
                new SearchItem("Name", typeof(string), "姓名"),
                new SearchItem("Address", typeof(string), "地址")
                {
                    MetaData = new StringSearchMetaData()
                }
            });
            pb.Add(a => a.OnChanged, action =>
            {
                filter = action;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnResetSearchClick, () =>
            {
                reset = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnSearchClick, () =>
            {
                search = true;
                return Task.CompletedTask;
            });
        });

        // 测试更新搜索值
        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.Change("test-value"));
        Assert.NotNull(filter);

        Assert.Single(filter.Filters);
        Assert.Equal("test-value", filter.Filters[0].FieldValue);

        var buttons = cut.FindComponents<DialogCloseButton>();
        Assert.Equal(2, buttons.Count);

        // 测试重置按钮
        var resetButton = buttons[0];
        await cut.InvokeAsync(() => resetButton.Instance.OnClickWithoutRender!.Invoke());
        Assert.True(reset);

        // 测试搜索按钮
        var searchButton = buttons[1];
        await cut.InvokeAsync(() => searchButton.Instance.OnClickWithoutRender!.Invoke());
        Assert.True(search);
    }
}
