// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Repeaters
/// </summary>
public partial class Repeaters
{
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private List<Foo>? EmptyItems { get; set; } = new();

    protected override void OnInitialized()
    {
        // 生成数据
        Items = Foo.GenerateFoo(FooLocalizer, 4);
    }

    private void OnClick()
    {
        var id = Items.Any() ? Items.Max(i => i.Id) : 0;
        var foo = Foo.Generate(FooLocalizer);
        foo.Id = id + 1;
        Items.Add(foo);
    }

    private void OnDelete(Foo foo)
    {
        Items.Remove(foo);
    }

    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.Items),
            Description = "数据集合",
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.ShowLoading),
            Description = "是否显示正在加载信息",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.ShowEmpty),
            Description = "是否显示空数据提示信息",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.LoadingTemplate),
            Description = "正在加载模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.EmptyTemplate),
            Description = "空数据模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.ItemTemplate),
            Description = "重复项模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.ContainerTemplate),
            Description = "容器模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(Repeater<Foo>.EmptyText),
            Description = "空数据提示信息",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
