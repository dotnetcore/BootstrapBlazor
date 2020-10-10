using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 组件 ID 生成器接口
    /// </summary>
    public interface IComponentIdGenerator
    {
        /// <summary>
        /// 生成组件 Id 方法
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        string Generate(ComponentBase component);
    }
}
