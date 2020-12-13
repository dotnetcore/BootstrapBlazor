// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TablesDetailRow
    {
        private IEnumerable<DetailRow> GetDetailRowsByName(string name) => Enumerable.Range(1, 4).Select(i => new DetailRow()
        {
            Id = i,
            Name = name,
            DateTime = DateTime.Now.AddDays(i - 1),
            Complete = random.Next(1, 100) > 50
        });

        private class DetailRow
        {
            /// <summary>
            /// 
            /// </summary>
            [DisplayName("主键")]
            public int Id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [DisplayName("培训课程")]
            public string Name { get; set; } = "";

            /// <summary>
            /// 
            /// </summary>
            [DisplayName("日期")]
            public DateTime DateTime { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [DisplayName("是/否")]
            public bool Complete { get; set; }
        }

        private bool ShowDetailRow(BindItem item)
        {
            return item.Complete;
        }
    }
}
