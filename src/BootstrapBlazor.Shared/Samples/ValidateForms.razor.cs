// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// ValidateForms
/// </summary>
public partial class ValidateForms
{
    #region 参数说明
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Model",
            Description = Localizer["Model"],
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ValidateAllProperties",
            Description = Localizer["ValidateAllProperties"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(ValidateForm.DisableAutoSubmitFormByEnter),
            Description = Localizer[nameof(ValidateForm.DisableAutoSubmitFormByEnter)],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowRequiredMark",
            Description = Localizer["ShowRequiredMark"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowLabelTooltip",
            Description = Localizer["ShowLabelTooltip"],
            Type = "bool?",
            ValueList = "true/false/null",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "ChildContent",
            Description = Localizer["ChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnValidSubmit",
            Description = Localizer["OnValidSubmit"],
            Type = "EventCallback<EditContext>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnInvalidSubmit",
            Description = Localizer["OnInvalidSubmit"],
            Type = "EventCallback<EditContext>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new MethodItem()
        {
            Name = "SetError",
            Description = Localizer["SetError"],
            Parameters = "PropertyName, ErrorMessage",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = "Validate",
            Description = Localizer["Validate"],
            Parameters = " — ",
            ReturnValue = "boolean"
        }
    };
    #endregion
}
