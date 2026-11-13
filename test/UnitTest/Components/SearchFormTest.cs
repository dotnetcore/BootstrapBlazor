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
        var stringSearchMetadata = new StringSearchMetadata()
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

        searchItem.Metadata = stringSearchMetadata;
        cut.Render();
        cut.Contains("placeholder-val");
        cut.Contains("foo-name-value");

        // 改变搜索项值
        await stringSearchMetadata.ValueChangedHandler("test1");
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
        var stringSearchMetadata = new StringSearchMetadata()
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
                    Metadata = stringSearchMetadata
                },
                new SearchItem(nameof(Foo.Address), typeof(string), "Address")
                {
                    Metadata = stringSearchMetadata
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
                    Metadata = new StringSearchMetadata()
                },
                new SearchItem(nameof(Foo.Address), typeof(string), "Address")
                {
                    Metadata = new StringSearchMetadata()
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
    public void Metadata_Ok()
    {
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Count), typeof(string), nameof(Foo.Count))
                {
                    Metadata = new NumberSearchMetadata()
                },
                new SearchItem(nameof(Foo.DateTime), typeof(string), nameof(Foo.DateTime))
                {
                    Metadata = new DateTimeSearchMetadata()
                },
                new SearchItem(nameof(Foo.DateTime), typeof(string), nameof(Foo.DateTime))
                {
                    Metadata = new DateTimeRangeSearchMetadata()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    Metadata = new SelectSearchMetadata()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    Metadata = new MultipleSelectSearchMetadata()
                },
                new SearchItem(nameof(Foo.Education), typeof(string), nameof(Foo.Education))
                {
                    Metadata = new CheckboxListSearchMetadata()
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

        item.Metadata = new StringSearchMetadata() { Value = "test" };
        action = item.GetFilter();
        Assert.NotNull(action);
    }

    [Fact]
    public void BuildSearchMetadata_Enum_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(EnumEducation));

        item.Metadata = item.BuildSearchMetadata(options);
        Assert.IsType<SelectSearchMetadata>(item.Metadata);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetadata_Number_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(int));

        item.Metadata = item.BuildSearchMetadata(options);
        Assert.IsType<NumberSearchMetadata>(item.Metadata);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetadata_Bool_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(bool));

        item.Metadata = item.BuildSearchMetadata(options);
        Assert.IsType<SelectSearchMetadata>(item.Metadata);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetadata_DateTimeRange_Ok()
    {
        var options = new SearchFormLocalizerOptions();
        var item = new SearchItem("Name", typeof(DateTime));

        item.Metadata = item.BuildSearchMetadata(options);
        Assert.IsType<DateTimeRangeSearchMetadata>(item.Metadata);
        item.Reset();
    }

    [Fact]
    public void BuildSearchMetadata_DateTime_Ok()
    {
        var item = new SearchItem("Name", typeof(DateTime)) { Metadata = new DateTimeSearchMetadata() };

        Assert.IsType<DateTimeSearchMetadata>(item.Metadata);
        item.Reset();
    }

    [Fact]
    public void Reset_Ok()
    {
        var item = new SearchItem("Name", typeof(string));

        // Metadata 为空时不报错
        item.Reset();
    }

    [Fact]
    public async Task CustomMetadata_Ok()
    {
        var filterKeyValueAction = new FilterKeyValueAction();
        var cut = Context.Render<SearchForm>(pb =>
        {
            pb.Add(a => a.OnChanged, action =>
            {
                filterKeyValueAction = action;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, new List<ISearchItem>()
            {
                new SearchItem(nameof(Foo.Name), typeof(string), "Name")
                {
                    Metadata = new CustomeMetadata() { Value = "foo-name-value" }
                }
            });
        });

        cut.Contains("value=\"foo-name-value\"");
    }

    class CustomeMetadata : StringSearchMetadata
    {
        public override RenderFragment? RenderContent() => builder =>
        {
            builder.OpenComponent<BootstrapInput<string>>(0);
            builder.AddAttribute(10, nameof(BootstrapInput<>.Value), Value);
            builder.AddAttribute(20, nameof(BootstrapInput<>.OnValueChanged), ValueChangedHandler);
            builder.AddAttribute(30, nameof(BootstrapInput<>.ShowLabel), true);
            builder.AddAttribute(60, nameof(BootstrapInput<>.SkipValidate), true);
            builder.CloseComponent();
        };
    }
}
