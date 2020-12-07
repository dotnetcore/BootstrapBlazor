// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using PP = PetaPoco;

namespace BootstrapBlazor.DataAcces.PetaPoco
{
    /// <summary>
    /// 
    /// </summary>
    internal class DefaultDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public DefaultDataService(IConfiguration config)
        {
            // 获取连接字符串

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Task<bool> SaveAsync(TModel model)
        {
            return base.SaveAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            return Task.FromResult(new QueryData<TModel>()
            {
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true,
                TotalCount = 2,
                Items = new TModel[]
                {
                    new TModel(),
                    new TModel()
                }
            });
        }
    }
}
