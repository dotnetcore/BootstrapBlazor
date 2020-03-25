using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IRules 接口
    /// </summary>
    public interface IValidateRules
    {
        /// <summary>
        /// 获得 Rules 集合
        /// </summary>
        ICollection<IValidator> Rules { get; }

        /// <summary>
        /// 验证组件添加时回调此方法
        /// </summary>
        /// <param name="validator"></param>
        void OnRuleAdded(IValidator validator);
    }
}
