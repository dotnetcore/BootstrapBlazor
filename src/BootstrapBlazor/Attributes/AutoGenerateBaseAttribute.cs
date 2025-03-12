// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Base class for AutoGenerateColumn attribute, used to mark auto-generated columns in <see cref="Table{TItem}"/>
/// </summary>
public abstract class AutoGenerateBaseAttribute : Attribute
{
    /// <summary>
    /// Gets or sets whether the current column is editable. Default is true. When set to false, the auto-generated edit UI will not generate this column.
    /// </summary>
    [Obsolete("Deprecated. If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the current column is rendered. Default is false. When set to true, the UI will not generate this column.
    /// </summary>
    public bool Ignore { get; set; }

    /// <summary>
    /// Gets or sets whether the current edit item is read-only. Default is false.
    /// </summary>
    public bool Readonly { get; set; }

    /// <summary>
    /// Gets or sets whether the current edit item is visible. Default is true.
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Gets or sets whether sorting is allowed. Default is false.
    /// </summary>
    public bool Sortable { get; set; }

    /// <summary>
    /// Gets or sets whether data filtering is allowed. Default is false.
    /// </summary>
    public bool Filterable { get; set; }

    /// <summary>
    /// Gets or sets whether the column participates in search. Default is false.
    /// </summary>
    public bool Searchable { get; set; }

    /// <summary>
    /// Gets or sets whether text wrapping is allowed in this column. Default is false.
    /// </summary>
    public bool TextWrap { get; set; }

    /// <summary>
    /// Gets or sets whether text overflow is ellipsis in this column. Default is false.
    /// </summary>
    public bool TextEllipsis { get; set; }

    /// <summary>
    /// Gets or sets the text alignment. Default is Alignment.None.
    /// </summary>
    public Alignment Align { get; set; }

    /// <summary>
    /// Gets or sets whether to show tooltips on mouse hover. Default is false.
    /// </summary>
    public bool ShowTips { get; set; }

    /// <summary>
    /// Gets or sets whether the column can be copied. Default is false.
    /// </summary>
    public bool ShowCopyColumn { get; set; }
}
