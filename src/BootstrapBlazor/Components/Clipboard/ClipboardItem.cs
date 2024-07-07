namespace BootstrapBlazor.Components;

/// <summary>
/// Represents an item on the clipboard with associated data and metadata.
/// </summary>
public class ClipboardItem
{
    /// <summary>
    /// Gets or sets the Internet Media Type (MIME type) of the content stored in the clipboard item.
    /// </summary>
    /// <remarks>
    /// The MIME type is used to specify the nature and format of the data stored in the clipboard.
    /// It can be used to determine how the data should be handled or presented.
    /// </remarks>
    public string? MimeType { get; set; }

    /// <summary>
    /// Gets or sets the raw data of the clipboard item.
    /// </summary>
    /// <remarks>
    /// The data is stored as a byte array, allowing for any binary or text content
    /// to be represented. The interpretation of the data depends on the MIME type.
    /// </remarks>
    public byte[]? Data { get; set; }

    /// <summary>
    /// Retrieves a text representation of the clipboard item if the MIME type indicates text content.
    /// </summary>
    /// <value>
    /// A string containing the text representation of the data if the MIME type starts with "text/",
    /// otherwise an empty string.
    /// </value>
    /// <remarks>
    /// This property checks the MIME type of the clipboard item and, if it's a text type,
    /// converts the byte array to a UTF-8 encoded string. If the data is not text or the MIME type
    /// is not specified, it returns an empty string.
    /// </remarks>
    public string Text
    {
        get
        {
            if (Data is not null && MimeType?.StartsWith("text/") == true)
            {
                return System.Text.Encoding.UTF8.GetString(Data);
            }
            return string.Empty;
        }
    }
}
