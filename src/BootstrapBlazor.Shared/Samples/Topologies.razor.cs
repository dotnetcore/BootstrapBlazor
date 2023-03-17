// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 人机交互界面
/// </summary>
public partial class Topologies
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(Topology.Content),
            Description = "Load Graphical Json Content",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Topology.Interval),
            Description = "Polling interval in polling mode",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2000"
        },
        new AttributeItem() {
            Name = nameof(Topology.OnQueryAsync),
            Description = "Get push data callback delegate method",
            Type = "Func<CancellationToken, Task<IEnumerable<TopologyItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Topology.OnBeforePushData),
            Description = "Callback method before starting to push data",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
