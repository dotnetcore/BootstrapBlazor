// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 登录模型
/// </summary>
public class LoginModel
{
    /// <summary>
    /// 
    /// </summary>
    [DataType(DataType.Text)]
    [Display(Name = "账号")]
    [Required(ErrorMessage = "{0}不能为空")]
    public string? UserName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "密码")]
    [Required(ErrorMessage = "{0}不能为空")]
    public string? Password { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool RememberMe { get; set; }
}
