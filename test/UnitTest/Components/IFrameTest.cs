﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
