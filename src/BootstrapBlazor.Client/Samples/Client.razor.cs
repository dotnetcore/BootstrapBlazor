namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Client 组件示例
/// </summary>
public partial class Client
{
    [Inject]
    [NotNull]
    private WebClientService? ClientService { get; set; }
    private ClientInfo ClientInfo { get; set; } = new ClientInfo();

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
            ClientInfo = await ClientService.GetClientInfo();
            StateHasChanged();
        }
    }
}
