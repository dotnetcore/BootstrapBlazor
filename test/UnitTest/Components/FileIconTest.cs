// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
