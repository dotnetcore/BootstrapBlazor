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
        var stringSearchMetaData = new StringSearchMetadata1()
        {
            PlaceHolder = "placeholder-val",
            Value = "foo-name-value"
        };
        var searchItem = new SearchItem(nameof(Foo.Name), typeof(string), "Name") { Cols = 6 };
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.OnChanged, action =>
            {
                filterKeyValueAction = action;
                return Task.CompletedTask;
            });
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
    public void SearchFormLocalizerOptions_Ok()
    {
        var searchFormLocalizerOptions = new SearchFormLocalizerOptions()
        {
            NumberStartValueLabelText = "Start-Text",
            NumberEndValueLabelText = "End-Text"
        };
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.SearchFormLocalizerOptions, searchFormLocalizerOptions);
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Count), typeof(int), nameof(Foo.Count))
            });
        });

        cut.Contains("Start-Text");
        cut.Contains("End-Text");
    }

    [Fact]
    public void ToFilter_Ok()
    {
        List<ISearchItem>? items = null;
        var filter = items.ToFilter();
        Assert.NotNull(filter);
    }

    [Fact]
    public void LabelAlign_Ok()
    {
        var stringSearchMetaData = new StringSearchMetadata1()
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
                    Text = "Name-Updated",
                    GroupName = "Group1",
                    GroupOrder = 1,
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
    public void Group_Ok()
    {
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.ShowUnsetGroupItemsOnTop, true);
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Name), typeof(string), "Name")
                {
                    GroupName = "Group1",
                    GroupOrder= 1,
                    MetaData = new StringSearchMetadata1()
                },
                new SearchItem(nameof(Foo.Address), typeof(string), "Address")
                {
                    MetaData = new StringSearchMetadata1()
                }
            });
        });

        // 查找标签一共有两个第一个应该是 Address 第二个应该是 Name
        var labels = cut.FindAll(".bb-search-form label");
        Assert.Equal(2, labels.Count);
        Assert.Equal("Address", labels[0].TextContent);
        Assert.Equal("Name", labels[1].TextContent);
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
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Count), typeof(string), nameof(Foo.Count))
                {
                    MetaData = new NumberSearchMetadata1()
                },
                new SearchItem(nameof(Foo.DateTime), typeof(string), nameof(Foo.DateTime))
                {
                    MetaData = new DateTimeSearchMetadata1()
                },
                new SearchItem(nameof(Foo.DateTime), typeof(string), nameof(Foo.DateTime))
                {
                    MetaData = new DateTimeRangeSearchMetadata1()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    MetaData = new SelectSearchMetadata1()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    MetaData = new MultipleSelectSearchMetadata1()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    MetaData = new CheckboxListSearchMetadata1()
                }
            });
        });
    }

    [Fact]
    public void GetFilter_Ok()
    {
        var item = new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education));
        var action = item.GetFilter();
        Assert.Null(action);

        item.MetaData = new StringSearchMetadata1() { Value = "test" };
        action = item.GetFilter();
        Assert.NotNull(action);
    }

    [Fact]
    public void BuildSearchMetaData_Enum_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(EnumEducation));

        item.MetaData = item.BuildSearchMetadata(options);
        Assert.IsType<SelectSearchMetadata1>(item.MetaData);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetaData_Number_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(int));

        item.MetaData = item.BuildSearchMetadata(options);
        Assert.IsType<NumberSearchMetadata1>(item.MetaData);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetaData_Bool_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(bool));

        item.MetaData = item.BuildSearchMetadata(options);
        Assert.IsType<SelectSearchMetadata1>(item.MetaData);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetaData_DateTimeRange_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(DateTime));

        item.MetaData = item.BuildSearchMetadata(options);
        Assert.IsType<DateTimeRangeSearchMetadata1>(item.MetaData);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetaData_DateTime_Ok()
    {
        var item = new SearchItem("Name", typeof(DateTime)) { MetaData = new DateTimeSearchMetadata1() };

        Assert.IsType<DateTimeSearchMetadata1>(item.MetaData);
        item.Reset();
    }

    [Fact]
    public void Reset_Ok()
    {
        var item = new SearchItem("Name", typeof(string));

        // Metadata 为空时不报错
        item.Reset();
    }
}
