// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// TimePicker 组件示例代码
/// </summary>
public partial class TimePickers
{
    [Inject]
    [NotNull]
    private IStringLocalizer<TimePickers>? Localizer { get; set; }

}
