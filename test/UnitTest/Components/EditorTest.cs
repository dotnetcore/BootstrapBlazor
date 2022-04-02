// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class EditorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Editor_Ok()
    {
        var value = new Foo();
        var cut = Context.RenderComponent<Editor>(pb =>
        {
            pb.Add(a => a.Value, value.Name);
            pb.Add(a => a.ValueChanged, v => value.Name = v);
            pb.Add(a => a.IsEditor, false);
            pb.Add(a => a.Height, 200);
        });

        await cut.InvokeAsync(() => cut.Instance.Update("Test"));
        Assert.Equal("Test", value.Name);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnValueChanged, v =>
            {
                value.Name = v;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() => cut.Instance.Update("Test1"));
        Assert.Equal("Test1", value.Name);
    }

    [Fact]
    public async Task CustomerToolbarButtons_Ok()
    {
        var cut = Context.RenderComponent<Editor>(pb =>
        {
            pb.Add(a => a.Value, "Test");
            pb.Add(a => a.CustomerToolbarButtons, new EditorToolbarButton[]
            {
                new EditorToolbarButton()
                {
                    ButtonName = "Test1",
                    IconClass = "Class1",
                    Tooltip = "Tooltip1"
                }
            });
        });

        IEnumerable<object>? buttons = null;
        await cut.InvokeAsync(async () => buttons = await cut.Instance.GetToolBar());
        Assert.NotNull(buttons);

        IEnumerable<EditorToolbarButton>? btns = null;
        await cut.InvokeAsync(async () => btns = await cut.Instance.GetPluginAttrs());
        Assert.Single(btns);
        Assert.Equal("Class1", btns!.First().IconClass);
        Assert.Equal("Tooltip1", btns!.First().Tooltip);

        var name = "";
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnClickButton, v =>
            {
                return Task.FromResult("Test");
            });
            pb.Add(a => a.Value, null);
        });
        await cut.InvokeAsync(async () => name = await cut.Instance.ClickPluginItem("Test1"));
        Assert.Equal("Test", name);
    }

    [Fact]
    public void DoMethodAysnc_Ok()
    {
        var cut = Context.RenderComponent<Editor>(pb =>
        {
            pb.Add(a => a.Value, "Test");
        });
        cut.Instance.DoMethodAysnc("test", new object[] { "1" });
    }
}
