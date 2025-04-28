// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace UnitTest.Options;

public class OtpOptionsTest
{
    [Fact]
    public void OtpOptions_Ok()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBootstrapBlazor();
        AddOptions(serviceCollection);

        var provider = serviceCollection.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<OtpOptions>>().Value;
        Assert.Equal("OMM2LVLFX6QJHMYI", options.SecretKey);
        Assert.Equal("Simulator", options.IssuerName);
        Assert.Equal("BootstrapBlazor", options.AccountName);
        Assert.Equal("BootstrapBlazor", options.UserName);
        Assert.Equal(OtpHashMode.Sha1, options.Algorithm);
        Assert.Equal(OtpType.Totp, options.Type);
        Assert.Equal(6, options.Digits);
        Assert.Equal(30, options.Period);
        Assert.Equal(0, options.Counter);
    }

    private static void AddOptions(ServiceCollection serviceCollection)
    {
        var builder = new ConfigurationBuilder();
        builder.AddInMemoryCollection(new Dictionary<string, string?>()
        {
            { "OtpOptions:SecretKey", "OMM2LVLFX6QJHMYI" },
            { "OtpOptions:IssuerName", "Simulator" },
            { "OtpOptions:AccountName", "BootstrapBlazor" },
            { "OtpOptions:UserName", "BootstrapBlazor" },
            { "OtpOptions:Algorithm", OtpHashMode.Sha1.ToString() },
            { "OtpOptions:Type", OtpType.Totp.ToString() },
            { "OtpOptions:Digits", "6" },
            { "OtpOptions:Period", "30" },
            { "OtpOptions:Counter", "0" }
        });
        var config = builder.Build();
        serviceCollection.AddSingleton<IConfiguration>(config);
    }
}
