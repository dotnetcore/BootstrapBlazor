// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DragDropTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Drag_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.MaxItems, 3);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        cut.Contains("bb-dd-dropzone");
        cut.Render(pb => pb.Add(a => a.ChildContent, (RenderFragment<string>?)null));
    }

    [Fact]
    public void ItemWrapperClass_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ItemWrapperClass, v => "test-ItemWrapperClass");
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        cut.Contains("test-ItemWrapperClass");
    }

    [Fact]
    public void DragOver_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-dropzone");
        cut.InvokeAsync(() => div.DragOver());
    }

    [Fact]
    public void DragEnter_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-dropzone");
        cut.InvokeAsync(() => div.DragEnter());
    }

    [Fact]
    public void Drop_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-dropzone");
        cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public void SpaceOnDrop_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-spacing");
        cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public void SpaceOnDragEnter_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-spacing");
        cut.InvokeAsync(() => div.DragEnter());
    }

    [Fact]
    public void SpaceOnDragLeave_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-spacing");
        cut.InvokeAsync(() => div.DragLeave());
    }

    [Fact]
    public void ItemOnDropStart_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-draggable");
        cut.InvokeAsync(() => div.DragStart());
    }

    [Fact]
    public void ItemOnDropEnter_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var div = cut.Find(".bb-dd-draggable");
        cut.InvokeAsync(() => div.DragEnter());
    }

    [Fact]
    public void OnDrop_Test()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });

        cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".bb-dd-dropzone > div");
            var div = items[^2];
            div.DragStart();
            div.DragEnd();

            items = cut.FindAll(".bb-dd-dropzone > div");
            var divTarget = items[0];
            divTarget.DragEnter();
            div.DragLeave();
            divTarget.DragEnter();
            div.Drop();
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData(1)]
    [InlineData(2)]
    public void MaxItem_Ok(int? items)
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["3", "4"]);
            pb.Add(a => a.MaxItems, items);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.OnItemDropRejectedByMaxItemLimit, EventCallback.Factory.Create<string>(this, e =>
            {

            }));
        });

        cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".bb-dd-dropzone > div");
            var div = items[1];
            div.DragStart();
        });

        cut1.InvokeAsync(() =>
        {
            var items = cut1.FindAll(".bb-dd-dropzone > div");
            var divTarget = items[0];
            divTarget.DragEnter();
            divTarget.Drop();
        });
    }

    [Fact]
    public void IsItemDraggable_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.AllowsDrag, s => false);
        });

        cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".bb-dd-dropzone > div");
            var div = items[^2];
            div.DragStart();
            div.DragEnd();
        });

        cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".bb-dd-dropzone > div");
            var divTarget = items[0];
            divTarget.DragEnter();
            divTarget.Drop();
        });
    }

    [Fact]
    public async Task DropRejected_Test()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.Accepts, (s, s1) => false);
            pb.Add(a => a.OnItemDropRejected, EventCallback<string>.Empty);
            pb.Add(a => a.OnItemDrop, EventCallback<string>.Empty);
        });
        var items = cut.FindAll(".bb-dd-dropzone > div");
        var div = items[^2];
        await cut.InvokeAsync(() => div.DragStart());
        await cut.InvokeAsync(() => div.DragEnd());

        items = cut.FindAll(".bb-dd-dropzone > div");
        var divTarget = items[0];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public async Task OnDropOver_Test()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.OnReplacedItemDrop, EventCallback<string>.Empty);
        });
        var items = cut.FindAll(".bb-dd-dropzone > div");
        var div = items[^2];
        await cut.InvokeAsync(() => div.DragStart());
        await cut.InvokeAsync(() => div.DragEnd());

        items = cut.FindAll(".bb-dd-dropzone > div");
        var divTarget = items[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => div.Drop());
    }

    [Fact]
    public async Task CopyItem_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["3", "4"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.CopyItem, s => s);
        });

        var items = cut.FindAll(".bb-dd-dropzone > div");
        var div = items[1];
        await cut.InvokeAsync(() => div.DragStart());

        items = cut1.FindAll(".bb-dd-dropzone > div");
        var divTarget = items[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task CopyItem2_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["3", "4"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.CopyItem, s => s);
        });

        var items = cut.FindAll(".bb-dd-dropzone > div");
        var div = items[1];
        await cut.InvokeAsync(() => div.DragStart());

        items = cut1.FindAll(".bb-dd-dropzone");
        var divTarget = items[0];
        await cut.InvokeAsync(() => divTarget.DragEnter());
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task CopyItem3_Ok()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["1", "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
        });
        var cut1 = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["3", "4"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.CopyItem, s => s);
        });

        var items = cut.FindAll(".bb-dd-dropzone > div");
        var div = items[1];
        await cut.InvokeAsync(() => div.DragStart());

        items = cut1.FindAll(".bb-dd-dropzone > div");
        var divTarget = items[2];
        await cut.InvokeAsync(() => divTarget.DragEnter());

        items = cut1.FindAll(".bb-dd-dropzone > div");
        divTarget = items[4];
        await cut.InvokeAsync(() => divTarget.DragLeave());

        items = cut1.FindAll(".bb-dd-dropzone > div");
        divTarget = items[2];
        await cut.InvokeAsync(() => divTarget.DragEnter());

        items = cut1.FindAll(".bb-dd-dropzone > div");
        divTarget = items[4];
        await cut.InvokeAsync(() => divTarget.Drop());
    }

    [Fact]
    public async Task Special_Test()
    {
        var cut = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, [null!, "2"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, (object?)null));
        });
        var cut1 = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["3", "4"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.MaxItems, 2);
        });
        var cut2 = Context.Render<Dropzone<string>>(pb =>
        {
            pb.Add(a => a.Items, ["3", "4"]);
            pb.Add(a => a.ChildContent, v => builder => builder.AddContent(0, v));
            pb.Add(a => a.Accepts, ((s, s1) => false));
        });

        var items = cut.FindAll(".bb-dd-dropzone > div");
        var div = items[1];
        await cut.InvokeAsync(() => div.DragEnter());
        items = cut.FindAll(".bb-dd-dropzone > div");
        div = items[3];
        await cut.InvokeAsync(() => div.DragStart());

        items = cut1.FindAll(".bb-dd-dropzone > div");
        var divTarget = items[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());

        items = cut1.FindAll(".bb-dd-dropzone > div");
        div = items[3];
        await cut.InvokeAsync(() => div.DragStart());

        items = cut2.FindAll(".bb-dd-dropzone > div");
        divTarget = items[1];
        await cut.InvokeAsync(() => divTarget.DragEnter());
    }
}
