// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

//using HarmonyLib;

namespace UnitTest.Components;

public class BootstrapBlazorRootTest : TestBase
{
    [Fact]
    public void Render_Ok()
    {
        Context.Services.AddBootstrapBlazor();
        Context.Services.AddScoped<IRootComponentGenerator, MockGenerator>();
        Context.Services.GetRequiredService<ICacheManager>();
        var cut = Context.Render<BootstrapBlazorRoot>();
        cut.Contains("<div class=\"auto-generator\"></div>");
    }

    class MockGenerator : IRootComponentGenerator
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public RenderFragment Generator() => builder =>
        {
            builder.AddContent(0, new MarkupString("<div class=\"auto-generator\"></div>"));
        };
    }
}
