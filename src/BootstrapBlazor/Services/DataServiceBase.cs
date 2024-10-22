// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IDataServie 实现类基类
/// </summary>
public abstract class DataServiceBase<TModel> : IDataService<TModel> where TModel : class
{
    /// <summary>
    /// 新建数据操作方法
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public virtual Task<bool> AddAsync(TModel model) => Task.FromResult(true);

    /// <summary>
    /// 删除数据操作方法
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    public virtual Task<bool> DeleteAsync(IEnumerable<TModel> models) => Task.FromResult(true);

    /// <summary>
    /// 保存数据操作方法
    /// </summary>
    /// <param name="model"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    public virtual Task<bool> SaveAsync(TModel model, ItemChangedType changedType) => Task.FromResult(true);

    /// <summary>
    /// 查询数据操作方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public abstract Task<QueryData<TModel>> QueryAsync(QueryPageOptions option);
}
