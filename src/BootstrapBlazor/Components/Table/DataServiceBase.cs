// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IDataServie 实现类基类
    /// </summary>
    public abstract class DataServiceBase<TModel> : IDataService<TModel> where TModel : class, new()
    {
        /// <summary>
        /// 新建数据操作方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual Task<bool> AddAsync(TModel model) => Task.FromResult(true);

        /// <summary>
        /// 删除数据操作方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(IEnumerable<TModel> models) => Task.FromResult(true);

        /// <summary>
        /// 保存数据操作方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual Task<bool> SaveAsync(TModel model) => Task.FromResult(true);

        /// <summary>
        /// 查询数据操作方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public abstract Task<QueryData<TModel>> QueryAsync(QueryPageOptions option);
    }
}
