namespace BootstrapBlazor.Shared.Samples;

public partial class Dispatches
{
    private async Task OnDispatch()
    {
        DispatchService.Dispatch(new DispatchEntry<MessageItem>() { Name = nameof(MessageItem), Entry = new MessageItem() { Message = $"{DateTime.Now:HH:mm:ss} {Localizer["DispatchNoticeMessage"]}" } });
        await Task.Delay(30 * 1000);
    }
}
