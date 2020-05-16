using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Timelines
    {
        private bool isRevers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>

        public void OnStateChanged(CheckboxState state, SelectedItem value)
        {
            if (value.Text == "正序")
            {
                isRevers = false;
                StateHasChanged();
            }
            else
            {
                isRevers = true;
                StateHasChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<SelectedItem> items { get; set; } = new SelectedItem[]
        {
            new SelectedItem("1","正序"){ Active=true },
            new SelectedItem("2","反序")
         };

        /// <summary>
        /// 
        /// </summary>
        private readonly IEnumerable<TimelineItem> timelineitems = new TimelineItem[]
        {
            new TimelineItem{  Content="创建时间",DateTime=DateTime.Now.ToString("yyyy-MM-dd")},
            new TimelineItem{  Content="通过审核",DateTime=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")},
            new TimelineItem{  Content="活动按期开始",DateTime=DateTime.Now.AddDays(2).ToString("yyyy-MM-dd")}
        };

        /// <summary>
        /// 
        /// </summary>
        private readonly IEnumerable<TimelineItem> timelineitemsColor = new TimelineItem[]
        {
            new TimelineItem{ Color=Color.Warning, Content="创建时间",DateTime=DateTime.Now.ToString("yyyy-MM-dd")},
            new TimelineItem{ Color=Color.Info, Content="通过审核",DateTime=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")},
            new TimelineItem{ Color=Color.Success, Content="活动按期开始",DateTime=DateTime.Now.AddDays(2).ToString("yyyy-MM-dd")}
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Reverse",
                Description = "是否倒序显示",
                Type = "boolean",
            },
            new AttributeItem() {
                Name = "Items",
                Description = "数据集合",
                Type = "IEnumerable<SelectedItem>",
                ValueList = "—",
                DefaultValue = " — "
            }
        };
    }
}
