// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 图标库
/// </summary>
public partial class Topologys
{
    private string? Content { get; set; } = "{\"x\":0,\"y\":0,\"scale\":1,\"pens\":[{\"text\":\"正方形\",\"width\":100,\"height\":100,\"name\":\"square\",\"form\":[{\"key\":\"text\",\"type\":\"text\",\"name\":\"文本\"}],\"id\":\"af2712f\",\"children\":[],\"x\":399,\"y\":160,\"lineWidth\":1,\"fontSize\":12,\"lineHeight\":1.5,\"anchors\":[{\"id\":\"0\",\"penId\":\"af2712f\",\"x\":0.5,\"y\":0},{\"id\":\"1\",\"penId\":\"af2712f\",\"x\":1,\"y\":0.5},{\"id\":\"2\",\"penId\":\"af2712f\",\"x\":0.5,\"y\":1},{\"id\":\"3\",\"penId\":\"af2712f\",\"x\":0,\"y\":0.5}],\"rotate\":0}],\"origin\":{\"x\":0,\"y\":0},\"center\":{\"x\":0,\"y\":0},\"paths\":{},\"component\":false,\"version\":\"1.1.5\",\"websocket\": \"\"}";

    private Task<IEnumerable<TopologyItem>> GetData(CancellationToken token)
    {
        var data = new List<TopologyItem>()
        {
            new TopologyItem()
            {
                Text = DateTime.Now.ToString(),
                ID = "af2712f",
                Tag = "zhengfangxing"
            }
        };
        return Task.FromResult(data.AsEnumerable());
    }
}
