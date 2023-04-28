// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class JSRuntimeEventHandlerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void RegisterEvent_Ok()
    {
        var eh = Context.Services.GetRequiredService<IJSRuntimeEventHandler>();
        _ = eh.RegisterEvent(DOMEvents.Scroll);
        _ = eh.RunEval("test");
    }

    [Fact]
    public void Invoke_Ok()
    {
        var eh = Context.Services.GetRequiredService<IJSRuntimeEventHandler>();

        _ = eh.GetElementPropertiesByTagFromIdAsync<int>("id", "tag");
        _ = eh.GetDocumentPropertiesByTagAsync<int>("tag");
        _ = eh.GetElementPropertiesByTagAsync<int>(new ElementReference(), "tag");
    }
}
