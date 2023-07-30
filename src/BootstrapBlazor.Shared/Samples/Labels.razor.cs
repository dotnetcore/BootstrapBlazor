// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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

        Dummy1 = Foo.Generate(LocalizerFoo);
        Dummy2 = Foo.Generate(LocalizerFoo);
        Dummy3 = Foo.Generate(LocalizerFoo);
        Dummy4 = Foo.Generate(LocalizerFoo);
    }
}
