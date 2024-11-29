﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Pages;

/// <summary>
/// 
/// </summary>
public partial class Layout
{
    [Inject]
    [NotNull]
    private DialogService? Dialog { get; set; }

    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    [Inject]
    [NotNull]
    private SwalService? Swal { get; set; }

    [Inject]
    [NotNull]
    private MessageService? Message { get; set; }

    private async Task ShowDialog()
    {
        await Dialog.Show(new DialogOption()
        {
            Title = "测试弹窗",
            BodyTemplate = builder =>
            {
                builder.AddContent(0, BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
                {
                    [nameof(Button.Text)] = "Toast",
                    [nameof(Button.OnClickWithoutRender)] = async () =>
                    {
                        await Toast.Show(new ToastOption()
                        {
                            Title = "Toast",
                            Content = "Dialog 中弹窗 Toast 测试成功",
                            IsAutoHide = false
                        });
                    }
                }).Render());
                builder.AddContent(0, BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
                {
                    ["class"] = "ms-3",
                    [nameof(Button.Text)] = "Message",
                    [nameof(Button.OnClickWithoutRender)] = async () =>
                    {
                        await Message.Show(new MessageOption()
                        {
                            Content = "Dialog 中弹窗 Message 测试成功"
                        });
                    }
                }).Render());
                builder.AddContent(0, BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
                {
                    ["class"] = "ms-3",
                    [nameof(Button.Text)] = "Swal",
                    [nameof(Button.OnClickWithoutRender)] = async () =>
                    {
                        await Swal.Show(new SwalOption()
                        {
                            Category = SwalCategory.Success,
                            Title = "Sweet Alert",
                            Content = "Dialog 中弹窗 Swal 测试成功"
                        });
                    }
                }).Render());
            }
        });
    }
}
