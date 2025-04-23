// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Configuration class for the Dialog component
/// </summary>
public class DialogOption
{
    /// <summary>
    /// Gets or sets the related modal instance
    /// </summary>
    internal Modal? Modal { get; set; }

    /// <summary>
    /// Gets or sets the dialog title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the custom style of the dialog
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// Gets or sets the size of the dialog
    /// </summary>
    public Size Size { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    /// Gets or sets the full screen size of the dialog, default is None
    /// </summary>
    /// <remarks>To ensure functionality, when this value is set, <see cref="ShowMaximizeButton"/>, <seealso cref="ShowResize"/>, and <seealso cref="IsDraggable"/> are not available</remarks>
    public FullScreenSize FullScreenSize { get; set; } = FullScreenSize.None;

    /// <summary>
    /// Gets or sets whether to show the maximize button, default is false
    /// </summary>
    /// <remarks>To ensure functionality, when this value is set to true, <seealso cref="ShowResize"/> and <seealso cref="IsDraggable"/> are not available</remarks>
    public bool ShowMaximizeButton { get; set; }

    /// <summary>
    /// Gets or sets whether the dialog is vertically centered, default is true
    /// </summary>
    public bool IsCentered { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the dialog content scrolls when it is too long, default is false
    /// </summary>
    public bool IsScrolling { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to show the resize button, default is false
    /// </summary>
    public bool ShowResize { get; set; }

    /// <summary>
    /// Gets or sets whether to show the close button, default is true
    /// </summary>
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to show the header close button, default is true
    /// </summary>
    public bool ShowHeaderCloseButton { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to enable fade animation, default is null
    /// </summary>
    public bool? IsFade { get; set; }

    /// <summary>
    /// Gets or sets whether to support closing the dialog with the ESC key, default is true
    /// </summary>
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to support closing the dialog by clicking the backdrop, default is false
    /// </summary>
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// Gets or sets whether to show the footer, default is true
    /// </summary>
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to show the print button, default is false
    /// </summary>
    public bool ShowPrintButton { get; set; }

    /// <summary>
    /// Gets or sets whether to show the save button, default is false
    /// </summary>
    public bool ShowSaveButton { get; set; }

    /// <summary>
    /// Gets or sets whether to show the print button in the header, default is false
    /// </summary>
    public bool ShowPrintButtonInHeader { get; set; }

    /// <summary>
    /// Gets or sets the text of the print button in the header, default is "Print" from the resource file
    /// </summary>
    public string? PrintButtonText { get; set; }

    /// <summary>
    /// Gets or sets the related data, mostly used for passing values
    /// </summary>
    public object? BodyContext { get; set; }

    /// <summary>
    /// Gets or sets the ModalBody component
    /// </summary>
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the ModalFooter component
    /// </summary>
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// Gets or sets the ModalHeader component template
    /// </summary>
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// Gets or sets the custom buttons in the ModalHeader component
    /// </summary>
    public RenderFragment? HeaderToolbarTemplate { get; set; }

    /// <summary>
    /// Gets or sets the custom component
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }

    /// <summary>
    /// Gets or sets the icon of the save button, default is null and uses the current theme icon
    /// </summary>
    public string? SaveButtonIcon { get; set; }

    /// <summary>
    /// Gets or sets the text of the save button
    /// </summary>
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// Gets or sets the callback method for the save button
    /// </summary>
    public Func<Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// Gets or sets the icon of the close button, default is null and uses the current theme icon
    /// </summary>
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// Gets or sets the text of the close button
    /// </summary>
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// Gets or sets the callback method for closing the dialog
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically close the dialog after saving successfully, default is true
    /// </summary>
    public bool IsAutoCloseAfterSave { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the dialog can be dragged, default is false
    /// </summary>
    public bool IsDraggable { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the dialog is shown
    /// </summary>
    public Func<Task>? OnShownAsync { get; set; }

    /// <summary>
    /// Gets or sets whether to show the export PDF button, default is false
    /// </summary>
    public bool ShowExportPdfButton { get; set; }

    /// <summary>
    /// Gets or sets whether to show the export PDF button in the header, default is false
    /// </summary>
    public bool ShowExportPdfButtonInHeader { get; set; }

    /// <summary>
    /// Gets or sets the configuration options for the export PDF button
    /// </summary>
    public ExportPdfButtonOptions? ExportPdfButtonOptions { get; set; }

    /// <summary>
    /// Gets or sets whether to hide the previous dialog when opening a new one, default is false
    /// </summary>
    public bool IsHidePreviousDialog { get; set; }

    /// <summary>
    /// Method to close the dialog
    /// </summary>
    public async Task CloseDialogAsync()
    {
        if (Modal != null)
        {
            await Modal.Close();
        }
    }

    /// <summary>
    /// Method to convert parameters to component attributes
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, object> ToAttributes()
    {
        var ret = new Dictionary<string, object>
        {
            [nameof(ModalDialog.Size)] = Size,
            [nameof(ModalDialog.FullScreenSize)] = FullScreenSize,
            [nameof(ModalDialog.IsCentered)] = IsCentered,
            [nameof(ModalDialog.IsScrolling)] = IsScrolling,
            [nameof(ModalDialog.IsHidePreviousDialog)] = IsHidePreviousDialog,
            [nameof(ModalDialog.ShowCloseButton)] = ShowCloseButton,
            [nameof(ModalDialog.ShowSaveButton)] = ShowSaveButton,
            [nameof(ModalDialog.ShowHeaderCloseButton)] = ShowHeaderCloseButton,
            [nameof(ModalDialog.ShowFooter)] = ShowFooter,
            [nameof(ModalDialog.ShowResize)] = ShowResize,
            [nameof(ModalDialog.ShowPrintButton)] = ShowPrintButton,
            [nameof(ModalDialog.ShowPrintButtonInHeader)] = ShowPrintButtonInHeader,
            [nameof(ModalDialog.IsAutoCloseAfterSave)] = IsAutoCloseAfterSave,
            [nameof(ModalDialog.IsDraggable)] = IsDraggable,
            [nameof(ModalDialog.ShowMaximizeButton)] = ShowMaximizeButton,
            [nameof(ModalDialog.ShowExportPdfButton)] = ShowExportPdfButton,
            [nameof(ModalDialog.ShowExportPdfButtonInHeader)] = ShowExportPdfButtonInHeader,
        };
        if (ExportPdfButtonOptions != null)
        {
            ret.Add(nameof(ModalDialog.ExportPdfButtonOptions), ExportPdfButtonOptions);
        }
        if (!string.IsNullOrEmpty(PrintButtonText))
        {
            ret.Add(nameof(ModalDialog.PrintButtonText), PrintButtonText);
        }
        if (!string.IsNullOrEmpty(Title))
        {
            ret.Add(nameof(ModalDialog.Title), Title);
        }
        if (BodyContext != null)
        {
            ret.Add(nameof(ModalDialog.BodyContext), BodyContext);
        }
        return ret;
    }
}
