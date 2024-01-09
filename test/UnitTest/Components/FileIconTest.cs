// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class FileIconTest : TestBase
{
    [Fact]
    public void Extension_Ok()
    {
        var cut = Context.RenderComponent<FileIcon>(pb =>
        {
            pb.Add(a => a.Extension, ".xlsx");
        });
        cut.Contains("badge bg-primary");
        cut.Contains(".xlsx");
    }

    [Fact]
    public void IconColor_Ok()
    {
        var cut = Context.RenderComponent<FileIcon>(pb =>
        {
            pb.Add(a => a.Extension, ".xlsx");
            pb.Add(a => a.IconColor, Color.Danger);
        });
        cut.Contains("badge bg-danger");
    }

    [Fact]
    public void BackgroundTemplate_Ok()
    {
        var cut = Context.RenderComponent<FileIcon>(pb =>
        {
            pb.Add(a => a.Extension, ".xlsx");
            pb.Add(a => a.BackgroundTemplate, context => context.AddContent(0, "test-content"));
        });
        cut.Contains("test-content");
    }
}
