using BootstrapBlazor.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TablesBaseColumn : TablesBaseQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        protected Task<string> IntFormatter(object? d)
        {
            var data = (int?)d;
            return Task.FromResult(data?.ToString("0.00") ?? "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        protected Task CustomerButton(IEnumerable<BindItem> items)
        {
            var cate = ToastCategory.Information;
            var title = "自定义按钮处理方法";
            var content = $"通过不同的函数区分按钮处理逻辑，参数 Items 为 Table 组件中选中的行数据集合，当前选择数据 {items.Count()} 条";
            ToastService?.Show(new ToastOption()
            {
                Category = cate,
                Title = title,
                Content = content
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        protected Task OnRowButtonClick(BindItem item)
        {
            var cate = ToastCategory.Success;
            var title = "行内按钮处理方法";
            var content = "通过不同的函数区分按钮处理逻辑，参数 Item 为当前行数据";
            ToastService?.Show(new ToastOption()
            {
                Category = cate,
                Title = title,
                Content = content
            });
            return Task.CompletedTask;
        }
    }
}
