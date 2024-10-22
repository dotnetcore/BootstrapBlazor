// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Player 组件示例
/// </summary>
public partial class Players
{
    private string _url = "https://bitdash-a.akamaihd.net/content/sintel/hls/playlist.m3u8";

    private List<SelectedItem> Items { get; } = [];

    private Player _hlsPlayer = default!;

    private readonly PlayerOptions _options = new();

    private readonly PlayerOptions _hlsOptions = new();

    private readonly PlayerOptions _audioOptions = new();

    private readonly PlayerOptions _youtubeOptions = new();

    private readonly PlayerOptions _vimeoOptions = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items.AddRange(new SelectedItem[]
        {
            new("https://rtvelivestream.akamaized.net/rtvesec/la1/la1_main.m3u8", "TestVideo1"),
            new("https://d2zihajmogu5jn.cloudfront.net/bipbop-advanced/bipbop_16x9_variant.m3u8", "TestVideo2"),
            new("https://test-streams.mux.dev/x36xhzz/x36xhzz.m3u8", "TestVideo3"),
            new("https://res.cloudinary.com/dannykeane/video/upload/sp_full_hd/q_80:qmax_90,ac_none/v1/dk-memoji-dark.m3u8", "TestVideo4"),
            new("https://devstreaming-cdn.apple.com/videos/streaming/examples/img_bipbop_adv_example_fmp4/master.m3u8", "TestVideo5"),
            new("https://moctobpltc-i.akamaihd.net/hls/live/571329/eight/playlist.m3u8", "TestVideo6"),
            new("https://cph-p2p-msl.akamaized.net/hls/live/2000341/test/master.m3u8", "TestVideo7"),
            new("https://demo.unified-streaming.com/k8s/features/stable/video/tears-of-steel/tears-of-steel.mp4/.m3u8", "TestVideo8"),
            new("https://diceyk6a7voy4.cloudfront.net/e78752a1-2e83-43fa-85ae-3d508be29366/hls/fitfest-sample-1_Ott_Hls_Ts_Avc_Aac_16x9_1280x720p_30Hz_6.0Mbps_qvbr.m3u8", "TestVideo9")
        });

        _options.Source.Poster = "https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-HD.jpg";
        _options.Source.Sources.AddRange(new PlayerSources[]
        {
            new() { Url = "https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-576p.mp4", Type = "video/mp4", Size = 576 },
            new() { Url = "https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-720p.mp4", Type = "video/mp4", Size = 720 },
            new() { Url = "https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-1080p.mp4", Type = "video/mp4", Size = 1080 },
            new() { Url = "https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-1440p.mp4", Type = "video/mp4", Size = 1440 }
        });

        _options.Source.Tracks.AddRange(new PlayerTracks[]
        {
            new() { Src = "https://cdn.plyr.io/static/demo/video/View_From_A_Blue_Moon_Trailer-HD.en.vtt", SrcLang = "en", Label = "English", Default = true },
            new() { Src = "https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-HD.fr.vtt", SrcLang = "fr", Label = "French" }
        });

        _options.Thumbnail.Enabled = true;
        _options.Thumbnail.PicNum = 184;
        _options.Thumbnail.Urls.Add("https://cdn.plyr.io/static/demo/thumbs/100p-00001.jpg");
        _options.Thumbnail.Urls.Add("https://cdn.plyr.io/static/demo/thumbs/100p-00002.jpg");
        _options.Thumbnail.Urls.Add("https://cdn.plyr.io/static/demo/thumbs/100p-00003.jpg");
        _options.Thumbnail.Urls.Add("https://cdn.plyr.io/static/demo/thumbs/100p-00004.jpg");

        _options.Markers.Enabled = true;
        _options.Markers.Points.Add(new PlayerPoint() { Time = 10, Label = "First Marker" });
        _options.Markers.Points.Add(new PlayerPoint() { Time = 50, Label = "Second Marker" });

        // hls
        _hlsOptions.IsHls = true;
        _hlsOptions.Poster = "https://bitdash-a.akamaihd.net/content/sintel/poster.png";
        _hlsOptions.Source.Sources.Add(new PlayerSources { Url = _url, Type = "application/x-mpegURL" });

        _hlsOptions.Markers.Enabled = true;
        _hlsOptions.Markers.Points.Add(new PlayerPoint() { Time = 60, Label = "First Marker" });
        _hlsOptions.Markers.Points.Add(new PlayerPoint() { Time = 300, Label = "Second Marker" });

        // audio
        _audioOptions.Source.Type = PlayerMode.Audio;
        _audioOptions.Source.Sources.AddRange(new PlayerSources[]
        {
            new() { Url = "https://cdn.plyr.io/static/demo/Kishi_Bashi_-_It_All_Began_With_a_Burst.mp3", Type = "audio/mp3" },
            new() { Url = "https://cdn.plyr.io/static/demo/Kishi_Bashi_-_It_All_Began_With_a_Burst.ogg", Type = "audio/ogg" }
        });

        // youtube
        _youtubeOptions.Source.Sources.Add(new PlayerSources { Url = "https://youtube.com/watch?v=bTqVqk7FSmY", Provider = "youtube" });

        // vimeo
        _vimeoOptions.Source.Sources.Add(new PlayerSources { Url = "https://vimeo.com/40648169", Provider = "vimeo" });
    }

    private Task ChangeUrl(SelectedItem e)
    {
        _url = e.Value;

        var options = new PlayerOptions();
        options.Source.Sources.Add(new PlayerSources { Url = _url, Type = "application/x-mpegURL" });
        options.Markers.Enabled = true;
        options.Markers.Points.Add(new PlayerPoint() { Time = 10, Label = "First Marker" });
        options.Markers.Points.Add(new PlayerPoint() { Time = 60, Label = "Second Marker" });
        _hlsPlayer.Reload(options);

        StateHasChanged();
        return Task.CompletedTask;
    }
}
