// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContentRoot 转换器
/// </summary>
class DockViewContentRootConverter : JsonConverter<List<IDockViewComponent>>
{
    public override List<IDockViewComponent>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<IDockViewComponent> value, JsonSerializerOptions options)
    {
        if (value.Count > 0)
        {
            var converter = new DockViewComponentConverter();
            converter.Write(writer, value, options);
        }
    }
}
