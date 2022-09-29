// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System.Dynamic;

namespace BootstrapBlazor.Shared.Samples.Table;

public partial class TablesDynamicObject
{
    private readonly string[] DynamicList = new[] { "A", "B", "C", "Z" };

    private readonly List<NetDynamic> netDynamics;
    private readonly List<BBDynamic> bbDynamics;

    public DynamicTableSample()
    {
        const int count = 10;
        netDynamics = Enumerable.Range(1, count)
            .Select(e => new NetDynamic(
                e.ToString(),
                DynamicList.ToDictionary(d => d, d => d.ToString() + e)))
            .ToList();
        bbDynamics = Enumerable.Range(1, count)
            .Select(e => new BBDynamic(
                e.ToString(),
                DynamicList.ToDictionary(d => d, d => d.ToString() + e)))
            .ToList();
    }

    public class BBDynamic : IDynamicObject
    {
        public string Fix { get; set; } = "";

        public Dictionary<string, string> Dynamic { get; set; }

        public Guid DynamicObjectPrimaryKey { get => new(Fix); set { } }

        public BBDynamic(string fix, Dictionary<string, string> data)
        {
            Fix = fix;
            Dynamic = data;
        }

        public BBDynamic() : this("", new()) { }

        public object? GetValue(string propertyName)
        {
            if (propertyName == nameof(Fix))
                return Fix;
            return Dynamic.TryGetValue(propertyName, out string? v) ? v : "";
        }

        public void SetValue(string propertyName, object? value)
        {
            if (value is not string str)
                return;
            if (propertyName == nameof(Fix))
                Fix = str;
            Dynamic[propertyName] = str;
        }
    }

    public class NetDynamic : System.Dynamic.DynamicObject
    {
        public string Fix { get; set; } = "";

        public Dictionary<string, string> Dynamic { get; set; }

        public NetDynamic(string fix, Dictionary<string, string> data)
        {
            Fix = fix;
            Dynamic = data;
        }

        public NetDynamic() : this("", new()) { }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (binder.Name == nameof(Fix))
            {
                result = Fix;
                return true;
            }
            else if (Dynamic.ContainsKey(binder.Name))
            {
                result = Dynamic[binder.Name];
                return true;
            }
            else
            {
                // When property name not found, return empty
                result = "";
                return true;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (value is not string str)
            {
                return false;
            }
            else if (binder.Name == nameof(Fix))
            {
                Fix = str;
                return true;
            }
            else if (Dynamic.ContainsKey(binder.Name))
            {
                Dynamic[binder.Name] = str;
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
