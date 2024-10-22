// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IHandlerException
/// </summary>
public interface IHandlerException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorContent"></param>
    Task HandlerException(Exception ex, RenderFragment<Exception> errorContent);
}
