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
    /// Gets the text representation of the clipboard item.
    /// </summary>
    public string Text
    {
        get
        {
            if (Data is not null && !string.IsNullOrEmpty(MimeType) && MimeType.StartsWith("text"))
            {
                return System.Text.Encoding.UTF8.GetString(Data);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
