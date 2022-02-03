// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages;

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
