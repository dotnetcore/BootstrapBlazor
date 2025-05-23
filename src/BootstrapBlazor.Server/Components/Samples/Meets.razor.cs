using Microsoft.AspNetCore.Components;
using Console = System.Console;

namespace BootstrapBlazor.Server.Components.Samples;

public partial class Meets : ComponentBase
{
    private MeetOption? Option { get; set; }

    private Meet? Meet { get; set; }

    private string Domain { get; set; } = "meet.jit.si";

    private string? RoomName { get; set; } = "BootstrapBlazor";

    private bool IsDisable { get; set; } = true;

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Option = new MeetOption();
        Option.RoomName = RoomName;
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

