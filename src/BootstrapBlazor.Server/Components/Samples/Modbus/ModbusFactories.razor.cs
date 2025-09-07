// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Longbow.Modbus;

namespace BootstrapBlazor.Server.Components.Samples.Modbus;

/// <summary>
/// IModbusFactory 服务说明文档
/// </summary>
public partial class ModbusFactories
{
    [Inject, NotNull]
    private IModbusFactory? ModbusFactory { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var client  = ModbusFactory.RemoveTcpMaster("test");

        client.
    }
}
