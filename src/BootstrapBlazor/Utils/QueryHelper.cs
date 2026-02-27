// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.Encodings.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">提供解析和操作查询字符串的方法</para>
/// <para lang="en">Provides methods for parsing and manipulating query strings</para>
/// </summary>
[ExcludeFromCodeCoverage]
public static class QueryHelper
{
    /// <summary>
    /// <para lang="zh">将给定的查询键和值附加到 URI</para>
    /// <para lang="en">Append the given query key and value to the URI</para>
    /// </summary>
    /// <param name="uri">
    ///  <para lang="zh">基础 URI</para>
    ///  <para lang="en">The base URI.</para>
    /// </param>
    /// <param name="name">
    ///  <para lang="zh">查询键的名称</para>
    ///  <para lang="en">The name of the query key.</para>
    /// </param>
    /// <param name="value">
    ///  <para lang="zh">查询值</para>
    ///  <para lang="en">The query value.</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">组合后的结果</para>
    ///  <para lang="en">The combined result.</para>
    /// </returns>
    public static string AddQueryString(string uri, string name, string value) => AddQueryString(
            uri, new[] { new KeyValuePair<string, string?>(name, value) });

    /// <summary>
    /// <para lang="zh">将给定的查询键和值附加到 URI</para>
    /// <para lang="en">Append the given query keys and values to the URI</para>
    /// </summary>
    /// <param name="uri">
    ///  <para lang="zh">基础 URI</para>
    ///  <para lang="en">The base URI.</para>
    /// </param>
    /// <param name="queryString">
    ///  <para lang="zh">要附加的查询键和值的字典</para>
    ///  <para lang="en">A dictionary of query keys and values to append.</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">组合后的结果</para>
    ///  <para lang="en">The combined result.</para>
    /// </returns>
    public static string AddQueryString(string uri, IDictionary<string, string?> queryString) => AddQueryString(uri, (IEnumerable<KeyValuePair<string, string?>>)queryString);

    /// <summary>
    /// <para lang="zh">将给定的查询键和值附加到 URI</para>
    /// <para lang="en">Append the given query keys and values to the URI</para>
    /// </summary>
    /// <param name="uri">
    ///  <para lang="zh">基础 URI</para>
    ///  <para lang="en">The base URI.</para>
    /// </param>
    /// <param name="queryString">
    ///  <para lang="zh">要附加的查询键和值的集合</para>
    ///  <para lang="en">A collection of query names and values to append.</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">组合后的结果</para>
    ///  <para lang="en">The combined result.</para>
    /// </returns>
    public static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, StringValues>> queryString) => AddQueryString(uri, queryString.SelectMany(kvp => kvp.Value, (kvp, v) => KeyValuePair.Create<string, string?>(kvp.Key, v)));

    /// <summary>
    /// <para lang="zh">将给定的查询键和值附加到 URI</para>
    /// <para lang="en">Append the given query keys and values to the URI</para>
    /// </summary>
    /// <param name="uri">
    ///  <para lang="zh">基础 URI</para>
    ///  <para lang="en">The base URI.</para>
    /// </param>
    /// <param name="queryString">
    ///  <para lang="zh">要附加的查询键和值的集合</para>
    ///  <para lang="en">A collection of name value query pairs to append.</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">组合后的结果</para>
    ///  <para lang="en">The combined result.</para>
    /// </returns>
    public static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, string?>> queryString)
    {
        var anchorIndex = uri.IndexOf('#');
        var uriToBeAppended = uri;
        var anchorText = "";
        // If there is an anchor, then the query string must be inserted before its first occurrence.
        if (anchorIndex != -1)
        {
            anchorText = uri[anchorIndex..];
            uriToBeAppended = uri[..anchorIndex];
        }

        var queryIndex = uriToBeAppended.IndexOf('?');
        var hasQuery = queryIndex != -1;

        var sb = new StringBuilder();
        sb.Append(uriToBeAppended);
        foreach (var parameter in queryString)
        {
            if (parameter.Value == null)
            {
                continue;
            }

            sb.Append(hasQuery ? '&' : '?');
            sb.Append(UrlEncoder.Default.Encode(parameter.Key));
            sb.Append('=');
            sb.Append(UrlEncoder.Default.Encode(parameter.Value));
            hasQuery = true;
        }

        sb.Append(anchorText);
        return sb.ToString();
    }

    /// <summary>
    /// <para lang="zh">将查询字符串解析为其组成的键和值部分</para>
    /// <para lang="en">Parse a query string into its component key and value parts</para>
    /// </summary>
    /// <param name="queryString">
    ///  <para lang="zh">原始查询字符串值，可以带或不带前导 '?'</para>
    ///  <para lang="en">The raw query string value, with or without the leading '?'.</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">解析后的键值集合</para>
    ///  <para lang="en">A collection of parsed keys and values.</para>
    /// </returns>
    public static Dictionary<string, StringValues> ParseQuery(string? queryString) => ParseNullableQuery(queryString) ?? [];

    /// <summary>
    /// <para lang="zh">将查询字符串解析为其组成的键和值部分</para>
    /// <para lang="en">Parse a query string into its component key and value parts</para>
    /// </summary>
    /// <param name="queryString">
    ///  <para lang="zh">原始查询字符串值，可以带或不带前导 '?'</para>
    ///  <para lang="en">The raw query string value, with or without the leading '?'.</para>
    /// </param>
    /// <returns>
    ///  <para lang="zh">解析后的键值集合，如果没有条目则为 null</para>
    ///  <para lang="en">A collection of parsed keys and values, null if there are no entries.</para>
    /// </returns>
    public static Dictionary<string, StringValues>? ParseNullableQuery(string? queryString)
    {
        Dictionary<string, StringValues>? ret = null;
        var q = queryString.AsMemory();
        var payload = q.IsEmpty || q.Span[0] != '?'
            ? q
            : q[1..];

        while (!payload.IsEmpty)
        {
            ReadOnlyMemory<char> segment;
            var delimiterIndex = payload.Span.IndexOf('&');
            if (delimiterIndex >= 0)
            {
                segment = payload[..delimiterIndex];
                payload = payload[(delimiterIndex + 1)..];
            }
            else
            {
                segment = payload;
                payload = default;
            }

            // If it's nonempty, emit it
            var equalIndex = segment.Span.IndexOf('=');
            if (equalIndex >= 0)
            {
                ret ??= [];
                var v = Uri.UnescapeDataString(segment[(equalIndex + 1)..].ToString());
                ret.Add(segment[..equalIndex].ToString(), v);
            }
            else if (!segment.IsEmpty)
            {
                ret ??= [];
                ret.Add(segment.ToString(), default);
            }
        }
        return ret;
    }
}
