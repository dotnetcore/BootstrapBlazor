// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Ajax示例类
/// </summary>
public partial class Ajaxs
{
    private string? ResultMessage { get; set; }

    [Inject]
    [NotNull]
    private AjaxService? AjaxService { get; set; }

    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    private async Task Success()
    {
        var option = new AjaxOption
        {
            Url = "/api/Login",
            Data = new User() { UserName = "admin", Password = "123456" }
        };
        var result = await AjaxService.GetMessage(option);
        if (result == null)
        {
            ResultMessage = "响应失败";
        }
        else
        {
            ResultMessage = result;
            var doc = JsonDocument.Parse(result);
            if (200 == doc.RootElement.GetProperty("code").GetInt32())
            {
                await SwalService.Show(new SwalOption() { Content = "登录成功！", Category = SwalCategory.Success });
            }
            else
            {
                await SwalService.Show(new SwalOption() { Content = $"登录失败:{doc.RootElement.GetProperty("message").GetString()}", Category = SwalCategory.Error });
            }
        }
    }

    private async Task Fail()
    {
        var option = new AjaxOption
        {
            Url = "/api/Login",
            Data = new User() { UserName = "admin", Password = "1234567" }
        };
        var result = await AjaxService.GetMessage(option);
        if (result == null)
        {
            ResultMessage = "响应失败";
        }
        else
        {
            ResultMessage = result;
            var doc = JsonDocument.Parse(result);
            if (200 == doc.RootElement.GetProperty("code").GetInt32())
            {
                await SwalService.Show(new SwalOption() { Content = "登录成功！", Category = SwalCategory.Success });
            }
            else
            {
                await SwalService.Show(new SwalOption() { Content = $"登录失败:{doc.RootElement.GetProperty("message").GetString()}", Category = SwalCategory.Error });
            }
        }
    }

    private async Task Goto()
    {
        await AjaxService.Goto("/introduction");
    }

    private async Task GotoSelf()
    {
        await AjaxService.Goto("/ajaxs");
    }
}
