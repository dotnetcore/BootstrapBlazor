namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Client 组件示例
/// </summary>
public partial class Client
{
    [Inject]
    [NotNull]
    private WebClientService? ClientService { get; set; }

    private ClientInfo _clientInfo = new();

    /// <summary>
    /// OnAfterRenderAsync
    /// </summary>
    /// <param name = "firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _clientInfo = await ClientService.GetClientInfo();
            StateHasChanged();
        }
    }
}
