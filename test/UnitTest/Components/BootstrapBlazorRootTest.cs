// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

//using HarmonyLib;

namespace UnitTest.Components;

public class BootstrapBlazorRootTest : TestBase
{
    [Fact]
    public void Render_Ok()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var sc = context.Services;
        sc.AddBootstrapBlazor();
        sc.ConfigureJsonLocalizationOptions(op =>
        {
            op.IgnoreLocalizerMissing = false;
        });
        sc.AddScoped<IRootComponentGenerator, MockGenerator>();
        var cut = context.RenderComponent<BootstrapBlazorRoot>();
        cut.Contains("<div class=\"auto-generator\"></div>");
    }

    class MockGenerator : IRootComponentGenerator
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public RenderFragment Generator() => builder =>
        {
            builder.AddContent(0, new MarkupString("<div class=\"auto-generator\"></div>"));
        };
    }
}
