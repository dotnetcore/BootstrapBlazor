﻿// Licensed to the .NET Foundation under one or more agreements.
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
