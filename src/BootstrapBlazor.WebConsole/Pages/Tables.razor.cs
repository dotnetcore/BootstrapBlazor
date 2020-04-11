using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Tables
    {
        private IEnumerable<BindItem> Items => GenerateItems();

        private IEnumerable<BindItem> GenerateItems()
        {
            var random = new Random();
            return Enumerable.Range(1, 10).Select(i => new BindItem()
            {
                Id = i.ToString(),
                Name = $"张三{i}",
                DateTime = DateTime.Now.AddDays(i - 1),
                Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
                Count = random.Next(1, 100),
                Complete = random.Next(1, 100) > 50
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BindItem
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("主键")]
        public string? Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("姓名")]
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("日期")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("地址")]
        public string? Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("数量")]
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("是/否")]
        public bool Complete { get; set; }
    }
}
