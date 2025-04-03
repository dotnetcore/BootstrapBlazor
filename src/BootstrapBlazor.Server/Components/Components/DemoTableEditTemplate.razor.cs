// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// DemoTableEditTemplate component
/// </summary>
public partial class DemoTableEditTemplate
{
    /// <summary>
    /// Gets or sets the Foo instance.
    /// </summary>
    [Parameter]
    [NotNull]
    public Foo? Model { get; set; }

    private IEnumerable<SelectedItem>? Hobbies { get; set; }

    private string? EducationDesc => Model.Education switch
    {
        EnumEducation.Primary => Localizer["TablesEditTemplateDisplayDetail1"],
        EnumEducation.Middle => Localizer["TablesEditTemplateDisplayDetail2"],
        _ => ""
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Hobbies = Foo.GenerateHobbies(LocalizerFoo);
    }
}
