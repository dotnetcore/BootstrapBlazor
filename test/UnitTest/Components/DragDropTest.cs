// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class DragDropTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Drag_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.MaxItems, 3);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        cut.Contains("bb-dd-dropzone");
        cut.SetParametersAndRender(pb => pb.Add(a => a.ChildContent, (RenderFragment<string>?)null));
    }

    [Fact]
    public void ItemWrapperClass_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ItemWrapperClass, v => "test-ItemWrapperClass");
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        cut.Contains("test-ItemWrapperClass");
    }

    [Fact]
    public void DragOver_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-dropzone");
        cut.InvokeAsync(() => div.DragOver());
    }

    [Fact]
    public void DragEnter_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-dropzone");
        cut.InvokeAsync(() => div.DragEnter());
    }

    [Fact]
    public void Drop_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-dropzone");
        cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public void SpaceOnDrop_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-spacing");
        cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public void SpaceOnDragEnter_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-spacing");
        cut.InvokeAsync(() => div.DragEnter());
    }

    [Fact]
    public void SpaceOnDragLeave_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-spacing");
        cut.InvokeAsync(() => div.DragLeave());
    }

    [Fact]
    public void ItemOnDropStart_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-draggable");
        cut.InvokeAsync(() => div.DragStart());
    }

    [Fact]
    public void ItemOnDropEnter_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-draggable");
        cut.InvokeAsync(() => div.DragEnter());
    }

    [Fact]
    public async Task OnDrop_Test()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[^2];
        await cut.InvokeAsync(() => div.DragStart());
        await cut.InvokeAsync(() => div.DragEnd());

        divs = cut.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[0];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => div.DragLeave());
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public async Task MaxItem_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "3", "4" });
            pb.Add(a => a.MaxItems, 2);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.OnItemDropRejectedByMaxItemLimit, EventCallback.Factory.Create<string>(this, e =>
            {

            }));
        });

        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[1];
        await cut.InvokeAsync(() => div.DragStart());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[0];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task IsItemDragable_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.AllowsDrag, s => false);
        });
        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[^2];
        await cut.InvokeAsync(() => div.DragStart());
        await cut.InvokeAsync(() => div.DragEnd());

        divs = cut.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[0];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task DropRejected_Test()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.Accepts, (s, s1) => false);
            pb.Add(a => a.OnItemDropRejected, EventCallback<string>.Empty);
            pb.Add(a => a.OnItemDrop, EventCallback<string>.Empty);
        });
        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[^2];
        await cut.InvokeAsync(() => div.DragStart());
        await cut.InvokeAsync(() => div.DragEnd());

        divs = cut.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[0];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public async Task OnDropOver_Test()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.OnReplacedItemDrop, EventCallback<string>.Empty);
        });
        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[^2];
        await cut.InvokeAsync(() => div.DragStart());
        await cut.InvokeAsync(() => div.DragEnd());

        divs = cut.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public async Task CopyItem_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "3", "4" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.CopyItem, s => s);
        });

        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[1];
        await cut.InvokeAsync(() => div.DragStart());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task CopyItem2_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "3", "4" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.CopyItem, s => s);
        });

        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[1];
        await cut.InvokeAsync(() => div.DragStart());

        divs = cut1.FindAll(".bb-dd-dropzone");
        var divTarget = divs[0];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task CopyItem3_Ok()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "1", "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "3", "4" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.CopyItem, s => s);
        });

        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[1];
        await cut.InvokeAsync(() => div.DragStart());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[2];
        await cut.InvokeAsync(() => divTarget.DragEnter());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        divTarget = divs[4];
        await cut.InvokeAsync(() => divTarget.DragLeave());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        divTarget = divs[2];
        await cut.InvokeAsync(() => divTarget.DragEnter());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        divTarget = divs[4];
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task Special_Test()
    {
        var cut = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { null!, "2" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, (object?)null));
        });
        var cut1 = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "3", "4" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.MaxItems, 2);
        });
        var cut2 = Context.RenderComponent<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<string> { "3", "4" });
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.Accepts, ((s, s1) => false));
        });

        var divs = cut.FindAll(".bb-dd-dropzone > div");
        var div = divs[1];
        await cut.InvokeAsync(() => div.DragEnter());
        divs = cut.FindAll(".bb-dd-dropzone > div");
        div = divs[3];
        await cut.InvokeAsync(() => div.DragStart());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        var divTarget = divs[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());

        divs = cut1.FindAll(".bb-dd-dropzone > div");
        div = divs[3];
        await cut.InvokeAsync(() => div.DragStart());

        divs = cut2.FindAll(".bb-dd-dropzone > div");
        divTarget = divs[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());
    }
}
