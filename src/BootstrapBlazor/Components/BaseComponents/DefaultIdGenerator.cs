using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 默认组件 ID 生成器
    /// </summary>
    internal class DefaultIdGenerator : IComponentIdGenerator
    {
        /// <summary>
        /// 生成组件 Id 字符串
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public string Generate(ComponentBase component) => $"bb_{component.GetHashCode()}";
    }
}
