// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace UnitTest.Extensions;

public class ConfigurationExtensionsTest
{
    [Fact]
    public void GetEnvironmentInformation_Ok()
    {
        var mi = GetMethod("GetEnvironmentInformation");
        var keyValues = new Dictionary<string, string?>()
        {
            ["LOGNAME"] = "Test-UserName",
            ["ASPNETCORE_ENVIRONMENT"] = "ENVIRONMENT",
            ["ASPNETCORE_IIS_PHYSICAL_PATH"] = "IIS_PATH",
            ["VisualStudioEdition"] = "VisualStudioEdition",
            ["VisualStudioVersion"] = "VisualStudioVersion"
        };
        var config = BuildConfiguration(keyValues);
        mi.Invoke(null, new object[] { config });
    }

    [Theory]
    [InlineData("GetUserName")]
    [InlineData("GetEnvironmentName")]
    [InlineData("GetIISPath")]
    [InlineData("GetVisualStudioVersion")]
    public void GetDefaultValue_Ok(string methodName)
    {
        var mi = GetMethod(methodName);
        var keyValues = new Dictionary<string, string?>()
        {
        };
        var config = BuildConfiguration(keyValues);
        var v = mi.Invoke(null, new object[] { config, "Test" });
        Assert.Equal("Test", v);
    }

    private static MethodInfo GetMethod(string methodName)
    {
        var type = Type.GetType("Microsoft.Extensions.Configuration.ConfigurationExtensions, BootstrapBlazor")!;
        return type.GetMethod(methodName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)!;
    }

    private static IConfiguration BuildConfiguration(Dictionary<string, string?> keyValues)
    {
        var builder = new ConfigurationBuilder();
        builder.AddInMemoryCollection(keyValues);
        return builder.Build();
    }
}
