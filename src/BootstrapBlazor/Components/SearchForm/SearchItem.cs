namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">EditorItem 搜索表单渲染项实体类</para>
/// <para lang="en">EditorItem form field class</para>
/// </summary>
/// <param name="fieldName"></param>
/// <param name="fieldType"></param>
/// <param name="fieldText"></param>
public class SearchItem(string fieldName, Type fieldType, string? fieldText = null) : ISearchItem
{
    /// <summary>
    /// <inheritdoc cref="ISearchItem.PropertyType"/>
    /// </summary>
    public Type PropertyType { get; } = fieldType;

    /// <summary>
    /// <inheritdoc cref="ISearchItem.Text"/>
    /// </summary>
    [NotNull]
    public string? Text { get; set; } = fieldText;

    /// <summary>
    /// <inheritdoc cref="ISearchItem.FieldName"/>
    /// </summary>
    public string FieldName { get; } = fieldName;

    /// <summary>
    /// <inheritdoc cref="ISearchItem.ShowLabel"/>
    /// </summary>
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchItem.ShowLabelTooltip"/>
    /// </summary>
    public bool? ShowLabelTooltip { get; set; }
    /// <summary>
    /// <inheritdoc cref="ISearchItem.GroupName"/>
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchItem.GroupOrder"/>
    /// </summary>
    public int GroupOrder { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchItem.Order"/>
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchItem.Cols"/>
    /// </summary>
    public int Cols { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchItem.MetaData"/>
    /// </summary>
    public ISearchMetaData? MetaData { get; set; }
}
