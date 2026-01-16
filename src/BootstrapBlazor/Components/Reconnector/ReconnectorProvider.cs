// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class ReconnectorProvider : IReconnectorProvider
{
    private Action<RenderFragment?, RenderFragment?, RenderFragment?>? _action;

    public void NotifyContentChanged(IReconnector reconnector)
    {
        _action?.Invoke(reconnector.ReconnectingTemplate, reconnector.ReconnectFailedTemplate, reconnector.ReconnectRejectedTemplate);
    }

    public void Register(Action<RenderFragment?, RenderFragment?, RenderFragment?> action) => _action = action;
}
