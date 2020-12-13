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
    /// IDataService 接口
    /// </summary>
    public interface IDataService<TModel> where TModel : class, new()
    {
        /// <summary>
        /// 新建数据方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddAsync(TModel model);

        /// <summary>
        /// 保存数据方法
        /// </summary>
        /// <param name="model">保存实体类实例</param>
        /// <returns></returns>
        Task<bool> SaveAsync(TModel model);

        /// <summary>
        /// 删除数据方法
        /// </summary>
        /// <param name="models">要删除的数据集合</param>
        /// <returns>成功返回真，失败返回假</returns>
        Task<bool> DeleteAsync(IEnumerable<TModel> models);

        /// <summary>
        /// 查询数据方法
        /// </summary>
        /// <param name="option">查询条件参数集合</param>
        /// <returns></returns>
        Task<QueryData<TModel>> QueryAsync(QueryPageOptions option);
    }
}
