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
        /// 点击控件时触发上一页
        /// </summary>
        protected void PrevClick()
        {
            CurrentPage = CurrentPage == 1 ? Total : (CurrentPage - 1);
        }

        /// <summary>
        /// 点击控件时触发下一页
        /// </summary>
        protected void NextClick()
        {
            CurrentPage = CurrentPage == Total ? 1 : (CurrentPage + 1);
        }

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
            if (Total <= 7)
            {
                for (int i = 1; i <= Total; i++)
                {
                    list.Add(i.ToString());
                }
                return list;
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
            return list;
        }

    }
}
