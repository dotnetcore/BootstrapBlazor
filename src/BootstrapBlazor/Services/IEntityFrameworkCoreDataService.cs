// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IEntityFrameworkCoreDataService 接口</para>
/// <para lang="en">IEntityFrameworkCoreDataService Interface</para>
/// </summary>
public interface IEntityFrameworkCoreDataService
{
    /// <summary>
    /// <para lang="zh">取消方法，由于编辑时使用的是克隆数据，常见取消用法不需要写任何代码，可用于保存数据下次编辑时恢复</para>
    /// <para lang="en">Cancel method. Since cloned data is used during editing, no code is required for common cancellation usage. It can be used to restore data for the next edit save</para>
    /// </summary>
    Task CancelAsync();

    /// <summary>
    /// <para lang="zh">编辑方法，可对未提供编辑 UI 的数据进行填充</para>
    /// <para lang="en">Edit method, can fill data not provided by the edit UI</para>
    /// </summary>
    /// <param name="model"></param>
    Task EditAsync(object model);
}
