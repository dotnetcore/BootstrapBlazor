using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">搜索元数据接口</para>
/// <para lang="en">Search metadata interface</para>
/// </summary>
public interface ISearchMetaData
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索值变化回调</para>
    /// <para lang="en">Gets or sets the search value changed callback</para>
    /// </summary>
    Func<Task>? ValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤项与其他过滤条件逻辑关系</para>
    /// <para lang="en">Gets or sets logical relationship between filter item and other filters</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    FilterLogic FilterLogic { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件行为</para>
    /// <para lang="en">Gets or sets Filter condition behavior</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    FilterAction FilterAction { get; set; }

    /// <summary>
    /// <para lang="zh">根据字段名称获取过滤器实例</para>
    /// <para lang="en">Gets the filter instance based on the field name</para> 
    /// </summary>
    /// <param name="fieldName">
    ///   <para lang="zh">字段名称</para>
    ///   <para lang="en">Field name</para>
    /// </param>
    /// <returns>
    ///   <para lang="zh">过滤器实例</para>
    ///   <para lang="en">Filter instance</para>
    /// </returns>
    FilterKeyValueAction? GetFilter(string fieldName);

    /// <summary>
    /// <para lang="zh">获得/设置 获取过滤器实例回调，由 <see cref="GetFilter(string)"/> 方法调用</para>
    /// <para lang="en">Gets or sets the callback to get the filter instance, called by the <see cref="GetFilter(string)"/></para>
    /// </summary>
    Func<object?, FilterKeyValueAction>? GetFilterCallback { get; set; }
}
