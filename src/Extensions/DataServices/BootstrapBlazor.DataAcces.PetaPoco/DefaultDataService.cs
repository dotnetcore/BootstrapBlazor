// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using PetaPoco;
using PetaPoco.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.DataAcces.PetaPoco
{
    /// <summary>
    /// PetaPoco ORM 的 IDataService 接口实现
    /// </summary>
    internal class DefaultDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
    {
        private readonly IDatabase _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultDataService(IDatabase db)
        {
            _db = db;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override Task<bool> DeleteAsync(IEnumerable<TModel> models)
        {
            // 通过模型获取主键列数据
            // 支持批量删除
            _db.DeleteBatch(models);
            return Task.FromResult(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<bool> SaveAsync(TModel model)
        {
            await _db.SaveAsync(model);
            return true;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override async Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            var items = await _db.FetchAsync<TModel>(option.Filters.Concat(option.Searchs), option.SortName, option.SortOrder);
            var ret = new QueryData<TModel>()
            {
                TotalCount = items.Count,
                Items = items,
                IsSorted = true,
                IsFiltered = true,
                IsSearch = true
            };
            return ret;
        }
    }
}
