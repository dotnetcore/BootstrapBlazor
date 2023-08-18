// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// EditorForms
/// </summary>
public sealed partial class EditorForms
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbies { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    private List<SelectedItem> DummyItems { get; } = new List<SelectedItem>()
    {
        new SelectedItem("1", "1"),
        new SelectedItem("2", "2"),
        new SelectedItem("3", "3"),
        new SelectedItem("4", "4"),
        new SelectedItem("5", "5")
    };

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
    }

    [NotNull]
    private Foo? ValidateModel { get; set; }


    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Model",
            Description = Localizer["Att1"],
            Type = "TModel",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FieldItems",
            Description = Localizer["Att2"],
            Type = "RenderFragment<TModel>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Buttons",
            Description = Localizer["Att3"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(EditorForm<Foo>.IsDisplay),
            Description = Localizer["IsDisplay"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["Att4"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowLabelTooltip",
            Description = Localizer["ShowLabelTooltip"],
            Type = "bool?",
            ValueList = "true/false/null",
            DefaultValue = "null"
        },
        new()
        {
            Name = "AutoGenerateAllItem",
            Description = Localizer["Att5"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ItemsPerRow",
            Description = Localizer["Att6"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "RowType",
            Description = Localizer["Att7"],
            Type = "RowType",
            ValueList = "Row|Inline",
            DefaultValue = "Row"
        },
        new()
        {
            Name = "LabelAlign",
            Description = Localizer["Att8"],
            Type = "Alignment",
            ValueList = "None|Left|Center|Right",
            DefaultValue = "None"
        }
    };

    private IEnumerable<AttributeItem> GetEditorItemAttributes() => new AttributeItem[]
    {
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
    };
}
