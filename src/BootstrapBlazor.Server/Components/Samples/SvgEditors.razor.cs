﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// SvgEditor 示例文档
/// </summary>
public partial class SvgEditors
{
    [NotNull]
    private ConsoleLogger? Console { get; set; }

    private Task OnSaveChanged(string content)
    {
        Console.Log(content);
        return Task.CompletedTask;
    }
}
