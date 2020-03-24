using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IRules 接口
    /// </summary>
    public interface IRules
    {
        /// <summary>
        /// 获得 Rules 集合
        /// </summary>
        ICollection<ValidatorComponentBase> Rules { get; }
    }
}
