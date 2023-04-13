// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace System.Text.Json.Serialization;

class ChartLegendPositionConverter : JsonConverter<ChartLegendPosition>
{
    public override ChartLegendPosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ChartLegendPosition value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToDescriptionString());
    }
}
