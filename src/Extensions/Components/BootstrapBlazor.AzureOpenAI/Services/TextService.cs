namespace BootstrapBlazor.Components;

public class TextService
{
    private List<string> Names { get; set; } = new List<string>()
    {
        "Name","Address"
    };

    public async Task<bool> IsShowField(string fileName)
    {
        await Task.Delay(200);

        return Names.Contains(fileName, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<List<string>> GetList()
    {
        return await Task.FromResult(Names);
    }

}
