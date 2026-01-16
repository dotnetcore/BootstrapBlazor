// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Input 组件示例代码
/// </summary>
public partial class Inputs
{
    private string? PlaceHolderText { get; set; }

    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private BootstrapInput<string>? Input { get; set; }

    [NotNull]
    private Foo? PlaceholderModel { get; set; }

    [NotNull]
    private Foo? LabelsModel { get; set; }

    [NotNull]
    private Foo? ValidateModel { get; set; }

    [NotNull]
    private Foo? GenericModel { get; set; }

    private byte[] ByteArray { get; set; } = [0x01, 0x12, 0x34, 0x56];

    [NotNull]
    private Foo? FormatModel { get; set; }

    [NotNull]
    private Foo? TrimModel { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PlaceHolderText = Localizer["PlaceHolder"];
        Model = new Foo() { Name = Localizer["TestName"] };
        PlaceholderModel = new Foo() { Name = Localizer["TestName"] };
        LabelsModel = new Foo() { Name = Localizer["TestName"] };
        ValidateModel = new Foo() { Name = Localizer["TestName"] };
        GenericModel = new Foo() { Name = Localizer["TestName"] };
        FormatModel = new Foo() { Name = Localizer["TestName"] };
        TrimModel = new Foo() { Name = Localizer["TestName"] };
    }

    private Task OnEnterAsync(string val)
    {
        Logger.Log($"Enter {Localizer["InputsKeyboardLog"]}: {val}");
        return Task.CompletedTask;
    }

    private Task OnEscAsync(string val)
    {
        Logger.Log($"Esc {Localizer["InputsKeyboardLog"]}: {val}");
        return Task.CompletedTask;
    }

    private async Task OnEnterSelectAllAsync(string val)
    {
        Logger.Log($"Enter call SelectAllText {Localizer["InputsKeyboardLog"]}: {val}");
        await Input.SelectAllTextAsync();
    }

    private static string DateTimeFormatter(DateTime source) => source.ToString("yyyy-MM-dd");

    private static string ByteArrayFormatter(byte[] source) => Convert.ToBase64String(source);
}
