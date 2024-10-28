﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IDataService 接口
/// </summary>
public interface IDataService<TModel> where TModel : class
{
    /// <summary>
    /// 新建数据方法
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> AddAsync(TModel model);

    /// <summary>
    /// 保存数据方法
    /// </summary>
    /// <param name="model">保存实体类实例</param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    Task<bool> SaveAsync(TModel model, ItemChangedType changedType);

    /// <summary>
    /// 删除数据方法
    /// </summary>
    /// <param name="models">要删除的数据集合</param>
    /// <returns>成功返回真，失败返回假</returns>
    Task<bool> DeleteAsync(IEnumerable<TModel> models);

    /// <summary>
    /// 查询数据方法
    /// </summary>
    /// <param name="option">查询条件参数集合</param>
    /// <returns></returns>
    Task<QueryData<TModel>> QueryAsync(QueryPageOptions option);
}

/// <summary>
/// 内部默认数据注入服务实现类
/// </summary>
internal class NullDataService<TModel> : DataServiceBase<TModel> where TModel : class
{
    /// <summary>
    /// 查询操作方法
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions options) => Task.FromResult(new QueryData<TModel>()
    {
        Items = new List<TModel>(),
        TotalCount = 0
    });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    public override Task<bool> SaveAsync(TModel model, ItemChangedType changedType) => Task.FromResult(false);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    public override Task<bool> DeleteAsync(IEnumerable<TModel> models) => Task.FromResult(false);
}
