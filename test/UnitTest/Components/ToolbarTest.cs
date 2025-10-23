// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ToolbarTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Toolbar_Ok()
    {
        var cut = Context.RenderComponent<Toolbar>(pb =>
        {
            pb.Add(a => a.IsWrap, true);
            pb.AddChildContent<ToolbarItem>(pb =>
            {
                pb.AddChildContent<Button>();
            });
            pb.AddChildContent<ToolbarSeparator>();
            pb.AddChildContent<ToolbarButtonGroup>(pb =>
            {
                pb.AddChildContent<Button>();
                pb.AddChildContent<Button>();
            });
            pb.AddChildContent<ToolbarSpace>();
        });

        // 检查 IsWrap 参数
        cut.Contains("flex-wrap: wrap;");

        // 检查 Button 按钮
        cut.Contains("bb-toolbar-item");

        // 检查 ButtonGroup
        cut.Contains("bb-toolbar-group btn-group");

        // 检查 分隔符
        cut.Contains("bb-toolbar-vr vr");

        // 检查 填充元素
        cut.Contains("bb-toolbar-space");
    }

    [Fact]
    public void ToolbarButtonGroup_Ok()
    {
        var cut = Context.RenderComponent<ToolbarButtonGroup>(pb =>
        {
            pb.AddChildContent<Button>(pb2 =>
            {
                pb2.Add(a => a.Text, "Button1");
            });
            pb.AddChildContent<Button>(pb2 =>
            {
                pb2.Add(a => a.Text, "Button2");
            });
        });
        // 检查 ButtonGroup
        cut.Contains("bb-toolbar-group btn-group");

        // 检查 Button1
        cut.Contains("Button1");

        // 检查 Button2
        cut.Contains("Button2");
    }
}
