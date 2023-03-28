// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Textareas
/// </summary>
public partial class Textareas
{
    private IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
            new AttributeItem() {
                Name = "ShowLabel",
                Description = Localizer["TextareasShowLabel"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = Localizer["TextareasDisplayText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = Localizer["TextareasIsDisabled"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "ScrollToTop",
                Description = Localizer["TextareasScrollToTop"],
                Type = "Task",
                ValueList = "-",
                DefaultValue = "-"
            },
            new AttributeItem()
            {
                Name = "ScrollTo",
                Description = Localizer["TextareasScrollTo"],
                Type = "Task",
                ValueList = "-",
                DefaultValue = "-"
            },
            new AttributeItem()
            {
                Name = "ScrollToBottom",
                Description = Localizer["TextareasScrollToBottom"],
                Type = "Task",
                ValueList = "-",
                DefaultValue = "-"
            },
            new AttributeItem(){
                Name = nameof(BootstrapBlazor.Components.Textarea.IsAutoScroll),
                Description = Localizer["TextareasAutoScroll"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },

        };
    }
}
