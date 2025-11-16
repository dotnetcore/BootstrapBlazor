// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class HtmlRendererTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Render_Ok()
    {
        var renderService = Context.Services.GetRequiredService<IComponentHtmlRenderer>();
        var html = await renderService.RenderAsync<Button>();
        Assert.NotEmpty(html);

        html = await renderService.RenderAsync(typeof(Button));
        Assert.NotEmpty(html);
    }
}
