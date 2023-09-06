// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Steps
/// </summary>
public sealed partial class Steps
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private List<StepOption>? Items { get; set; }

    private Step? _step1;

    private Step? _step2;

    private Step? _step3;

    private Step? _step4;

    private Step? _step5;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Items = new()
        {
            new StepOption()
            {
                Template = BootstrapDynamicComponent.CreateComponent<Counter>().Render()
            },
            new StepOption()
            {
                Template = BootstrapDynamicComponent.CreateComponent<FetchData>().Render()
            },
            new StepOption()
            {
                Template = BootstrapDynamicComponent.CreateComponent<Counter>().Render()
            }
        };
    }

    private static void PrevStep(Step? step)
    {
        step?.Prev();
    }

    private static void NextStep(Step? step)
    {
        step?.Next();
    }

    private static void ResetStep(Step? step)
    {
        step?.Reset();
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Items",
            Description = Localizer["StepsItems"],
            Type = "List<StepOption>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "StepIndex",
            Description = Localizer["StepsAttrStepIndex"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "IsVertical",
            Description = Localizer["StepsIsVertical"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };

    private IEnumerable<AttributeItem> GetStepItemAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Text",
            Description = Localizer["StepsAttrText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Title",
            Description = Localizer["StepsAttrTitle"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Icon",
            Description = Localizer["StepsAttrIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FinishedIcon",
            Description = Localizer["StepsAttrFinishedIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Description",
            Description = Localizer["StepsAttrDescription"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["StepsAttrHeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TitleTemplate",
            Description = Localizer["StepsAttrTitleTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["StepsAttrChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
