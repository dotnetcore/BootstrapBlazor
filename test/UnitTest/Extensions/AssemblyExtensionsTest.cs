// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;
using System.Runtime.Loader;

namespace UnitTest.Extensions;

public class AssemblyExtensionsTest
{
    [Fact]
    public void GetUniqueName_Ok()
    {
        var type = Type.GetType("BootstrapBlazor.Components.AssemblyExtensions, BootstrapBlazor");
        Assert.NotNull(type);

        var methodInfo = type.GetMethod("GetUniqueName", BindingFlags.Static | BindingFlags.Public);
        Assert.NotNull(methodInfo);

        var actual = methodInfo.Invoke(null, [GetType().Assembly]);
        Assert.Equal(GetType().Assembly.GetName().Name, actual);

        actual = methodInfo.Invoke(null, [new MockAssembly()]);
        Assert.Equal("", actual);

        var loader = new AssemblyLoadContext("Test", true);
        var assemblyFile = Path.Combine(AppContext.BaseDirectory, "Plugins", "Test.dll");
        Assert.True(File.Exists(assemblyFile));

        var assembly = loader.LoadFromAssemblyPath(assemblyFile);
        actual = methodInfo.Invoke(null, [assembly]);
        Assert.Contains("Test-", actual?.ToString());
        loader.Unload();
    }

    [Fact]
    public void GetUniqueTypeName_Ok()
    {
        var type = Type.GetType("BootstrapBlazor.Components.TypeExtensions, BootstrapBlazor");
        Assert.NotNull(type);

        var methodInfo = type.GetMethod("GetUniqueTypeName", BindingFlags.Static | BindingFlags.Public);
        Assert.NotNull(methodInfo);

        var actual = methodInfo.Invoke(null, [GetType()]);
        Assert.Equal(GetType().FullName, actual);

        actual = methodInfo.Invoke(null, [new MockTypeInfo()]);
        Assert.Equal("", actual);

        var loader = new AssemblyLoadContext("Test", true);
        var assemblyFile = Path.Combine(AppContext.BaseDirectory, "Plugins", "Test.dll");
        Assert.True(File.Exists(assemblyFile));

        var assembly = loader.LoadFromAssemblyPath(assemblyFile);
        type = assembly.GetType("ConsoleApp3.TestClass");
        actual = methodInfo.Invoke(null, [type]);
        Assert.Contains("ConsoleApp3.TestClass-", actual?.ToString());

        loader.Unload();
    }

    class MockTypeInfo : TypeDelegator
    {
        public override string? FullName => null;

        public override bool IsCollectible => false;

        public override RuntimeTypeHandle TypeHandle => new();
    }

    class MockAssembly : Assembly
    {
        public override AssemblyName GetName() => new();

        public override bool IsCollectible => false;
    }
}
