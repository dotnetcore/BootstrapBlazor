// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class TotpServiceTest
{
    [Fact]
    public void TotpService_Ok()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBootstrapBlazor();

        var provider = serviceCollection.BuildServiceProvider();
        var service = provider.GetRequiredService<ITotpService>();
        var data = service.GenerateSecretKey();
        Assert.Equal("OMM2LVLFX6QJHMYI", data);
        Assert.Equal("123456", service.Compute("OMM2LVLFX6QJHMYI"));
        Assert.Equal("otpauth://totp/BootstrapBlazor?secret=OMM2LVLFX6QJHMYI&issuer=Simulator", service.GenerateOtpUri());
        Assert.Empty(service.GetSecretKeyBytes(""));
        Assert.Equal(30, service.GetRemainingSeconds());
        Assert.False(service.Verify("123456"));
        Assert.Equal(30, service.Instance.GetRemainingSeconds());
        Assert.False(service.Instance.Verify("123456"));
    }
}
