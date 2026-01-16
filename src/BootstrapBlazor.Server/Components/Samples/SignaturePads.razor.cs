// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 签名演示
/// </summary>
public partial class SignaturePads
{
    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result2 { get; set; }

    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result3 { get; set; }

    private Task OnResult(string result)
    {
        Result = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnResult2(string result)
    {
        Result2 = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnResult3(string result)
    {
        Result3 = result;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
