// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace BootstrapBlazor.Components;

static class JsonElementExtensions
{
    extension(JsonElement element)
    {
        public T? Parse<T>(JsonSerializerDefaults options = JsonSerializerDefaults.Web)
        {
            try
            {
                return element.Deserialize<T>(new JsonSerializerOptions(options));
            }
            catch
            {
                return default;
            }
        }
    }
}

