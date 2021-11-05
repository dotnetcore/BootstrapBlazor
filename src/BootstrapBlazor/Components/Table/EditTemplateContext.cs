// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class EditTemplateContext<TModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="changedType"></param>
        public EditTemplateContext(TModel model, ItemChangedType changedType)
        {
            Model = model;
            ChangedType = changedType;
        }

        /// <summary>
        /// 获得/设置 行数据实例
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// 获得/设置 改变类型
        /// </summary>
        public ItemChangedType ChangedType { get; set; }
    }
}
