// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared;

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
