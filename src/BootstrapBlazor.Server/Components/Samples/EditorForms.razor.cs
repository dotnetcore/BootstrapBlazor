// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// EditorForms
/// </summary>
public sealed partial class EditorForms
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbies { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    private List<string> _ignoreItems = [];
    private EditorFormGroupType _groupType = EditorFormGroupType.GroupBox;

    private Task OnSwitchIgnoreItems()
    {
        if (_ignoreItems.Count != 0)
        {
            _ignoreItems.Clear();
        }
        else
        {
            _ignoreItems = [nameof(Foo.Hobby)];
        }
        return Task.CompletedTask;
    }

    private List<SelectedItem> DummyItems { get; } =
    [
        new SelectedItem("1", "1"),
        new SelectedItem("2", "2"),
        new SelectedItem("3", "3"),
        new SelectedItem("4", "4"),
        new SelectedItem("5", "5")
    ];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Hobbies = Foo.GenerateHobbies(FooLocalizer);
        ValidateModel = Foo.Generate(FooLocalizer);
        Model = new Foo()
        {
            Name = Localizer["TestName"],
            Count = 1,
            Address = Localizer["TestAddress"],
            DateTime = new DateTime(1997, 12, 05),
            Education = EnumEducation.Middle
        };
        Model.Hobby = Foo.GenerateHobbies(FooLocalizer).Take(3).Select(i => i.Text);
    }

    [NotNull]
    private Foo? ValidateModel { get; set; }

    private List<AttributeItem> GetEditorItemAttributes() =>
    [
        new()
        {
            Name = "Field",
            Description = Localizer["Att9"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FieldType",
            Description = Localizer["Att10"],
            Type = "Type",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Editable",
            Description = Localizer["Att11"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Readonly",
            Description = Localizer["Att12"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Text",
            Description = Localizer["Att13"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditTemplate",
            Description = Localizer["Att14"],
            Type = "RenderFragment<object>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
