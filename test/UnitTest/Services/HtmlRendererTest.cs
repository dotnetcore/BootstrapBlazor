// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

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
