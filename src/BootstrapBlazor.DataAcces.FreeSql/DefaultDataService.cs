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
using System.Threading.Tasks;

namespace BootstrapBlazor.DataAcces.FreeSql
{
    /// <summary>
    /// PetaPoco ORM 的 IDataService 接口实现
    /// </summary>
    internal class DefaultDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
    {
        private readonly IFreeSql _db;
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultDataService(IFreeSql db)
        {
            _db = db;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task<bool> DeleteAsync(IEnumerable<TModel> models)
        {
            // 通过模型获取主键列数据
            // 支持批量删除
            await _db.Delete<TModel>(models).ExecuteAffrowsAsync();
            return true;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<bool> SaveAsync(TModel model)
        {
            await _db.GetRepository<TModel>().InsertOrUpdateAsync(model);
            return true;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            //TODO: 是否能够通过参数判断是否进行分页
            var items = _db.Select<TModel>()
                .Count(out var count)
                .Page(option.PageIndex, option.PageItems)
                .ToList();
            var ret = new QueryData<TModel>()
            {
                TotalCount = (int)count,
                Items = items
            };
            return Task.FromResult(ret);
        }
    }
}
