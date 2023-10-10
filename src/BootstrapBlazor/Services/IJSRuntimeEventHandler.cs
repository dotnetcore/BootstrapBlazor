// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Runtime.InteropServices;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器事件,通用属性帮助类接口
/// <para>该接口已弃用，请使用新接口：<see cref="IBootstrapBlazorJSHelper"/></para>
/// </summary>
[Obsolete("The IJSRuntimeEventHandler class is obsolete and should no longer be used. Please use the IBootstrapBlazorJSHelper class instead. For more information, please refer to the migration guide.", false)]
[ComVisible(true)]
public interface IJSRuntimeEventHandler : IBootstrapBlazorJSHelper, IAsyncDisposable
{
}
