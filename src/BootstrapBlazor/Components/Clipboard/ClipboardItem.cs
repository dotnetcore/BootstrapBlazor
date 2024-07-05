namespace BootstrapBlazor.Components;

/// <summary>
/// ClipboardItem
/// </summary>
public class ClipboardItem
{
    /// <summary>
    /// Gets or sets the MIME type of the clipboard item.
    /// </summary>
    public string? MimeType { get; set; }

    /// <summary>
    /// Gets or sets the data of the clipboard item.
    /// </summary>
    public byte[]? Data { get; set; }

    /// <summary>
    /// Gets the text of the clipboard item.
    /// </summary>
    /// <returns></returns>
    public string GetText() => Data is not null ? System.Text.Encoding.UTF8.GetString(Data) : string.Empty;
}
