// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.DataAcces.EntityFrameworkCore
{
    /// <summary>
    /// Entity Framework ORM 的 IDataService 接口实现
    /// </summary>
    internal class DefaultDataService<TModel> : DataServiceBase<TModel>, IEntityFrameworkCoreDataService where TModel : class, new()
    {
        private readonly DbContext _db;
        private TModel? Model { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultDataService(Func<IEntityFrameworkCoreDataService, DbContext> dbContextResolve)
        {
            _db = dbContextResolve(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Task<bool> AddAsync(TModel model)
        {
            Model = model;
            return base.AddAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task CancelAsync()
        {
            if (Model != null)
            {
                if (_db.Entry(Model).IsKeySet) _db.Entry(Model).State = EntityState.Unchanged;
                else _db.Entry(Model).State = EntityState.Detached;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task EditAsync(object model)
        {
            Model = model as TModel;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task<bool> DeleteAsync(IEnumerable<TModel> models)
        {
            _db.RemoveRange(models);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<bool> SaveAsync(TModel model)
        {
            if (_db.Entry(model).IsKeySet) _db.Update(model);
            else await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            var query = _db.Set<TModel>().AsQueryable();

            // TODO: 未做搜索处理
            query = query.Where(option.Filters.GetFilterLambda<TModel>());

            // TODO: 未做排序处理
            var items = query.Skip((option.PageIndex - 1) * option.PageItems).Take(option.PageItems);
            var ret = new QueryData<TModel>()
            {
                TotalCount = query.Count(),
                Items = items
            };
            return Task.FromResult(ret);
        }
    }
}
