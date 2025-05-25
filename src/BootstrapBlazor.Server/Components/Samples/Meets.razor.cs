// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Meet 视频会议组件示例
/// </summary>
public partial class Meets : ComponentBase
{
    private MeetOption? _option;
    private Meet? _meet;
    private readonly string _domain = "meet.jit.si";

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _option = new MeetOption
        {
            RoomName = "BootstrapBlazor",
            Width = "100%",
            Height = 700,
            ConfigOverwrite = new
            {
                Lobby = new { EnableChat = false },
                HiddenPremeetingButtons = new string[] { "invite" },
                DisableInviteFunctions = true,
                ButtonsWithNotifyClick = new[] { new { key = "invite", preventExecution = true } }
            },
            UserInfo = new UserInfo() { DisplayName = "BootstrapBlazor", Email = "a@blazor.zone" }
        };
    }

    private void OnLoad()
    {
        ToastService.Information("Meet 示例", "会议室加载完成");
    }

    private async Task RunCommand()
    {
        if (_meet != null)
        {
            await _meet.ExecuteCommand("toggleChat");
        }
    }
}

