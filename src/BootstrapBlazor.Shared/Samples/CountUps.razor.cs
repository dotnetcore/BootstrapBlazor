// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// CountUps
/// </summary>
public partial class CountUps
{
    private int Value { get; set; }

    private static Random Rnd { get; } = new();

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        OnUpdate();
    }

    private void OnUpdate()
    {
        Value = Rnd.Next(1234, 99999);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Value",
            Description = Localizer["Value"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnCompleted",
            Description = Localizer["OnCompleted"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
