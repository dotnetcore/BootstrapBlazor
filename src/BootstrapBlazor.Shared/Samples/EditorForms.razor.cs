// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public sealed partial class EditorForms
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<EditorForms>? Localizer { get; set; }

    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private Foo? ValidateModel { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbys { get; set; }

    private List<SelectedItem> DummyItems { get; } = new List<SelectedItem>()
        {
            new SelectedItem("1", "1"),
            new SelectedItem("2", "2"),
            new SelectedItem("3", "3"),
            new SelectedItem("4", "4"),
            new SelectedItem("5", "5")
        };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Hobbys = Foo.GenerateHobbys(FooLocalizer);
        Model = new Foo()
        {
            Name = Localizer["TestAddr"],
            Count = 23,
            Address = Localizer["TestAddr"],
            DateTime = new DateTime(1997, 12, 05),
            Education = EnumEducation.Middel
        };
        ValidateModel = new Foo()
        {
            Name = Localizer["TestName"],
            Count = 23,
            DateTime = new DateTime(1997, 12, 05),
            Education = EnumEducation.Middel
        };
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Model",
                Description = Localizer["Att1"],
                Type = "TModel",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FieldItems",
                Description = Localizer["Att2"],
                Type = "RenderFragment<TModel>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Buttons",
                Description = Localizer["Att3"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = nameof(EditorForm<Foo>.IsDisplay),
                Description = Localizer["IsDisplay"],
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowLabel",
                Description = Localizer["Att4"],
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "AutoGenerateAllItem",
                Description = Localizer["Att5"],
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ItemsPerRow",
                Description = Localizer["Att6"],
                Type = "int?",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RowType",
                Description = Localizer["Att7"],
                Type = "RowType",
                ValueList = "Row|Inline",
                DefaultValue = "Row"
            },
            new AttributeItem() {
                Name = "LabelAlign",
                Description = Localizer["Att8"],
                Type = "Alignment",
                ValueList = "None|Left|Center|Right",
                DefaultValue = "None"
            }
    };

    private IEnumerable<AttributeItem> GetEditorItemAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Field",
                Description = Localizer["Att9"],
                Type = "TValue",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FieldType",
                Description = Localizer["Att10"],
                Type = "Type",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Editable",
                Description = Localizer["Att11"],
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Readonly",
                Description = Localizer["Att12"],
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Text",
                Description = Localizer["Att13"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditTemplate",
                Description = Localizer["Att14"],
                Type = "RenderFragment<object>",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
