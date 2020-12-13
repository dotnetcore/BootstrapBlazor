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

namespace BootstrapBlazor.DataAcces.EntityFramework
{
    /// <summary>
    /// Entity Framework ORM 的 IDataService 接口实现
    /// </summary>
    internal class DefaultDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
    {
        private readonly DbContext _db;
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultDataService(Func<DbContext> dbContextResolve)
        {
            _db = dbContextResolve();
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
            // 由于 Table 组件内部编辑时是 Clone 一份 EditContext 用于取消时不破坏原有数据，所以这里 model 并不在 _db.Set 中
            // 需要判断主键是否是插入还是更新操作，然后调用 SaveChanges 方法
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
            var items = query.Skip((option.PageIndex - 1) * option.PageItems).Take(option.PageItems).ToList();
            var ret = new QueryData<TModel>()
            {
                TotalCount = items.Count,
                Items = items
            };
            return Task.FromResult(ret);
        }
    }
}
