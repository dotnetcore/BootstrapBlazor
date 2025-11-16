// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class IpAddressTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task IpAddress_Ok()
    {
        var cut = Context.Render<IpAddress>();
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
        var cut = Context.Render<IpAddress>();
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
        var cut = Context.Render<IpAddress>();
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
        var cut = Context.Render<ValidateForm>(pb =>
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
