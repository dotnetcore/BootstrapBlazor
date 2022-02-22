// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TreeTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<Tree>();
        cut.DoesNotContain("tree-root");

        // 由于 Items 为空不生成 TreeItem 显示 loading
        cut.Contains("table-loading");
        cut.DoesNotContain("li");

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowSkeleton, true));
        cut.Contains("Loading");

        // 设置 Items
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<TreeItem>()
            {
                new TreeItem() { Text = "Test1" }
            });
        });
        cut.Contains("li");
    }

    [Fact]
    public void OnClick_Ok()
    {
        var clicked = false;
        var expanded = false;
        var cut = Context.RenderComponent<Tree>(pb =>
        {
            pb.Add(a => a.IsAccordion, true);
            pb.Add(a => a.ClickToggleNode, true);
            pb.Add(a => a.OnTreeItemClick, item =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnExpandNode, item =>
            {
                expanded = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, new List<TreeItem>()
            {
                new TreeItem()
                {
                    Text = "Test1",
                    Items = new List<TreeItem>()
                    {
                        new TreeItem()
                        {
                            Text = "Test11",
                        }
                    }
                },
                new TreeItem()
                {
                    Text = "Test2",
                    IsCollapsed = false,
                    Items = new List<TreeItem>()
                    {
                        new TreeItem()
                        {
                            Text = "Test21",
                        }
                    }
                }
            });
        });

        cut.InvokeAsync(() => cut.Find(".tree-node").Click());
        Assert.True(clicked);
        Assert.True(expanded);
    }

    [Fact]
    public void OnStateChanged_Ok()
    {
        List<TreeItem>? checkedLists = null;
        var cut = Context.RenderComponent<Tree>(pb =>
        {
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.OnTreeItemChecked, items =>
            {
                checkedLists = items;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, new List<TreeItem>()
            {
                new TreeItem()
                {
                    Text = "Test1",
                    Icon = "fa fa-fa",
                    CssClass = "Test-Class",
                    Items = new List<TreeItem>()
                    {
                        new()
                        {
                            Text = "Test1-1",
                            Items = new List<TreeItem>()
                            {
                                new()
                                {
                                    Text = "Test1-1-1"
                                }
                            }
                        }
                    }
                }
            });
        });

        cut.InvokeAsync(() => cut.Find("[type=\"checkbox\"]").Click());
        cut.DoesNotContain("fa fa-fa");
        cut.Contains("Test-Class");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowIcon, true);
        });
        cut.Contains("fa fa-fa");
    }

    [Fact]
    public void Template_Ok()
    {
        var item = new TreeItem()
        {
            Text = "Test1",
            Key = "TestKey",
            Tag = "TestTag",
            Template = builder => builder.AddContent(0, "Test-Template")
        };
        var cut = Context.RenderComponent<Tree>(pb =>
        {
            pb.Add(a => a.Items, new List<TreeItem>()
            {
                item
            });
        });
        cut.Contains("Test-Template");
        Assert.Equal("TestKey", item.Key);
        Assert.Equal("TestTag", item.Tag);
    }

    [Fact]
    public void OnExpandRowAsync_Ok()
    {
        var expanded = false;
        var cut = Context.RenderComponent<Tree>(pb =>
        {
            pb.Add(a => a.OnExpandNode, item =>
            {
                expanded = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, new List<TreeItem>()
            {
                new TreeItem()
                {
                    Text = "Test1",
                    Items = new List<TreeItem>()
                    {
                        new TreeItem()
                        {
                            Text = "Test11",
                            HasChildNode = true,
                            ShowLoading = true
                        }
                    }
                }
            });
        });

        cut.InvokeAsync(() => cut.Find(".fa-caret-right").Click());
        Assert.True(expanded);
    }

    [Fact]
    public void GetAllSubItems_Ok()
    {
        var items = new List<TreeItem>()
        {
            new TreeItem() { Text = "Test1", Id = "01" },
            new TreeItem() { Text = "Test2", Id = "02", ParentId = "01" },
            new TreeItem() { Text = "Test3", Id = "03", ParentId = "02" },
        };

        var data = items.CascadingTree();
        Assert.Single(data);

        var subs = data.First().GetAllSubItems();
        Assert.Equal(2, subs.Count());
    }

    [Fact]
    public void ShowRadio_Ok()
    {
        List<TreeItem>? checkedLists = null;
        var cut = Context.RenderComponent<Tree>(pb =>
        {
            pb.Add(a => a.ShowRadio, true);
            pb.Add(a => a.OnTreeItemChecked, items =>
            {
                checkedLists = items;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, new List<TreeItem>()
            {
                new() { Text = "Test1", Icon = "fa fa-fa" },
                new() { Text = "Test2", Icon = "fa fa-fa" }
            });
        });
        cut.Find("[type=\"radio\"]").Click();
        Assert.Single(checkedLists);
        Assert.Equal("Test1", checkedLists![0].Text);

        cut.FindAll("[type=\"radio\"]")[1].Click();
        Assert.Equal("Test2", checkedLists![0].Text);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowSkeleton, false);
        });
    }
}
