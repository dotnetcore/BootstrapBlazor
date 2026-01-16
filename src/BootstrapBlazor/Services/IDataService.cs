// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IDataService 接口</para>
/// <para lang="en">IDataService Interface</para>
/// </summary>
public interface IDataService<TModel> where TModel : class
{
    /// <summary>
    /// <para lang="zh">新建数据方法</para>
    /// <para lang="en">Add Data Method</para>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> AddAsync(TModel model);

    /// <summary>
    /// <para lang="zh">保存数据方法</para>
    /// <para lang="en">Save Data Method</para>
    /// </summary>
    /// <param name="model">保存实体类实例</param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    Task<bool> SaveAsync(TModel model, ItemChangedType changedType);

    /// <summary>
    /// <para lang="zh">删除数据方法</para>
    /// <para lang="en">Delete Data Method</para>
    /// </summary>
    /// <param name="models">要删除的数据集合</param>
    /// <returns>成功返回真，失败返回假</returns>
    Task<bool> DeleteAsync(IEnumerable<TModel> models);

    /// <summary>
    /// <para lang="zh">查询数据方法</para>
    /// <para lang="en">Query Data Method</para>
    /// </summary>
    /// <param name="option">查询条件参数集合</param>
    /// <returns></returns>
    Task<QueryData<TModel>> QueryAsync(QueryPageOptions option);
}

/// <summary>
/// <para lang="zh">内部默认数据注入服务实现类</para>
/// <para lang="en">Internal Default Data Service Implementation</para>
/// </summary>
internal class NullDataService<TModel> : DataServiceBase<TModel> where TModel : class
{
    /// <summary>
    /// <para lang="zh">查询操作方法</para>
    /// <para lang="en">Query Method</para>
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions options) => Task.FromResult(new QueryData<TModel>()
    {
        Items = new List<TModel>(),
        TotalCount = 0
    });

    /// <summary>
    /// <para lang="zh">///</para>
    /// <para lang="en">///</para>
    /// </summary>
    /// <param name="model"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    public override Task<bool> SaveAsync(TModel model, ItemChangedType changedType) => Task.FromResult(false);

    /// <summary>
    /// <para lang="zh">///</para>
    /// <para lang="en">///</para>
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    public override Task<bool> DeleteAsync(IEnumerable<TModel> models) => Task.FromResult(false);
}
