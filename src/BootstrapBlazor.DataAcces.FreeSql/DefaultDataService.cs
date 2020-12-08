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
        public override Task<bool> DeleteAsync(IEnumerable<TModel> models)
        {
            // 通过模型获取主键列数据
            // 支持批量删除
            _db.Delete<TModel>(models).ExecuteAffrows();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Task<bool> SaveAsync(TModel model)
        {

            // 插入或更新数据，此功能依赖数据库特性（低版本可能不支持），参考如下：<para></para>
            // MySql 5.6+: on duplicate key update<para></para>
            // PostgreSQL 9.4+: on conflict do update<para></para>
            // SqlServer 2008+: merge into<para></para>
            // Oracle 11+: merge into<para></para>
            // Sqlite: replace into<para></para>
            // Firebird: merge into<para></para>
            // 达梦: merge into<para></para>
            // 人大金仓：on conflict do update<para></para>
            // 神通：merge into<para></para>
            // MsAccess：不支持<para></para>
            // 注意区别：FreeSql.Repository 仓储也有 InsertOrUpdate 方法（不依赖数据库特性）

            //_db.InsertOrUpdate<TModel>();

            //兼容旧版sql保险的方式
            var temp = typeof(TModel);

            _db.GetRepository<TModel>().InsertOrUpdate(model);
            return Task.FromResult(true);
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            var items = _db.Select<TModel>()
                .Page(option.PageIndex, option.PageItems)
                .ToList();
            var ret = new QueryData<TModel>()
            {
                TotalCount = items.Count,
                Items = items
            };
            return Task.FromResult(ret);
        }
    }
}
