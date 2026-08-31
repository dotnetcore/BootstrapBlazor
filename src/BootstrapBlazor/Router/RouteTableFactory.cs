// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
internal static class RouteTableFactory
{
    private static readonly object _lock = new();
    private static readonly HashSet<Assembly> _assemblies = [];
    private static RouteDefinition[] _routes = [];

    public static RouteContext Create(IEnumerable<Assembly> assemblies, string url)
    {
        var routes = GetRoutes(assemblies);
        var segments = GetSegments(url);

        foreach (var route in routes)
        {
            if (route.TryMatch(segments, out var parameters))
            {
                return new RouteContext(segments, route.Handler, parameters);
            }
        }

        return new RouteContext(segments);
    }

    private static RouteDefinition[] GetRoutes(IEnumerable<Assembly> assemblies)
    {
        var assemblySet = assemblies.ToHashSet();
        lock (_lock)
        {
            if (!_assemblies.SetEquals(assemblySet))
            {
                _routes = CreateRoutes(assemblySet);
                _assemblies.Clear();
                _assemblies.UnionWith(assemblySet);
            }

            return _routes;
        }
    }

    private static RouteDefinition[] CreateRoutes(IEnumerable<Assembly> assemblies)
    {
        var routes = new List<RouteDefinition>();
        foreach (var componentType in assemblies.SelectMany(assembly => assembly.ExportedTypes)
                     .Where(type => typeof(IComponent).IsAssignableFrom(type)))
        {
            var templates = componentType.GetCustomAttributes<RouteAttribute>(inherit: false)
                .Select(attribute => ParseTemplate(attribute.Template))
                .ToArray();
            if (templates.Length == 0)
            {
                continue;
            }

            var allParameterNames = templates
                .SelectMany(template => template.Segments)
                .Where(segment => segment.IsParameter)
                .Select(segment => segment.Value)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var template in templates)
            {
                var routeParameterNames = template.Segments
                    .Where(segment => segment.IsParameter)
                    .Select(segment => segment.Value)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
                var unusedParameterNames = allParameterNames
                    .Where(name => !routeParameterNames.Contains(name))
                    .ToArray();

                routes.Add(new RouteDefinition(template.Template, template.Segments, componentType, unusedParameterNames));
            }
        }

        routes.Sort(CompareRoutes);
        DetectAmbiguousRoutes(routes);
        return [.. routes];
    }

    private static void DetectAmbiguousRoutes(List<RouteDefinition> routes)
    {
        for (var i = 1; i < routes.Count; i++)
        {
            var x = routes[i - 1];
            var y = routes[i];
            if (CompareRoutes(x, y) == 0)
            {
                throw new InvalidOperationException(
                    $"The following routes are ambiguous:{Environment.NewLine}" +
                    $"'{x.Template}' in '{x.Handler.FullName}'{Environment.NewLine}" +
                    $"'{y.Template}' in '{y.Handler.FullName}'");
            }
        }
    }

    private static string[] GetSegments(string url)
    {
        var pathEnd = url.AsSpan().IndexOfAny('?', '#');
        var path = pathEnd < 0 ? url : url[..pathEnd];
        return path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries)
            .Select(Uri.UnescapeDataString)
            .ToArray();
    }

    private static ParsedTemplate ParseTemplate(string template)
    {
        var trimmedTemplate = template.Trim('/');
        if (trimmedTemplate.Length == 0)
        {
            return new ParsedTemplate(template, []);
        }

        var parts = trimmedTemplate.Split('/');
        var segments = new RouteSegment[parts.Length];
        for (var i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            if (part.Length == 0)
            {
                throw new InvalidOperationException($"Invalid template '{template}'. Empty segments are not allowed.");
            }

            segments[i] = part[0] == '{'
                ? ParseParameterSegment(template, part)
                : ParseLiteralSegment(template, part);
        }

        ValidateSegments(template, segments);
        return new ParsedTemplate(template, segments);
    }

    private static RouteSegment ParseLiteralSegment(string template, string segment)
    {
        if (segment[^1] == '}')
        {
            throw new InvalidOperationException(
                $"Invalid template '{template}'. Missing '{{' in parameter segment '{segment}'.");
        }

        if (segment[^1] == '?')
        {
            throw new InvalidOperationException(
                $"Invalid template '{template}'. '?' is not allowed in literal segment '{segment}'.");
        }

        return new RouteSegment(segment, false, false, false, []);
    }

    private static RouteSegment ParseParameterSegment(string template, string segment)
    {
        if (segment[^1] != '}')
        {
            throw new InvalidOperationException(
                $"Invalid template '{template}'. Missing '}}' in parameter segment '{segment}'.");
        }

        if (segment.Length < 3)
        {
            throw new InvalidOperationException(
                $"Invalid template '{template}'. Empty parameter name in segment '{segment}' is not allowed.");
        }

        var value = segment[1..^1];
        var isCatchAll = value.StartsWith('*');
        if (isCatchAll)
        {
            value = value[1..];
            if (value.Contains('*'))
            {
                throw new InvalidOperationException(
                    $"Invalid template '{template}'. A catch-all parameter may only have one '*' at the beginning of the segment.");
            }
        }

        var tokens = value.Split(':');
        var name = tokens[0];
        var isOptional = tokens[^1].EndsWith('?');
        if (isOptional)
        {
            tokens[^1] = tokens[^1][..^1];
            if (tokens.Length == 1)
            {
                name = tokens[0];
            }
        }

        if (name.Length == 0)
        {
            throw new ArgumentException(
                $"Malformed parameter '{segment}' in route '{template}' has no name before the constraints list.");
        }

        if (name.IndexOfAny(['{', '}', '=', '.', '?']) >= 0)
        {
            throw new InvalidOperationException(
                $"Invalid template '{template}'. The parameter name '{name}' contains an invalid character.");
        }

        if (isOptional && isCatchAll)
        {
            throw new InvalidOperationException(
                $"Invalid segment '{segment}' in route '{template}'. A catch-all parameter cannot be marked optional.");
        }

        var constraints = new RouteValueConverter[tokens.Length - 1];
        for (var i = 1; i < tokens.Length; i++)
        {
            constraints[i - 1] = RouteValueConverter.GetRouteConstraint(template, segment, tokens[i]);
        }

        return new RouteSegment(name, true, isOptional, isCatchAll, constraints);
    }

    private static void ValidateSegments(string template, RouteSegment[] segments)
    {
        var parameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < segments.Length; i++)
        {
            var segment = segments[i];
            if (segment.IsCatchAll && i != segments.Length - 1)
            {
                throw new InvalidOperationException(
                    $"Invalid template '{template}'. A catch-all parameter can only appear as the last segment of the route template.");
            }

            if (!segment.IsParameter)
            {
                continue;
            }

            if (!parameterNames.Add(segment.Value))
            {
                throw new InvalidOperationException(
                    $"Invalid template '{template}'. The parameter '{segment.Value}' appears multiple times.");
            }

            if (segment.IsOptional && segments[(i + 1)..].Any(next => !next.IsOptional && !next.IsCatchAll))
            {
                throw new InvalidOperationException(
                    $"Invalid template '{template}'. Non-optional parameters or literal routes cannot appear after optional parameters.");
            }
        }
    }

    private static int CompareRoutes(RouteDefinition x, RouteDefinition y)
    {
        var minSegments = Math.Min(x.Segments.Length, y.Segments.Length);
        for (var i = 0; i < minSegments; i++)
        {
            var xSegment = x.Segments[i];
            var ySegment = y.Segments[i];
            var result = GetRank(xSegment).CompareTo(GetRank(ySegment));
            if (result == 0 && !xSegment.IsParameter)
            {
                result = StringComparer.OrdinalIgnoreCase.Compare(xSegment.Value, ySegment.Value);
            }

            if (result != 0)
            {
                return result;
            }
        }

        return x.Segments.Length.CompareTo(y.Segments.Length);
    }

    private static int GetRank(RouteSegment segment) => segment switch
    {
        { IsParameter: false } => 0,
        { IsCatchAll: false, Constraints.Length: > 0 } => 1,
        { IsCatchAll: false } => 2,
        { IsCatchAll: true, Constraints.Length: > 0 } => 3,
        _ => 4
    };

    private sealed record ParsedTemplate(string Template, RouteSegment[] Segments);

    private sealed record RouteSegment(
        string Value,
        bool IsParameter,
        bool IsOptional,
        bool IsCatchAll,
        RouteValueConverter[] Constraints)
    {
        public bool TryMatch(string pathSegment, out object? value)
        {
            value = IsParameter ? pathSegment : null;
            if (!IsParameter)
            {
                return string.Equals(Value, pathSegment, StringComparison.OrdinalIgnoreCase);
            }

            foreach (var constraint in Constraints)
            {
                if (!constraint.TryParse(pathSegment, out value))
                {
                    return false;
                }
            }

            return true;
        }
    }

    private sealed record RouteDefinition(
        string Template,
        RouteSegment[] Segments,
        [property: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type Handler,
        string[] UnusedParameterNames)
    {
        public bool TryMatch(string[] pathSegments, out IReadOnlyDictionary<string, object?>? parameters)
        {
            var values = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            var pathIndex = 0;
            var templateIndex = 0;

            while (pathIndex < pathSegments.Length && templateIndex < Segments.Length)
            {
                var segment = Segments[templateIndex];
                if (!segment.TryMatch(pathSegments[pathIndex], out var value))
                {
                    parameters = null;
                    return false;
                }

                if (!segment.IsCatchAll)
                {
                    if (segment.IsParameter)
                    {
                        values[segment.Value] = value;
                    }

                    pathIndex++;
                    templateIndex++;
                }
                else if (segment.Constraints.Length == 0)
                {
                    values[segment.Value] = string.Join('/', pathSegments, pathIndex, pathSegments.Length - pathIndex);
                    pathIndex = pathSegments.Length;
                    templateIndex++;
                }
                else
                {
                    pathIndex++;
                    if (pathIndex == pathSegments.Length)
                    {
                        values[segment.Value] = string.Join('/', pathSegments, templateIndex, pathSegments.Length - templateIndex);
                        templateIndex++;
                    }
                }
            }

            var remainingSegmentsAreOptional = templateIndex < Segments.Length &&
                Segments[templateIndex..].All(segment => segment.IsOptional || segment.IsCatchAll);
            if (pathIndex != pathSegments.Length ||
                (templateIndex != Segments.Length && !remainingSegmentsAreOptional))
            {
                parameters = null;
                return false;
            }

            if (remainingSegmentsAreOptional)
            {
                foreach (var segment in Segments[templateIndex..])
                {
                    values[segment.Value] = null;
                }
            }

            foreach (var name in UnusedParameterNames)
            {
                values[name] = null;
            }

            parameters = values.Count == 0 ? null : values;
            return true;
        }
    }
}
