using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class SweetAlerts
    {
        private Task OnSwal(SwalCategory cate)
        {
            SwalService.Show(new SwalOption()
            {
                Category = cate,
                Title = "Sweet 弹窗"
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Category",
                    Description = "弹出框类型",
                    Type = "SwalCategory",
                    ValueList = "Success/Information/Error/Warning",
                    DefaultValue = "Success"
                },
                new AttributeItem() {
                    Name = "Title",
                    Description = "弹窗标题",
                    Type = "string",
                    ValueList = "—",
                    DefaultValue = ""
                },
                new AttributeItem() {
                    Name = "Cotent",
                    Description = "弹窗内容",
                    Type = "string",
                    ValueList = "—",
                    DefaultValue = ""
                },
                new AttributeItem() {
                    Name = "Delay",
                    Description = "自动隐藏时间间隔",
                    Type = "int",
                    ValueList = "—",
                    DefaultValue = "4000"
                },
                new AttributeItem() {
                    Name = "IsAutoHide",
                    Description = "是否自动隐藏",
                    Type = "boolean",
                    ValueList = "",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsHtml",
                    Description = "内容中是否包含 Html 代码",
                    Type = "boolean",
                    ValueList = "",
                    DefaultValue = "false"
                }
            };
        }
    }
}
