// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class RecordNavigator
{
    /// <summary>
    /// 获得/设置 记录索引总数
    /// </summary>
    [Parameter]
    public int RecordCount { set; get; }
    /// <summary>
    /// 获得/设置 显示记录数，其余用...切换
    /// </summary>
    [Parameter]
    public int DisplayRecords { set; get; }


    /// <summary>
    /// 获得 Navigator 样式
    /// </summary>
    /// <returns></returns>
    protected string? RecordClass => CssBuilder.Default("recordnavigator")
        .AddClass("d-none", RecordCount <= 1)
        .Build();

    /// <summary>
    /// 获得 NavigatorBar 样式
    /// </summary>
    /// <returns></returns>
    protected string? NavigatorBarClass => CssBuilder.Default("navigator-bar")
        .AddClass("d-none", !ShowNavigatorInfo)
        .Build();


    /// <summary>
    /// 获得 RecordItem 样式
    /// </summary>
    /// <param name="active"></param>
    /// <returns></returns>
    protected static string? GetRecordItemClassName(bool active) => CssBuilder.Default("record-items")
        .AddClass("record-items-active", active)
        .Build();
    /// <summary>
    /// 获得 RecordLink 样式
    /// </summary>
    /// <param name="active"></param>
    /// <returns></returns>
    protected static string? GetRecordLinkClassName(bool active)
    {

        var css = active ? CssBuilder.Default("record-link-active") : CssBuilder.Default("record-link");
        return css.Build();
    }
    /// <summary>
    /// 获得/设置 开始记录索引
    /// </summary>
    protected int StartIndex {  get

        {
            int _start = 0;
            
        
            if ((Index+ DisplayRecords / 2) > RecordCount)
            {
                _start = RecordCount - DisplayRecords;
            }
            else
            {
                _start = Index - DisplayRecords / 2;
            }
            return _start > 1 ? _start : 2; 
        }
    }

    /// <summary>
    /// 获得/设置 结束记录索引
    /// </summary>
    protected int EndIndex {
        get
        {
            int _end = StartIndex+DisplayRecords;
            return _end < RecordCount ? _end :RecordCount ;
        }
    }
     

    /// <summary>
    /// 获得/设置 当前记录索引
    /// </summary>
    [Parameter]
    public int  Index { get; set; } = 1;
  

    /// <summary>
    /// 获得/设置 是否显示分页数据汇总信息 默认为 true 显示
    /// </summary>
    /// <value></value>
    [Parameter] public bool ShowNavigatorInfo { get; set; } = true;

     

    /// <summary>
    /// 点击记录索引时回调方法 第一个参数是当前记录索引，
    /// </summary>
    [Parameter]
    public Func<int,  Task>? OnIndexClick { get; set; }

  

    [Inject]
    [NotNull]
    private IStringLocalizer<RecordNavigator>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 AiraIndexLabel 显示文字 默认为 分页组件
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraIndexLabel { get; set; }

    /// <summary>
    /// 获得/设置 AiraPrevIndexText 显示文字 默认为 上一条
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraPrevIndexText { get; set; }

    /// <summary>
    /// 获得/设置 AiraFirstIndexText 显示文字 默认为 第一条
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraFirstIndexText { get; set; }

    /// <summary>
    /// 获得/设置 AiraNextIndexText 显示文字 默认为 下一条
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraNextIndexText { get; set; }

 
     

    /// <summary>
    /// 获得/设置 IndexInfoText 显示文字 默认为 显示 第 {0} 条/共 {1} 条记录
    /// </summary>
    [Parameter]
    [NotNull]
    public string? IndexInfoText { get; set; }
 

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
         
        AiraPrevIndexText ??= Localizer[nameof(AiraPrevIndexText)];
        AiraFirstIndexText ??= Localizer[nameof(AiraFirstIndexText)];
        AiraNextIndexText ??= Localizer[nameof(AiraNextIndexText)];  
        IndexInfoText ??= "第 {0} 条/共 {1} 条记录"; 
    }

    /// <summary>
    /// 上一页方法
    /// </summary>
    protected async Task MovePrev(int index)
    {
        var tmpIndex = Index > 1 ? Math.Max(1, Index - index) : RecordCount;
        await OnIndexItemClick(tmpIndex);
    }

    /// <summary>
    /// 下一页方法
    /// </summary>
    public async Task MoveNext(int index)
    {
        var tmpIndex = Index < RecordCount ? Math.Min(RecordCount, Index + index) : 1;
        await OnIndexItemClick(tmpIndex);
    }

    /// <summary>
    /// 点击记录索引时回调方法
    /// </summary>
    /// <param name="Index"></param>
    protected async Task OnIndexItemClick(int tmpIndex)
    {
        Index = tmpIndex;
        if (OnIndexClick != null)
        {
            await OnIndexClick.Invoke(Index);
        }
    }
     
     
    private string GetIndexInfoText() => string.Format(IndexInfoText, Index, RecordCount);
     
}
