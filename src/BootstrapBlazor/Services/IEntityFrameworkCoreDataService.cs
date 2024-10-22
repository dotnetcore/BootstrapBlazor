// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IEntityFrameworkCoreDataService 接口
/// </summary>
public interface IEntityFrameworkCoreDataService
{
    /// <summary>
    /// 取消方法，由于编辑时使用的是克隆数据，常见取消用法不需要写任何代码，可用于保存数据下次编辑时恢复
    /// </summary>
    /// <returns></returns>
    Task CancelAsync();

    /// <summary>
    /// 编辑方法，可对未提供编辑 UI 的数据进行填充
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task EditAsync(object model);
}
