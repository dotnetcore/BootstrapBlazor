// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// EditDialogs
/// </summary>
public sealed partial class EditDialogs
{
    /// <summary>
    /// get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "Whether to show labels",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Model",
                Description = "Generic parameters are used to render the UI",
                Type = "TModel",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "Edit item collection",
                Type = "IEnumerable<IEditorItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DialogBodyTemplate",
                Description = "EditDialog Body template",
                Type = "RenderFragment<TModel>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CloseButtonText",
                Description = "Close button text",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "closure"
            },
            new AttributeItem() {
                Name = "SaveButtonText",
                Description = "Save button text",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "save"
            },
            new AttributeItem() {
                Name = "OnSaveAsync",
                Description = "Save callback delegate",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ItemsPerRow",
                Description = "Displays the number of components per line",
                Type = "int?",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RowType",
                Description = "Set the component layout",
                Type = "RowType",
                ValueList = "Row|Inline",
                DefaultValue = "Row"
            },
            new AttributeItem() {
                Name = "LabelAlign",
                Description = "Inline Label alignment in layout mode",
                Type = "Alignment",
                ValueList = "None|Left|Center|Right",
                DefaultValue = "None"
            }
        };
    }
}
