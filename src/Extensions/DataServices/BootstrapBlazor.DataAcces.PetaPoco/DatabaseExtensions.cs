// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using PetaPoco;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.DataAcces.PetaPoco
{
    /// <summary>
    /// 
    /// </summary>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static Task<List<TModel>> FetchAsync<TModel>(this IDatabase db, IEnumerable<IFilterAction> where, string? sortName = null, SortOrder sortOrder = SortOrder.Unset)
        {
            var exp = where.GetFilterLambda<TModel>();
            var sql = new Sql();
            AnalysisExpression(exp, db, sql);

            if (!string.IsNullOrEmpty(sortName) && sortOrder != SortOrder.Unset)
            {
                sql.OrderBy(sortOrder == SortOrder.Asc ? sortName : $"{sortName} desc");
            }
            return db.FetchAsync<TModel>(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static Task<Page<TModel>> PageAsync<TModel>(this IDatabase db, long pageIndex, long pageItems, IEnumerable<IFilterAction> where, string? sortName = null, SortOrder sortOrder = SortOrder.Unset)
        {
            var exp = where.GetFilterLambda<TModel>();
            var sql = new Sql();
            AnalysisExpression(exp, db, sql);

            if (!string.IsNullOrEmpty(sortName) && sortOrder != SortOrder.Unset)
            {
                sql.OrderBy(sortOrder == SortOrder.Asc ? sortName : $"{sortName} desc");
            }
            return db.PageAsync<TModel>(pageIndex, pageItems, sql);
        }

        private static void AnalysisExpression(Expression expression, IDatabase db, Sql sql)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    var exp = expression as LambdaExpression;
                    AnalysisExpression(exp!.Body, db, sql);
                    break;
                case ExpressionType.AndAlso:
                    var andExp = expression as BinaryExpression;
                    AnalysisExpression(andExp!.Left, db, sql);
                    AnalysisExpression(andExp!.Right, db, sql);
                    break;
                case ExpressionType.Call:
                    var callExp = expression as MethodCallExpression;
                    if (callExp!.Method.Name == "Contains")
                    {
                        var callLeft = callExp!.Object as MemberExpression;
                        var callColName = GetColumnName(callLeft!.Member) ?? callLeft!.Member.Name;
                        var p = (callExp.Arguments[0] as ConstantExpression)?.Value;
                        if (p != null)
                        {
                            sql.Where($"{db.Provider.EscapeSqlIdentifier(callColName)} like @0", $"%{p}%");
                        }
                    }
                    break;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                    var binaryExp = (expression as BinaryExpression)!;
                    var left = (binaryExp.Left as MemberExpression)!;

                    // 查找 PetaPoco.Column 标签
                    var columnName = GetColumnName(left.Member) ?? left.Member.Name;

                    // 查找操作符右侧
                    var right = (binaryExp.Right as ConstantExpression)!;
                    var v = right.Value;

                    if (v != null)
                    {
                        var operatorExp = GetOperatorExpression(expression);
                        sql.Where($"{db.Provider.EscapeSqlIdentifier(columnName)} {operatorExp} @0", v);
                    }
                    break;

            }
        }

        private static string? GetColumnName(MemberInfo member) => member.CustomAttributes
            .FirstOrDefault(i => i.AttributeType == typeof(ColumnAttribute))?.NamedArguments
            .FirstOrDefault(i => i.MemberName == "Name").TypedValue.Value?.ToString();

        private static string GetOperatorExpression(Expression expression) => expression.NodeType switch
        {
            ExpressionType.Equal => "=",
            ExpressionType.NotEqual => "!=",
            ExpressionType.GreaterThan => ">",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThan => "<",
            ExpressionType.LessThanOrEqual => "<=",
            _ => ""
        };
    }
}
