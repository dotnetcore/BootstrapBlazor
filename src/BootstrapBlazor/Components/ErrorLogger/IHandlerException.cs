// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
