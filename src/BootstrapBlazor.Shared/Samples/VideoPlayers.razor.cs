// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// VideoPlayers
/// </summary>
public partial class VideoPlayers
{
    private string MineType = "video/mp4";
    private string Url = "//vjs.zencdn.net/v/oceans.mp4";

    [NotNull]
    private VideoPlayer? Player { get; set; }

    private List<string> VideoList { get; } = new()
    {
        "https://rtvelivestream.akamaized.net/rtvesec/la1/la1_main.m3u8",
        "https://d2zihajmogu5jn.cloudfront.net/bipbop-advanced/bipbop_16x9_variant.m3u8",
        "https://test-streams.mux.dev/x36xhzz/x36xhzz.m3u8",
        "https://res.cloudinary.com/dannykeane/video/upload/sp_full_hd/q_80:qmax_90,ac_none/v1/dk-memoji-dark.m3u8",
        "https://devstreaming-cdn.apple.com/videos/streaming/examples/img_bipbop_adv_example_fmp4/master.m3u8",
        "https://moctobpltc-i.akamaihd.net/hls/live/571329/eight/playlist.m3u8",
        "https://cph-p2p-msl.akamaized.net/hls/live/2000341/test/master.m3u8",
        "https://demo.unified-streaming.com/k8s/features/stable/video/tears-of-steel/tears-of-steel.mp4/.m3u8",
        "https://diceyk6a7voy4.cloudfront.net/e78752a1-2e83-43fa-85ae-3d508be29366/hls/fitfest-sample-1_Ott_Hls_Ts_Avc_Aac_16x9_1280x720p_30Hz_6.0Mbps_qvbr.m3u8"
    };

    private List<SelectedItem> Items { get; } = new();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        for (var i = 0; i < VideoList.Count; i++)
        {
            Items.Add(new SelectedItem { Text = $"TestVideo{i}", Value = VideoList[i] });
        }
        Items.Add(new SelectedItem { Text = "Mp4", Value = "//vjs.zencdn.net/v/oceans.mp4" });
    }

    private async Task ChangeURL(SelectedItem e)
    {
        Url = e.Value;
        MineType = e.Value.EndsWith("mp4") ? "video/mp4" : "application/x-mpegURL";
        StateHasChanged();
        await Apply();
    }

    private async Task Apply()
    {
        await Player.SetPoster("//vjs.zencdn.net/v/oceans.png");
        await Player.Reload(Url, MineType);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new() {
            Name = nameof(VideoPlayer.Url),
            Description = Localizer["ResourceUrl"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(VideoPlayer.MineType),
            Description = Localizer["MineTypeDesc"],
            Type = "string?",
            ValueList = Localizer["ValueList"],
            DefaultValue = "application/x-mpegURL"
        },
        new() {
            Name = nameof(VideoPlayer.Width),
            Description = Localizer["Width"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "300"
        },
        new() {
            Name = nameof(VideoPlayer.Height),
            Description = Localizer["Height"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "200"
        },
        new() {
            Name = nameof(VideoPlayer.Controls),
            Description = Localizer["ShowBar"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = nameof(VideoPlayer.Autoplay),
            Description = Localizer["AutoPaly"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = nameof(VideoPlayer.Poster),
            Description = Localizer["Poster"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Reload(string url, string type)",
            Description = Localizer["SwitchResource"],
            Type = "async Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "SetPoster(string poster)",
            Description = Localizer["SetPoster"],
            Type = "async Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnError",
            Description = Localizer["OnError"],
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };
}
