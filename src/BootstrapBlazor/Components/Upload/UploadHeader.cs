// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Upload 组件 Http 请求头类
    /// </summary>
    public class UploadHeader
    {
        /// <summary>
        /// 获得 请求头名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 获得 请求头值
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public UploadHeader(string name, string value) => (Name, Value) = (name, value);
    }
}
