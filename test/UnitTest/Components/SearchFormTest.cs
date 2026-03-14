// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SearchFormTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Filter_Ok()
    {
        var filterKeyValueAction = new FilterKeyValueAction();
        var stringSearchMetaData = new StringSearchMetaData()
        {
            PlaceHolder = "placeholder-val",
            Value = "foo-name-value"
        };
        var searchItem = new SearchItem(nameof(Foo.Name), typeof(string), "Name") { Cols = 6 };
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.Filter, filterKeyValueAction);
            pb.Add(a => a.OnFilterChanged, action =>
            {
                filterKeyValueAction = action;
                return Task.CompletedTask;
            });
            pb.Add(a => a.FilterChanged, EventCallback.Factory.Create<FilterKeyValueAction>(this, v =>
            {
                filterKeyValueAction = v;
            }));
            pb.Add(a => a.ItemsPerRow, 4);
            pb.Add(a => a.RowType, RowType.Inline);
            pb.Add(a => a.LabelAlign, Alignment.Right);
            pb.Add(a => a.LabelWidth, 120);
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.ShowLabelTooltip, true);
            pb.Add(a => a.GroupType, EditorFormGroupType.GroupBox);
            pb.Add(a => a.ShowUnsetGroupItemsOnTop, true);
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                searchItem
            });
        });

        cut.Contains("col-sm-6");
        cut.Contains("form-inline-end");

        searchItem.MetaData = stringSearchMetaData;
        cut.Render();
        cut.Contains("placeholder-val");
        cut.Contains("foo-name-value");

        // 改变搜索项值
        await stringSearchMetaData.ValueChangedHandler("test1");
        Assert.Single(filterKeyValueAction.Filters);
        Assert.Equal("test1", filterKeyValueAction.Filters[0].FieldValue);
    }

    [Fact]
    public void LabelAlign_Ok()
    {
        var stringSearchMetaData = new StringSearchMetaData()
        {
            PlaceHolder = "placeholder-val",
            Value = "foo-name-value"
        };
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.RowType, RowType.Inline);
            pb.Add(a => a.LabelAlign, Alignment.Center);
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Name), typeof(string), "Name")
                {
                    GroupName = "Group1",
                    MetaData = stringSearchMetaData
                },
                new SearchItem(nameof(Foo.Address), typeof(string), "Address")
                {
                    MetaData = stringSearchMetaData
                }
            });
        });

        cut.Contains("form-inline form-inline-center");

        cut.Render(pb =>
        {
            pb.Add(a => a.RowType, RowType.Normal);
        });
        cut.DoesNotContain("form-inline form-inline-center");
    }

    [Fact]
    public void ShowUnsetGroupItemsOnTop_Ok()
    {
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.ShowUnsetGroupItemsOnTop, true);
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Name), typeof(string), "Name")
                {
                    GroupName = "Group1",
                    MetaData = new StringSearchMetaData()
                },
                new SearchItem(nameof(Foo.Address), typeof(string), "Address")
                {
                    MetaData = new StringSearchMetaData()
                }
            });
        });
    }

    [Fact]
    public void Buttons_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.Buttons, builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.CloseComponent();
            });
        });

        cut.Contains("bb-editor-footer form-footer");
    }

    [Fact]
    public void MetaData_Ok()
    {
        var filterKeyValueAction = new FilterKeyValueAction();
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.Filter, filterKeyValueAction);
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Count), typeof(string), nameof(Foo.Count))
                {
                    MetaData = new NumberSearchMetaData()
                },
                new SearchItem(nameof(Foo.DateTime), typeof(string), nameof(Foo.DateTime))
                {
                    MetaData = new DateTimeSearchMetaData()
                },
                new SearchItem(nameof(Foo.DateTime), typeof(string), nameof(Foo.DateTime))
                {
                    MetaData = new DateTimeRangeSearchMetaData()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    MetaData = new SelectSearchMetaData()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    MetaData = new MultipleSelectSearchMetaData()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    MetaData = new CheckboxListSearchMetaData()
                }
            });
        });
    }
}
