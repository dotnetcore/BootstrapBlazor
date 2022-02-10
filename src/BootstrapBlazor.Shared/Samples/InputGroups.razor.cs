// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class InputGroups
{
    /// <summary>
    /// 
    /// </summary>
    public string BindValue { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string StringAt { get; set; } = "@";
    /// <summary>
    /// 
    /// </summary>
    public string StringMailServer { get; set; } = "163.com";
    /// <summary>
    /// 
    /// </summary>

    private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
{
            new SelectedItem ("", "请选择 ..."),
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海")
};

    private Foo Model { get; set; } = new Foo() { Count = 10 };


    private IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
               
        };
    }
}
