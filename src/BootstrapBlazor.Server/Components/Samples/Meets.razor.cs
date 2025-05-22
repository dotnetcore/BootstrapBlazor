using Microsoft.AspNetCore.Components;
using Console = System.Console;

namespace BootstrapBlazor.Server.Components.Samples;

public partial class Meets : ComponentBase
{
    private MeetOption? Option { get; set; }

    private Meet? Meet { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Option = new MeetOption();
        Option.RoomName = "BootstrapBlazor";
        Option.Width = "100%";
        Option.Height = 700;
        Option.ConfigOverwrite = new
        {
            Lobby = new {EnableChat = false},
            HiddenPremeetingButtons = new string[]{"invite"},
            DisableInviteFunctions = true,
            ButtonsWithNotifyClick = new []{new {key = "invite", preventExecution = true}}
        };
        Option.UserInfo = new UserInfo() { DisplayName = "BootstrapBlazor", Email = "bb@blazor.zone" };

    }

    private void OnLoad()
    {
        Console.WriteLine("会议室加载完成");
    }

    private void RunCommand()
    {
        Meet?.ExecuteCommand("toggleChat");
    }
}

