// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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

    private Step? _step6;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Items =
        [
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
        ];
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

    private AttributeItem[] GetAttributes() =>
    [
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
    ];

    private AttributeItem[] GetStepItemAttributes() =>
    [
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
    ];
}
