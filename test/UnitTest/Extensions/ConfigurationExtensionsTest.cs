// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
