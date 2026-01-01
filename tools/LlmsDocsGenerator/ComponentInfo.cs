// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace LlmsDocsGenerator;

/// <summary>
/// Represents information about a Blazor component
/// </summary>
public class ComponentInfo
{
    /// <summary>
    /// Component name (e.g., "Table", "Button")
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Full type name including namespace
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// XML documentation summary
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Generic type parameters (e.g., "TItem", "TValue")
    /// </summary>
    public List<string> TypeParameters { get; set; } = new();

    /// <summary>
    /// Component parameters ([Parameter] properties)
    /// </summary>
    public List<ParameterInfo> Parameters { get; set; } = new();

    /// <summary>
    /// Public methods
    /// </summary>
    public List<MethodInfo> PublicMethods { get; set; } = new();

    /// <summary>
    /// Base class name
    /// </summary>
    public string? BaseClass { get; set; }

    /// <summary>
    /// Source file path
    /// </summary>
    public string SourcePath { get; set; } = string.Empty;

    /// <summary>
    /// Last modification time of the source file
    /// </summary>
    public DateTime LastModified { get; set; }

    /// <summary>
    /// Related sample file path (if exists)
    /// </summary>
    public string? SamplePath { get; set; }
}

/// <summary>
/// Represents a component parameter
/// </summary>
public class ParameterInfo
{
    /// <summary>
    /// Parameter name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Parameter type as string
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Default value (if any)
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// XML documentation summary
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Whether this is an EditorRequired parameter
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Whether this parameter is obsolete
    /// </summary>
    public bool IsObsolete { get; set; }

    /// <summary>
    /// Obsolete message (if obsolete)
    /// </summary>
    public string? ObsoleteMessage { get; set; }

    /// <summary>
    /// Whether this is an EventCallback
    /// </summary>
    public bool IsEventCallback { get; set; }
}

/// <summary>
/// Represents a public method
/// </summary>
public class MethodInfo
{
    /// <summary>
    /// Method name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Return type
    /// </summary>
    public string ReturnType { get; set; } = string.Empty;

    /// <summary>
    /// Method parameters
    /// </summary>
    public List<(string Type, string Name)> Parameters { get; set; } = new();

    /// <summary>
    /// XML documentation summary
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Whether this is a JSInvokable method
    /// </summary>
    public bool IsJSInvokable { get; set; }
}
