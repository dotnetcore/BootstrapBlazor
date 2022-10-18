// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Toasts
/// </summary>
public sealed partial class Toasts
{
    [NotNull]
    private Toast? Toast { get; set; }

    [NotNull]
    private ToastOption? Options1 { get; set; }

    [NotNull]
    private ToastOption? Options2 { get; set; }

    [NotNull]
    private ToastOption? Options3 { get; set; }

    [NotNull]
    private ToastOption? Options4 { get; set; }

    [CascadingParameter]
    [NotNull]
    private BootstrapBlazorRoot? Root { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Options1 = new ToastOption { Title = "save data", IsAutoHide = false, Content = "Save data successfully, automatically close after 4 seconds" };
        Options2 = new ToastOption { Category = ToastCategory.Error, Title = "save data", IsAutoHide = false, Content = "Save data successfully, automatically close after 4 seconds" };
        Options3 = new ToastOption { Category = ToastCategory.Information, Title = "prompt information", IsAutoHide = false, Content = "Information prompt pop-up window, automatically closes after 4 seconds" };
        Options4 = new ToastOption { Category = ToastCategory.Warning, Title = "Warning message", IsAutoHide = false, Content = "Information prompt pop-up window, automatically closes after 4 seconds" };

        Toast = Root.ToastContainer;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("$._showToast");
        }
    }

    private async Task OnPlacementClick(Placement placement)
    {
        Toast.SetPlacement(placement);
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "notification",
            Content = "<b>Toast</b> The component has changed position, it will automatically shut down after 4 seconds"
        });
    }

    private async Task OnSuccessClick()
    {
        Toast.SetPlacement(Placement.BottomEnd);
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Success,
            Title = "Successfully saved",
            Content = "Save data successfully, automatically close after 4 seconds"
        });
    }

    private async Task OnErrorClick()
    {
        Toast.SetPlacement(Placement.BottomEnd);
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Error,
            Title = "Failed to save",
            Content = "Failed to save data, automatically closes after 4 seconds"
        });
    }

    private async Task OnInfoClick()
    {
        Toast.SetPlacement(Placement.BottomEnd);
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "notification",
            Content = "The system adds new components, it will automatically shut down after 4 seconds"
        });
    }

    private async Task OnWarningClick()
    {
        Toast.SetPlacement(Placement.BottomEnd);
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Warning,
            Title = "Warning",
            Content = "If the system finds abnormality, please deal with it in time, and it will automatically shut down after 4 seconds"
        });
    }

    /// <summary>
    /// 
    /// </summary>
    private async Task OnNotAutoHideClick()
    {
        Toast.SetPlacement(Placement.BottomEnd);
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Warning,
            IsAutoHide = false,
            Title = "Notification",
            Content = "I will not close automatically, please click the close button in the upper right corner"
        });
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Category",
            Description = "Popover type",
            Type = "ToastCategory",
            ValueList = "Success/Information/Error/Warning",
            DefaultValue = "Success"
        },
        new AttributeItem() {
            Name = "Cotent",
            Description = "Popup content",
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Delay",
            Description = "Auto hide interval",
            Type = "int",
            ValueList = "—",
            DefaultValue = "4000"
        },
        new AttributeItem() {
            Name = "IsAutoHide",
            Description = "Whether to automatically hide",
            Type = "boolean",
            ValueList = "",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsHtml",
            Description = "Whether the content contains Html code",
            Type = "boolean",
            ValueList = "",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Placement",
            Description = "Location",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "Popup title",
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        }
    };
}
