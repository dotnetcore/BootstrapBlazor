// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Typed 组件示例
/// </summary>
public partial class Typeds
{
    private readonly TypedOptions _options = new();

    private void OnUpdate()
    {
        _options.Text = ["<code>BootstrapBlazor</code> is an enterprise-level UI component library", "你可以试试看 <b>Blazor</b> 框架"];
        _options.TypeSpeed = 70;
        _options.BackSpeed = 30;
        _options.Loop = true;
    }

    private static AttributeItem[] TypedAttributes =>
    [
        new()
        {
            Name = nameof(TypedOptions.Text),
            Description = "strings strings to be typed",
            Type = "List<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TypedOptions.TypeSpeed),
            Description = "typeSpeed type speed in milliseconds",
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TypedOptions.BackSpeed),
            Description = "backSpeed backspacing speed in milliseconds",
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TypedOptions.BackDelay),
            Description = "backDelay time before backspacing in milliseconds",
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TypedOptions.SmartBackspace),
            Description = "smartBackspace only backspace what doesn't match the previous string default true",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TypedOptions.Shuffle),
            Description = "shuffle the strings",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TypedOptions.Loop),
            Description = "loop loop strings default false",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TypedOptions.LoopCount),
            Description = "loopCount amount of loops default Infinity",
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — ",
        },
        new()
        {
            Name = nameof(TypedOptions.ShowCursor),
            Description = "showCursor show cursor",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(TypedOptions.CursorChar),
            Description = "cursorChar character for cursor",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "|"
        },
        new()
        {
            Name = nameof(TypedOptions.ContentType),
            Description = "contentType 'html' or 'null' for plaintext",
            Type = "string",
            ValueList = "html|null",
            DefaultValue = "html"
        }
    ];
}
