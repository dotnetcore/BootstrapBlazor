// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Label 组件示例
/// </summary>
public partial class Labels
{
    [NotNull]
    private Foo? Dummy1 { get; set; }

    [NotNull]
    private Foo? Dummy2 { get; set; }

    [NotNull]
    private Foo? Dummy3 { get; set; }

    [NotNull]
    private Foo? Dummy4 { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dummy1 = Foo.Generate(FooLocalizer);
        Dummy2 = Foo.Generate(FooLocalizer);
        Dummy3 = Foo.Generate(FooLocalizer);
        Dummy4 = Foo.Generate(FooLocalizer);
    }
}
