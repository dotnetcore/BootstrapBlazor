// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Reflection;

namespace BootstrapBlazor.Components.EditorForm
{
    /// <summary>
    /// IEditorItem 内部实现类
    /// </summary>
    internal class InternalEditorItem<TModel> : IEditorItem
    {
        /// <summary>
        /// 获得/设置 绑定数据模型
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// 获得/设置 字段数据类型
        /// </summary>
        public Type PropertyType { get; }

        /// <summary>
        /// 获得/设置 绑定字段名称
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// 获得/设置 是否可以编辑 默认为 true 可编辑
        /// </summary>
        public bool Editable { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否只读
        /// </summary>
        public bool Readonly { get; set; }

        /// <summary>
        /// 获得/设置 表头显示文字
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        public RenderFragment<object>? EditTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyInfo"></param>
        public InternalEditorItem(TModel model, PropertyInfo propertyInfo)
        {
            Model = model;
            FieldName = propertyInfo.Name;
            PropertyType = propertyInfo.PropertyType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName() => Text ?? Model!.GetDisplayName(FieldName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFieldName() => FieldName;
    }
}
