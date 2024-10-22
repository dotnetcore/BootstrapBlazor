// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Speeches;

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
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(SpeechWave.Show),
            Description = Localizer["ShowAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(SpeechWave.ShowUsedTime),
            Description = Localizer["ShowUsedTimeAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(SpeechWave.OnTimeout),
            Description = Localizer["OnTimeoutAttr"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(SpeechWave.TotalTime),
            Description = Localizer["TotalTimeSecondAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "60000"
        }
    ];
}
