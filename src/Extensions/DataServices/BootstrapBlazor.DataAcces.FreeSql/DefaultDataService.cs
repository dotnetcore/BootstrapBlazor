// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using FreeSql.Internal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.DataAcces.FreeSql
{
    /// <summary>
    /// FreeSql ORM 的 IDataService 接口实现
    /// </summary>
    internal class DefaultDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
    {
        private readonly IFreeSql _db;
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultDataService(IFreeSql db)
        {
            _db = db;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task<bool> DeleteAsync(IEnumerable<TModel> models)
        {
            // 通过模型获取主键列数据
            // 支持批量删除
            await _db.Delete<TModel>(models).ExecuteAffrowsAsync();
            return true;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<bool> SaveAsync(TModel model)
        {
            await _db.GetRepository<TModel>().InsertOrUpdateAsync(model);
            return true;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            var Items = FetchAsync(option, out var count);
            var ret = new QueryData<TModel>()
            {
                TotalCount = (int)count,
                Items = Items
            };
            return Task.FromResult(ret);
        }

        private List<TModel> FetchAsync(QueryPageOptions option, out long count)
        {
            var dynamicFilterInfo = MakeDynamicFilterInfo(option, out var isSerach);
            var fsql_select = _db.Select<TModel>();
            if (isSerach) fsql_select = fsql_select.WhereDynamicFilter(dynamicFilterInfo);
            return fsql_select
                .OrderByPropertyNameIf(option.SortOrder != SortOrder.Unset, option.SortName, option.SortOrder == SortOrder.Asc)
                .Count(out count)
                .Page(option.PageIndex, option.PageItems).ToList();

        }


        #region 生成Where子句的DynamicFilterInfo对象
        /// <summary>
        /// 生成Where子句的DynamicFilterInfo对象
        /// </summary>
        /// <param name="option"></param>
        /// <param name="isSerach"></param>
        /// <returns></returns>
        private DynamicFilterInfo? MakeDynamicFilterInfo(QueryPageOptions option, out bool isSerach)
        {
            var filters = new List<DynamicFilterInfo>();

            object? searchModel = option.SearchModel;
            Type type = searchModel.GetType();

            var instance = Activator.CreateInstance(type);

            if (string.IsNullOrEmpty(option.SearchText))
            {
                //生成高级搜索子句
                //TODO : 支持更多类型
                foreach (var propertyinfo in type.GetProperties().Where(a => a.PropertyType == typeof(string) || a.PropertyType == typeof(int)).ToList())
                {
                    if (propertyinfo.GetValue(searchModel) != null && !propertyinfo.GetValue(searchModel).Equals(propertyinfo.GetValue(instance)))
                    {
                        string propertyValue = propertyinfo.GetValue(searchModel).ToString();
                        if (propertyinfo.PropertyType == typeof(int) && !IsNumeric(propertyValue)) continue;

                        filters.Add(new DynamicFilterInfo()
                        {
                            Field = propertyinfo.Name,
                            Operator = propertyinfo.PropertyType == typeof(int) ? DynamicFilterOperator.Equal : DynamicFilterOperator.Contains,
                            Value = propertyinfo.PropertyType == typeof(int) ? Convert.ToInt32(propertyValue) : propertyValue,
                        });
                    }
                }

            }
            else
            {
                //生成默认搜索子句
                //TODO : 支持更多类型
                foreach (var propertyinfo in type.GetProperties().Where(a => a.PropertyType == typeof(string) || a.PropertyType == typeof(int)).ToList())
                {
                    if (propertyinfo.PropertyType == typeof(int) && !IsNumeric(option.SearchText)) continue;

                    filters.Add(new DynamicFilterInfo()
                    {
                        Field = propertyinfo.Name,
                        Operator = propertyinfo.PropertyType == typeof(int) ? DynamicFilterOperator.Equal : DynamicFilterOperator.Contains,
                        Value = propertyinfo.PropertyType == typeof(int) ? Convert.ToInt32(option.SearchText) : option.SearchText,
                    });
                }

            }

            if (option.Filters.Any())
            {
                foreach (var item in option.Filters)
                {
                    var filter = item.GetFilterConditions().First();
                    var filterOperator = DynamicFilterOperator.Contains;

                    switch (filter.FilterAction)
                    {
                        case FilterAction.Contains:
                            filterOperator = DynamicFilterOperator.Contains;
                            break;
                        case FilterAction.NotContains:
                            filterOperator = DynamicFilterOperator.NotContains;
                            break;
                        case FilterAction.NotEqual:
                            filterOperator = DynamicFilterOperator.NotEqual;
                            break;
                        case FilterAction.Equal:
                            filterOperator = DynamicFilterOperator.Equal;
                            break;
                    }

                    filters.Add(new DynamicFilterInfo()
                    {
                        Field = filter.FieldKey,
                        Operator = filterOperator,
                        Value = filter.FieldValue,
                    });
                }
            }

            if (filters.Any())
            {
                DynamicFilterInfo dyfilter = new DynamicFilterInfo()
                {
                    Logic = string.IsNullOrEmpty(option.SearchText) ? DynamicFilterLogic.And : DynamicFilterLogic.Or,
                    Filters = filters
                };
                isSerach = true;
                return dyfilter;

            }

            isSerach = false;
            return null;
        }
        private bool IsNumeric(string text) => double.TryParse(text, out _);
        #endregion

    }
}
