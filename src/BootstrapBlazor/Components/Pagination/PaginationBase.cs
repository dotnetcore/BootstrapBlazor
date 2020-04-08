using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Pagination 分页组件
    /// </summary>
    public abstract class PaginationBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 分页样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("pagination")
            .AddClass($"pagination-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();
 
        /// <summary>
        /// 获得/设置 Size 大小
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.None;

        /// <summary>
        /// 获得/设置 当前页数
        /// </summary>
        [Parameter] public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 获得/设置 每页大小
        /// </summary>
        [Parameter] public int? PageSize { get; set; }

        /// <summary>
        /// 获得/设置 分页总页数
        /// </summary>
        [Parameter] public int Total { get; set; } = 1;

        /// <summary>
        /// 获得/设置 分页设置要禁止
        /// </summary>
        [Parameter] public List<string> DisableList { get; set; } = new List<string>();

        /// <summary>
        /// 设置点击页数
        /// </summary>
        protected void OnClick(string SetPage)
        {
            if (SetPage == "<<")
            {
                CurrentPage = CurrentPage - 4 <= 1 ? 1 : CurrentPage - 4;
            }
            else if (SetPage == ">>")
            {
                CurrentPage = CurrentPage + 4 >= Total ? Total : CurrentPage + 4;
            }
            else if (SetPage == "Previous")
            {
                CurrentPage = CurrentPage == 1 ? 1 : (CurrentPage - 1);
            }
            else if (SetPage == "Next")
            {
                CurrentPage = CurrentPage == Total ? Total : (CurrentPage + 1);              
            }
            else
            {
                CurrentPage = int.Parse(SetPage);
            }    
        }

        /// <summary>
        /// 设置显示页数
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        protected List<string> GetShowPagination(int CurrentPage, int Total)
        {
            List<string> list = new List<string>();
            list.Add("Previous");
            if (Total <= 7)
            {
                for (int i = 1; i <= Total; i++)
                {
                    list.Add(i.ToString());
                }
            }
            else
            {
                if (CurrentPage - 3 <= 1)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        list.Add(i.ToString());
                    }
                    list.Add(">>");
                    list.Add(Total.ToString());
                }
                else if (CurrentPage + 3 >= Total)
                {
                    list.Add("1");
                    list.Add("<<");
                    for (int i = Total - 5; i <= Total; i++)
                    {
                        list.Add(i.ToString());
                    }
                }
                else
                {
                    list.Add("1");
                    list.Add("<<");
                    for (int i = CurrentPage - 2; i <= CurrentPage + 2; i++)
                    {
                        list.Add(i.ToString());
                    }
                    list.Add(">>");
                    list.Add(Total.ToString());
                }
            }
            list.Add("Next");
            return list;
        }

        /// <summary>
        /// 获取 分页li样式集合
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="ShowPagination"></param>
        /// <param name="Disabled"></param>
        /// <returns></returns>
        protected string? GetLiClassName(int CurrentPage,string ShowPagination ,bool Disabled)
        {
            return CssBuilder.Default("page-item")
            .AddClass("active", CurrentPage.ToString()== ShowPagination)
            .AddClass("disabled", Disabled)
            .Build();
        }
    }
}
