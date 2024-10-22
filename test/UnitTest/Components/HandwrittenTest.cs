// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class HandwrittenTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ClearButtonText_OK()
    {
        var cut = Context.RenderComponent<Handwritten>(builder => builder.Add(s => s.ClearButtonText, "Clear Text"));

        Assert.Contains("Clear Text", cut.Markup);
    }

    [Fact]
    public void SaveButtonText_OK()
    {
        var cut = Context.RenderComponent<Handwritten>(builder => builder.Add(s => s.SaveButtonText, "Save Text"));

        Assert.Contains("Save Text", cut.Markup);
    }

    [Fact]
    public void Interop_OK()
    {
        var cut = Context.RenderComponent<Handwritten>();

        // 反射设置内部 Interop 属性为 null
        var pi = cut.Instance.GetType().GetProperty("Interop", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        pi.SetValue(cut.Instance, null);
        cut.Dispose();
    }

    [Fact]
    public async Task HandwrittenBase64_OK()
    {
        var ret = false;
        var cut = Context.RenderComponent<Handwritten>(builder =>
        {
            builder.Add(s => s.HandwrittenBase64, EventCallback.Factory.Create<string>(this, v =>
            {
                ret = true;
            }));
        });

        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.OnValueChanged("test");
            var result = cut.Instance.Result;
            Assert.True(ret);
            Assert.Equal("test", result);
        });
    }
}
