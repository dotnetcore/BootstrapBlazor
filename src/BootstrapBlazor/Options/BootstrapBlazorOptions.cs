// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Delay 配置类
    /// </summary>
    public class BootstrapBlazorOptions
    {
        /// <summary>
        /// 获得/设置 Toast 组件 Delay 默认值 默认为 0
        /// </summary>
        public int ToastDelay { get; set; }

        /// <summary>
        /// 获得/设置 Message 组件 Delay 默认值 默认为 0
        /// </summary>
        public int MessageDelay { get; set; }

        /// <summary>
        /// 获得/设置 Swal 组件 Delay 默认值 默认为 0
        /// </summary>
        public int SwalDelay { get; set; }

        /// <summary>
        /// 获得/设置 回落默认语言文化 默认为 en 英文
        /// </summary>
        public string FallbackCultureName { get; set; } = "en";

        /// <summary>
        /// 获得 组件内置本地化语言列表
        /// </summary>
        public List<string> SupportedCultures { get; set; } = new List<string>() { "zh", "en" };

        /// <summary>
        /// 获得支持多语言集合
        /// </summary>
        /// <returns></returns>
        public IList<CultureInfo> GetSupportedCultures()
        {
            // 循环过滤掉上级文化
            var ret = new List<CultureInfo>();
            foreach (var name in SupportedCultures)
            {
                var culture = new CultureInfo(name);
                if (!ret.Any(c => c.Name == culture.Name))
                {
                    ret.Add(culture);
                }

                while (culture != culture.Parent)
                {
                    culture = culture.Parent;
                    var p = ret.FirstOrDefault(c => c.Name == culture.Name);
                    if (p != null)
                    {
                        ret.Remove(p);
                    }
                }
            }
            return ret;
        }
    }
}
