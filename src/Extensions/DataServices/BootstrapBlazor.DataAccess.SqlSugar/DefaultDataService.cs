// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.DataAccess.SqlSugar;


/// <summary>
/// SqlSugar ORM 的 IDataService 接口实现
/// </summary>
class DefaultDataService<TModel>(ISqlSugarClient db) : DataServiceBase<TModel> where TModel : class, new()
{
    /// <summary>
    /// 删除方法
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync(IEnumerable<TModel> models)
    {
        // 通过模型获取主键列数据
        // 支持批量删除
        await db.Deleteable<TModel>(models).ExecuteCommandAsync();
        return true;
    }

    /// <summary>
    /// 保存方法
    /// </summary>
    /// <param name="model"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    public override async Task<bool> SaveAsync(TModel model, ItemChangedType changedType)
    {
        await db.Storageable(model).ExecuteCommandAsync();
        return true;
    }

    /// <summary>
    /// 查询方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
    {
        int count = 0;

        var filter = option.ToFilter();

        var items = db.Queryable<TModel>()
            .WhereIF(filter.HasFilters(), filter.GetFilterLambda<TModel>())
            .OrderByIF(option.SortOrder != SortOrder.Unset, $"{option.SortName} {option.SortOrder}")
            .ToPageList(option.PageIndex, option.PageItems, ref count);

        var ret = new QueryData<TModel>()
        {
            TotalCount = count,
            Items = items,
            IsSorted = option.SortOrder != SortOrder.Unset,
            IsFiltered = option.Filters.Count > 0,
            IsAdvanceSearch = option.AdvanceSearches.Count > 0,
            IsSearch = option.Searches.Count > 0 || option.CustomerSearches.Count > 0
        };
        return Task.FromResult(ret);
    }
}
