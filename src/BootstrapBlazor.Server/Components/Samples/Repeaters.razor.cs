// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Repeaters
/// </summary>
public partial class Repeaters
{
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private List<Foo>? EmptyItems { get; set; } = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        // 生成数据
        Items = Foo.GenerateFoo(FooLocalizer, 4);
    }

    private void OnClick()
    {
        var id = Items.Count > 0 ? Items.Max(i => i.Id) : 0;
        var foo = Foo.Generate(FooLocalizer);
        foo.Id = id + 1;
        Items.Add(foo);
    }

    private void OnDelete(Foo foo)
    {
        Items.Remove(foo);
    }
}
