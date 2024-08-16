// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
