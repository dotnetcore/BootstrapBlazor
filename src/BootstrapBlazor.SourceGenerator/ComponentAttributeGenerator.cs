using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapBlazor.SourceGenerator
{
    [Generator]
    public class ComponentAttributeGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this generator
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // Find all syntax trees in the compilation
            var compilation = context.Compilation;
            var componentsDict = new Dictionary<string, List<PropertyInfo>>();

            // Get all named types in the compilation (including from referenced assemblies)
            var allTypes = GetAllTypes(compilation.GlobalNamespace);

            foreach (var typeSymbol in allTypes)
            {
                // Only process classes in BootstrapBlazor.Components namespace
                if (!typeSymbol.ContainingNamespace.ToDisplayString().StartsWith("BootstrapBlazor.Components"))
                    continue;

                // Skip abstract classes
                if (typeSymbol.IsAbstract)
                    continue;

                // Get all properties with [Parameter] attribute from this class and its base classes
                var properties = GetParameterProperties(typeSymbol);
                if (properties.Count > 0)
                {
                    var componentName = typeSymbol.Name;
                    if (!componentsDict.ContainsKey(componentName))
                    {
                        componentsDict[componentName] = new List<PropertyInfo>();
                    }
                    componentsDict[componentName].AddRange(properties);
                }
            }

            // Generate the source code
            if (componentsDict.Count > 0)
            {
                var sourceCode = GenerateAttributeProvider(componentsDict);
                context.AddSource("ComponentAttributeProvider.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
            }
        }

        private IEnumerable<INamedTypeSymbol> GetAllTypes(INamespaceSymbol namespaceSymbol)
        {
            foreach (var type in namespaceSymbol.GetTypeMembers())
            {
                yield return type;
            }

            foreach (var childNamespace in namespaceSymbol.GetNamespaceMembers())
            {
                foreach (var type in GetAllTypes(childNamespace))
                {
                    yield return type;
                }
            }
        }

        private List<PropertyInfo> GetParameterProperties(INamedTypeSymbol classSymbol)
        {
            var properties = new List<PropertyInfo>();
            var processedProperties = new HashSet<string>();

            // Walk up the inheritance chain
            var currentType = classSymbol;
            while (currentType != null)
            {
                foreach (var member in currentType.GetMembers())
                {
                    if (member is IPropertySymbol property && !processedProperties.Contains(property.Name))
                    {
                        // Check if property has [Parameter] attribute
                        var hasParameterAttribute = property.GetAttributes()
                            .Any(attr => attr.AttributeClass?.Name == "ParameterAttribute");

                        if (hasParameterAttribute)
                        {
                            var propInfo = new PropertyInfo
                            {
                                Name = property.Name,
                                Type = GetSimpleTypeName(property.Type),
                                Description = GetDocumentationComment(property),
                                DefaultValue = GetDefaultValue(property)
                            };
                            properties.Add(propInfo);
                            processedProperties.Add(property.Name);
                        }
                    }
                }

                currentType = currentType.BaseType;
            }

            return properties;
        }

        private string GetSimpleTypeName(ITypeSymbol type)
        {
            if (type is INamedTypeSymbol namedType)
            {
                if (namedType.IsGenericType)
                {
                    var typeName = namedType.Name;
                    var typeArgs = string.Join(", ", namedType.TypeArguments.Select(GetSimpleTypeName));
                    var result = $"{typeName}<{typeArgs}>";
                    
                    // Handle nullable
                    if (namedType.NullableAnnotation == NullableAnnotation.Annotated)
                    {
                        return result + "?";
                    }
                    return result;
                }
            }

            var displayString = type.ToDisplayString();
            
            // Handle nullable reference types
            if (type.NullableAnnotation == NullableAnnotation.Annotated && !displayString.EndsWith("?"))
            {
                displayString += "?";
            }

            return displayString;
        }

        private string GetDocumentationComment(ISymbol symbol)
        {
            var xmlDoc = symbol.GetDocumentationCommentXml();
            if (string.IsNullOrEmpty(xmlDoc))
                return string.Empty;

            try
            {
                // Parse XML and extract summary
                var doc = System.Xml.Linq.XDocument.Parse(xmlDoc);
                var summary = doc.Descendants("summary").FirstOrDefault();
                if (summary != null)
                {
                    return summary.Value.Trim();
                }
            }
            catch
            {
                // Ignore XML parsing errors
            }

            return string.Empty;
        }

        private string GetDefaultValue(IPropertySymbol property)
        {
            // Try to get default value from property initializer
            var syntaxReferences = property.DeclaringSyntaxReferences;
            foreach (var syntaxRef in syntaxReferences)
            {
                var syntax = syntaxRef.GetSyntax();
                if (syntax is PropertyDeclarationSyntax propDecl)
                {
                    if (propDecl.Initializer != null)
                    {
                        var initValue = propDecl.Initializer.Value.ToString();
                        // Clean up the value
                        return initValue;
                    }
                }
            }

            // Return default based on type
            if (property.Type.IsValueType && property.Type.NullableAnnotation != NullableAnnotation.Annotated)
            {
                if (property.Type.SpecialType == SpecialType.System_Boolean)
                    return "false";
                if (property.Type.SpecialType == SpecialType.System_Int32)
                    return "0";
                if (property.Type.TypeKind == TypeKind.Enum)
                    return property.Type.Name + "." + property.Type.GetMembers().OfType<IFieldSymbol>().FirstOrDefault()?.Name;
            }

            return "null";
        }

        private string GenerateAttributeProvider(Dictionary<string, List<PropertyInfo>> componentsDict)
        {
            var sb = new StringBuilder();
            sb.AppendLine("#nullable enable");
            sb.AppendLine();
            sb.AppendLine("using BootstrapBlazor.Server.Data;");
            sb.AppendLine();
            sb.AppendLine("namespace BootstrapBlazor.Server.Components.Components;");
            sb.AppendLine();
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// Auto-generated component attribute provider");
            sb.AppendLine("/// </summary>");
            sb.AppendLine("public static partial class ComponentAttributeProvider");
            sb.AppendLine("{");
            sb.AppendLine("    private static readonly Dictionary<string, AttributeItem[]> _attributes = new()");
            sb.AppendLine("    {");

            foreach (var kvp in componentsDict.OrderBy(x => x.Key))
            {
                var componentName = kvp.Key;
                var properties = kvp.Value;

                sb.AppendLine($"        [\"{componentName}\"] = new AttributeItem[]");
                sb.AppendLine("        {");

                foreach (var prop in properties)
                {
                    sb.AppendLine("            new()");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                Name = \"{EscapeString(prop.Name)}\",");
                    sb.AppendLine($"                Description = \"{EscapeString(prop.Description)}\",");
                    sb.AppendLine($"                Type = \"{EscapeString(prop.Type)}\",");
                    sb.AppendLine($"                DefaultValue = \"{EscapeString(prop.DefaultValue)}\"");
                    sb.AppendLine("            },");
                }

                sb.AppendLine("        },");
            }

            sb.AppendLine("    };");
            sb.AppendLine();
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// Get attributes for a component by name");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    /// <param name=\"componentName\">Component name</param>");
            sb.AppendLine("    /// <returns>Array of attribute items or null if not found</returns>");
            sb.AppendLine("    public static AttributeItem[]? GetAttributes(string componentName)");
            sb.AppendLine("    {");
            sb.AppendLine("        return _attributes.TryGetValue(componentName, out var items) ? items : null;");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string EscapeString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Replace("\"", "\\\"").Replace("\r", "").Replace("\n", " ");
        }

        private class PropertyInfo
        {
            public string Name { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string DefaultValue { get; set; } = string.Empty;
        }
    }
}
