// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
