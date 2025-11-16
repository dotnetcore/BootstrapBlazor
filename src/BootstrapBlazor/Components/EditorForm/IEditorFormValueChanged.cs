// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="IEditorFormValueChanged"/> 接口
/// </summary>
public interface IEditorFormValueChanged
{
    /// <summary>
    /// 触发值变化通知方法
    /// </summary>
    /// <returns></returns>
    Task NotifyValueChanged();
}
