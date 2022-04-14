// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 语音识别波形图示例
/// </summary>
public partial class SpeechWaves
{
    private bool IsShow { get; set; }

    private string ButtonText => IsShow ? "隐藏" : "显示";

    private void OnClickShow()
    {
        IsShow = !IsShow;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = nameof(SpeechWave.Show),
            Description = "是否开始",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(SpeechWave.ShowUsedTime),
            Description = "是否显示时长",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(SpeechWave.OnTimeout),
            Description = "识别结束后超时回调方法",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(SpeechWave.TotalTimeSecond),
            Description = "语音识别设置总时长超出过调用 OnTimeout 回调",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "60"
        }
    };
}
