// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// QRCodes
/// </summary>
public sealed partial class QRCodes
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnGenerated()
    {
        Logger.Log(Localizer["SuccessText"]);
        return Task.CompletedTask;
    }

    private Task OnCleared()
    {
        Logger.Log(Localizer["ClearText"]);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = nameof(QRCode.PlaceHolder),
            Description = Localizer[nameof(QRCode.PlaceHolder)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["QRCodesPlaceHolderValue"]
        },
        new()
        {
            Name = nameof(QRCode.Width),
            Description = Localizer[nameof(QRCode.Width)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(QRCode.ClearButtonText),
            Description = Localizer[nameof(QRCode.ClearButtonText)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["QRCodesClearButtonTextValue"]
        },
        new()
        {
            Name = nameof(QRCode.ClearButtonIcon),
            Description = Localizer[nameof(QRCode.ClearButtonIcon)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(QRCode.GenerateButtonText),
            Description = Localizer[nameof(QRCode.GenerateButtonText)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["QRCodesGenerateButtonTextValue"]
        },
        new()
        {
            Name = nameof(QRCode.GenerateButtonIcon),
            Description = Localizer[nameof(QRCode.GenerateButtonIcon)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(QRCode.ShowButtons),
            Description = Localizer[nameof(QRCode.ShowButtons)],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(QRCode.DarkColor),
            Description = Localizer[nameof(QRCode.DarkColor)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(QRCode.LightColor),
            Description = Localizer[nameof(QRCode.LightColor)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(QRCode.OnGenerated),
            Description = Localizer[nameof(QRCode.OnGenerated)],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(QRCode.OnCleared),
            Description = Localizer[nameof(QRCode.OnCleared)],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
