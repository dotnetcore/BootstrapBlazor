// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
