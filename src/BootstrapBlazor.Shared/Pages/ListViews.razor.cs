using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazor.Shared.Pages.Components;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ListViews
    {
        private Logger? Trace { get; set; }
        private IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Products = Enumerable.Range(1, 100).Select(i => new Product()
            {
                ImageUrl = $"https://imgs.sdgxgz.com/images/Pic{i}.jpg",
                Description = $"Pic{i}.jpg",
                Category = $"Group{(i % 4) + 1}"
            });
        }

        private Task<QueryData<Product>> OnQueryAsync(QueryPageOptions options)
        {
            var items = Products.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
            return Task.FromResult(new QueryData<Product>()
            {
                Items = items,
                TotalCount = Products.Count()
            });
        }
        private Task OnListViewItemClick(Product item)
        {
            Trace?.Log($"ListViewItem: {item.Description} clicked");
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
                new AttributeItem(){
                    Name = "Items",
                    Description = "组件数据源",
                    Type = "IEnumerable<TItem>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "Pageable",
                    Description = "是否分页",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem(){
                    Name = "PageItemsSource",
                    Description = "每页显示数量数据源",
                    Type = "IEnumerable<int>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "HeaderTemplate",
                    Description = "ListView Header 模板",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "BodyTemplate",
                    Description = "ListView Body 模板",
                    Type = "RenderFragment<TItem>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "FooterTemplate",
                    Description = "ListView Footer 模板",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "OnQueryAsync",
                    Description = "异步查询回调方法",
                    Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
                    ValueList = "—",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "OnListViewItemClick",
                    Description = "ListView元素点击时回调委托",
                    Type = "Func<TItem, Task>",
                    ValueList = " — ",
                    DefaultValue = " — "
                }
            };
        }

        private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            new MethodItem()
            {
                Name = "QueryAsync",
                Description = "手工查询数据方法",
                Parameters = " - ",
                ReturnValue = "Task"
            },
        };
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Product
    {
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; } = "";
    }
}
