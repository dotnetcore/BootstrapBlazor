// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class IFrameTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Frame_Ok()
    {
        var postData = false;
        var cut = Context.RenderComponent<IFrame>(pb =>
        {
            pb.Add(a => a.Src, "/Cat");
            pb.Add(a => a.OnPostDataAsync, v =>
            {
                postData = true;
                return Task.CompletedTask;
            });
        });
        cut.Contains("iframe");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Data, new { Rows = new List<string>() { "1", "2" } });
        });

        cut.InvokeAsync(async () =>
        {
            await cut.Instance.CallbackAsync(new List<string> { "2", "3" });
            Assert.True(postData);
        });
    }
}
