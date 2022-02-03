// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IDataServie 实现类基类
/// </summary>
public abstract class DataServiceBase<TModel> : IDataService<TModel> where TModel : class, new()
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
