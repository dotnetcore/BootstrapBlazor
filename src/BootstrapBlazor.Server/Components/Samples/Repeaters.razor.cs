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

    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(Repeater<Foo>.Items),
            Description = "数据集合",
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Repeater<Foo>.ShowLoading),
            Description = "是否显示正在加载信息",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Repeater<Foo>.ShowEmpty),
            Description = "是否显示空数据提示信息",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Repeater<Foo>.LoadingTemplate),
            Description = "正在加载模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Repeater<Foo>.EmptyTemplate),
            Description = "空数据模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Repeater<Foo>.ItemTemplate),
            Description = "重复项模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Repeater<Foo>.ContainerTemplate),
            Description = "容器模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Repeater<Foo>.EmptyText),
            Description = "空数据提示信息",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
