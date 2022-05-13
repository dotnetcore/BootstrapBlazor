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

    private string ButtonText => IsShow ? Localizer["ValueButtonText1"] : Localizer["ValueButtonText2"];

    private void OnClickShow()
    {
        IsShow = !IsShow;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = nameof(SpeechWave.Show),
            Description = Localizer["ShowAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(SpeechWave.ShowUsedTime),
            Description = Localizer["ShowUsedTimeAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(SpeechWave.OnTimeout),
            Description = Localizer["OnTimeoutAttr"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(SpeechWave.TotalTime),
            Description = Localizer["TotalTimeSecondAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "60000"
        }
    };
}
