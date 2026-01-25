// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Represents an item on the clipboard with associated 数据 and meta数据.</para>
/// <para lang="en">Represents an item on the clipboard with associated data and metadata.</para>
/// </summary>
public class ClipboardItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 the Internet Media Type (MIME 类型) of the 内容 stored in the clipboard item.</para>
    /// <para lang="en">Gets or sets the Internet Media Type (MIME type) of the content stored in the clipboard item.</para>
    /// </summary>
    /// <remarks>
    /// The MIME type is used to specify the nature and format of the data stored in the clipboard.
    /// It can be used to determine how the data should be handled or presented.
    /// </remarks>
    public string? MimeType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the raw 数据 of the clipboard item.</para>
    /// <para lang="en">Gets or sets the raw data of the clipboard item.</para>
    /// </summary>
    /// <remarks>
    /// The data is stored as a byte array, allowing for any binary or text content
    /// to be represented. The interpretation of the data depends on the MIME type.
    /// </remarks>
    public byte[]? Data { get; set; }

    /// <summary>
    /// <para lang="zh">Retrieves a text representation of the clipboard item if the MIME 类型 indicates text 内容.</para>
    /// <para lang="en">Retrieves a text representation of the clipboard item if the MIME type indicates text content.</para>
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
