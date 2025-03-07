// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.CompilerServices;

namespace UnitTest.Services;

public class BootstrapBlazorRootRegisterServiceTest
{
    [Fact]
    public void Provider_Ok()
    {
        var service = new BootstrapBlazorRootRegisterService();
        var exception = Assert.ThrowsAny<InvalidOperationException>(() => service.RemoveProvider(new object(), new BootstrapBlazorRootContent()));
        Assert.Equal("There are no content providers with the given root ID 'System.Object'.", exception.Message);

        var identifier = new object();
        service.AddProvider(identifier, new BootstrapBlazorRootContent());
        exception = Assert.ThrowsAny<InvalidOperationException>(() => service.RemoveProvider(identifier, new BootstrapBlazorRootContent()));
        Assert.Equal("The provider was not found in the providers list of the given root ID 'System.Object'.", exception.Message);
    }

    [Fact]
    public void Subscribe_Ok()
    {
        var service = new BootstrapBlazorRootRegisterService();
        var identifier = new object();
        service.Subscribe(identifier, new BootstrapBlazorRootOutlet());
        service.Subscribe(identifier, new BootstrapBlazorRootOutlet());
        service.Unsubscribe(new object());
    }

    [Fact]
    public void NotifyContentProviderChanged_Ok()
    {
        var service = new BootstrapBlazorRootRegisterService();
        var exception = Assert.ThrowsAny<InvalidOperationException>(() => service.NotifyContentProviderChanged(new object(), new BootstrapBlazorRootContent()));
        Assert.Equal("There are no content providers with the given root ID 'System.Object'.", exception.Message);


        var identifier = new object();
        service.AddProvider(identifier, new BootstrapBlazorRootContent());

        var providers = service.GetProviders(identifier);
        providers.Clear();
        service.NotifyContentProviderChanged(identifier, new BootstrapBlazorRootContent());
    }

    [Fact]
    public void GetCurrentProviderContentOrDefault_Ok()
    {
        var service = new BootstrapBlazorRootRegisterService();
        Assert.Null(GetCurrentProviderContentOrDefault(service, []));

        var provider = new BootstrapBlazorRootContent();
        service.AddProvider(new object(), provider);
        Assert.Equal(provider, GetCurrentProviderContentOrDefault(service, [provider]));
    }


    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "GetCurrentProviderContentOrDefault")]
    static extern BootstrapBlazorRootContent? GetCurrentProviderContentOrDefault(BootstrapBlazorRootRegisterService @this, List<BootstrapBlazorRootContent> providers);

}
