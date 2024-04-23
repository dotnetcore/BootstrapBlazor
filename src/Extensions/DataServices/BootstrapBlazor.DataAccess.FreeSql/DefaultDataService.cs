// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.DataAccess.FreeSql;

/// <summary>
/// FreeSql ORM 的 IDataService 接口实现
/// </summary>
class DefaultDataService<TModel>(IFreeSql db) : DataServiceBase<TModel> where TModel : class, new()
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
        await db.Delete<TModel>(models).ExecuteAffrowsAsync();
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
        await db.GetRepository<TModel>().InsertOrUpdateAsync(model);
        return true;
    }

    /// <summary>
    /// 查询方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
    {
        var items = db.Select<TModel>().WhereDynamicFilter(option.ToDynamicFilter())
            .OrderByPropertyNameIf(option.SortOrder != SortOrder.Unset, option.SortName, option.SortOrder == SortOrder.Asc)
            .Count(out var count);

        if (option.IsPage)
        {
            items = items.Page(option.PageIndex, option.PageItems);
        }
        else if (option.IsVirtualScroll)
        {
            items = items.Skip(option.StartIndex).Take(option.PageItems);
        }

        var ret = new QueryData<TModel>()
        {
            TotalCount = (int)count,
            Items = items.ToList<TModel>(),
            IsSorted = option.SortOrder != SortOrder.Unset,
            IsFiltered = option.Filters.Count > 0,
            IsAdvanceSearch = option.AdvanceSearches.Count > 0,
            IsSearch = option.Searches.Count > 0 || option.CustomerSearches.Count > 0
        };
        return Task.FromResult(ret);
    }
}
