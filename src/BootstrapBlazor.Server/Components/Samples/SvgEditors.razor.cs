// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;
public partial class SvgEditors
{
    [NotNull]
    private BootstrapBlazor.Components.ConsoleLogger? Console { get; set; }

    private Task OnSaveChanged(string content)
    {
        Console.Log(content);
        return Task.CompletedTask;
    }
}
