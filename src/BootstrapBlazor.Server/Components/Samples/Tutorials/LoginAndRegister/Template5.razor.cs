// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials.LoginAndRegister;

/// <summary>
/// 高仿 Microsoft 登录界面
/// </summary>
public partial class Template5
{
    private bool isAuth = false;
    private bool showEmailError = false;
    private bool isEmailEntered = false;

    private readonly LoginModel _loginModel = new()
    {
        Email = "a@blazor.zone",
        Password = "123456"
    };

    private async Task OnEmailSubmit(EditContext context)
    {
        if (string.IsNullOrEmpty(_loginModel.Email))
        {
            showEmailError = true;
            isEmailEntered = false;
        }
        else
        {
            showEmailError = false;
            isEmailEntered = true;
            await InvokeVoidAsync("go", Id);
        }
        StateHasChanged();
    }

    private Task OnPasswordSubmit(EditContext context)
    {
        // 数据库检查密码逻辑可以在这里实现
        // 演示代码一律通过
        isAuth = true;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task GoBack()
    {
        isEmailEntered = false;
        showEmailError = false;
        StateHasChanged();
        await InvokeVoidAsync("back", Id);
    }

    private void OnReset()
    {
        isAuth = false;
        isEmailEntered = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }

    private class LoginModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
