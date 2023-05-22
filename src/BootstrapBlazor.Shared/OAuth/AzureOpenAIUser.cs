// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Longbow.OAuth;

namespace BootstrapBlazor.Shared.OAuth;

/// <summary>
/// 
/// </summary>
public class AzureOpenAIUser : OAuthUser
{
    /// <summary>
    /// 
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Total { get; } = 100;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Valid() => Count < Total;

    /// <summary>
    /// 
    /// </summary>
    public int Left => Total - Count;
}
