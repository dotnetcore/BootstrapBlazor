// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public DefaultDataService(DbContext db)
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
            _db.RemoveRange(models);
            return Task.FromResult(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Task<bool> SaveAsync(TModel model)
        {
            _db.SaveChanges();
            return Task.FromResult(true);
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
