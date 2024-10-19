// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Services;

public class SerialServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetPort_Ok()
    {
        Context.JSInterop.Setup<bool>("init", matcher => matcher.Arguments.Count == 1 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(true);
        Context.JSInterop.Setup<bool>("getPort", matcher => matcher.Arguments.Count == 1 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(true);
        Context.JSInterop.Setup<bool>("open", matcher => matcher.Arguments.Count == 4 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(true);
        Context.JSInterop.Setup<bool>("close", matcher => matcher.Arguments.Count == 1 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(true);
        Context.JSInterop.Setup<bool>("write", matcher => matcher.Arguments.Count == 2 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(true);
        var serialService = Context.Services.GetRequiredService<ISerialService>();

        var serialPort = await serialService.GetPort();
        Assert.True(serialService.IsSupport);
        Assert.NotNull(serialPort);

        // DataReceive
        serialPort.DataReceive = d =>
        {
            return Task.CompletedTask;
        };

        var option = new SerialOptions()
        {
            BaudRate = 9600,
            BufferSize = 1024,
            DataBits = 8,
            FlowControlType = SerialFlowControlType.Hardware,
            ParityType = SerialParityType.Odd,
            StopBits = 1
        };
        await serialPort.Open(option);
        Assert.True(serialPort.IsOpen);
        Assert.Equal(9600, option.BaudRate);
        Assert.Equal(1024, option.BufferSize);
        Assert.Equal(8, option.DataBits);
        Assert.Equal(SerialFlowControlType.Hardware, option.FlowControlType);
        Assert.Equal(SerialParityType.Odd, option.ParityType);
        Assert.Equal(1, option.StopBits);

        await serialPort.Write([0x31, 0x32]);

        var mi = serialPort.GetType().GetMethod("DataReceiveCallback");
        Assert.NotNull(mi);
        mi.Invoke(serialPort, ["12"u8.ToArray()]);

        await serialPort.Close();
        Assert.False(serialPort.IsOpen);

        await serialPort.DisposeAsync();
    }
}
