// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 可选择表格组件示例
/// </summary>
public partial class SelectTables
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SelectTables>? Localizer { get; set; }

    private List<Foo> _items = default!;

    private List<Foo> _colorItems = default!;

    private List<Foo> _templateItems = default!;

    private List<Foo> _disabledItems = default!;

    private List<Foo> _validateFormItems = default!;

    private Foo? _foo;

    private Foo? _colorFoo;

    private Foo? _templateFoo;

    private Foo? _disabledFoo;

    private SelectTableMode Model = new();

    /// <summary>
    ///
    /// </summary>
    protected override void OnInitialized()
    {
        _items = Foo.GenerateFoo(LocalizerFoo);
        _colorItems = Foo.GenerateFoo(LocalizerFoo);
        _templateItems = Foo.GenerateFoo(LocalizerFoo);
        _disabledItems = Foo.GenerateFoo(LocalizerFoo);
        _validateFormItems = Foo.GenerateFoo(LocalizerFoo);
    }

    private static string? GetTextCallback(Foo? foo) => foo?.Name;

    class SelectTableMode
    {
        public Foo? Foo { get; set; }
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Items",
            Description = Localizer["AttributeItems"],
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TableColumns",
            Description = Localizer["AttributeTableColumns"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = Localizer["AttributeColor"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["AttributeIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowAppendArrow",
            Description = Localizer["AttributeShowAppendArrow"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "GetTextCallback",
            Description = Localizer["AttributeGetTextCallback"],
            Type = "Func<TItem, string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PlaceHolder",
            Description = Localizer["AttributePlaceHolder"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Height",
            Description = Localizer["AttributeHeight"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "486"
        },
        new()
        {
            Name = "Template",
            Description = Localizer["AttributeTemplate"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
