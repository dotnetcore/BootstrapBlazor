// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class IpAddressTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task IpAddress_Ok()
    {
        var cut = Context.RenderComponent<IpAddress>();
        cut.Contains("ipaddress form-control");
        Assert.Equal("0.0.0.0", cut.Instance.Value);

        var inputs = cut.FindAll(".ipv4-cell");
        await cut.InvokeAsync(() => inputs[0].Change("1"));
        await cut.InvokeAsync(() => inputs[1].Change("1"));
        await cut.InvokeAsync(() => inputs[2].Change("1"));
        await cut.InvokeAsync(() => inputs[3].Change("1"));
        Assert.Equal("1.1.1.1", cut.Instance.Value);
    }

    [Fact]
    public async Task IpAddress_Null()
    {
        var cut = Context.RenderComponent<IpAddress>();
        var inputs = cut.FindAll(".ipv4-cell");
        await cut.InvokeAsync(() => inputs[0].Change(new ChangeEventArgs() { Value = null }));
        await cut.InvokeAsync(() => inputs[1].Change(new ChangeEventArgs() { Value = null }));
        await cut.InvokeAsync(() => inputs[2].Change(new ChangeEventArgs() { Value = null }));
        await cut.InvokeAsync(() => inputs[3].Change(new ChangeEventArgs() { Value = null }));
        Assert.Equal("0.0.0.0", cut.Instance.Value);
    }

    [Fact]
    public async Task IpAddress_Value()
    {
        var cut = Context.RenderComponent<IpAddress>();
        var inputs = cut.FindAll(".ipv4-cell");
        await cut.InvokeAsync(() => inputs[0].Change(new ChangeEventArgs() { Value = "1234" }));
        await cut.InvokeAsync(() => inputs[1].Change(new ChangeEventArgs() { Value = "1234" }));
        await cut.InvokeAsync(() => inputs[2].Change(new ChangeEventArgs() { Value = "1234" }));
        await cut.InvokeAsync(() => inputs[3].Change(new ChangeEventArgs() { Value = "1234" }));
        Assert.Equal("123.123.123.123", cut.Instance.Value);
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        var foo = new Foo() { Name = "1.1.1.1" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<IpAddress>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
            });
        });
        cut.Contains("form-label");
    }
}
