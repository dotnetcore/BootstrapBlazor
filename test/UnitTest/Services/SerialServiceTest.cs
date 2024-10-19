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
        Context.JSInterop.Setup<SerialPortUsbInfo>("getInfo", matcher => matcher.Arguments.Count == 1 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(new SerialPortUsbInfo());
        Context.JSInterop.Setup<SerialPortSignals>("getSignals", matcher => matcher.Arguments.Count == 1 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(new SerialPortSignals()
        {
            RING = false,
            CTS = true,
            DCD = true,
            DSR = true
        });
        Context.JSInterop.Setup<bool>("setSignals", matcher => matcher.Arguments.Count == 2 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_serial_") ?? false)).SetResult(true);
        var serialService = Context.Services.GetRequiredService<ISerialService>();

        var serialPort = await serialService.GetPort();
        Assert.True(serialService.IsSupport);
        Assert.NotNull(serialPort);

        // DataReceive
        serialPort.DataReceive = d =>
        {
            return Task.CompletedTask;
        };

        var option = new SerialPortOptions()
        {
            BaudRate = 9600,
            BufferSize = 1024,
            DataBits = 8,
            FlowControlType = SerialPortFlowControlType.Hardware,
            ParityType = SerialPortParityType.Odd,
            StopBits = 1
        };
        await serialPort.Open(option);
        Assert.True(serialPort.IsOpen);
        Assert.Equal(9600, option.BaudRate);
        Assert.Equal(1024, option.BufferSize);
        Assert.Equal(8, option.DataBits);
        Assert.Equal(SerialPortFlowControlType.Hardware, option.FlowControlType);
        Assert.Equal(SerialPortParityType.Odd, option.ParityType);
        Assert.Equal(1, option.StopBits);

        await serialPort.Write([0x31, 0x32]);

        var mi = serialPort.GetType().GetMethod("DataReceiveCallback");
        Assert.NotNull(mi);
        mi.Invoke(serialPort, ["12"u8.ToArray()]);

        // getInfo
        var info = await serialPort.GetUsbInfo();
        Assert.NotNull(info);
        Assert.Null(info.UsbVendorId);
        Assert.Null(info.UsbProductId);

        // getSignals
        var signals = await serialPort.GetSignals();
        Assert.NotNull(signals);
        Assert.False(signals.RING);
        Assert.True(signals.DSR);
        Assert.True(signals.CTS);
        Assert.True(signals.CTS);

        // setSignals
        var signalOption = new SerialPortSignalsOptions()
        {
            Break = true,
            DTR = true,
            RTS = true
        };
        var ret = await serialPort.SetSignals(signalOption);
        Assert.True(ret);
        Assert.True(signalOption.Break);
        Assert.True(signalOption.DTR);
        Assert.True(signalOption.RTS);

        await serialPort.Close();
        Assert.False(serialPort.IsOpen);

        await serialPort.DisposeAsync();
    }
}
