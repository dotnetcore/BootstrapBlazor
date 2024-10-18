import Data from "./data.js"
import EventHandler from "./event-handler.js"

export async function init(id) {
    Data.set(id, { serialPort: null });
    return navigator.serial !== void 0;
}

export async function getPort(id) {
    let ret = false;
    try {
        const port = await navigator.serial.requestPort();
        close(id);
        const data = Data.get(id);
        data.serialPort = port;
        ret = true;
    }
    catch (err) {
        console.log(err);
    }
    return ret;
}

export async function open(id, options) {
    let ret = false;
    const data = Data.get(id);
    if (data.serialPort !== null) {
        console.log(`open serial port: ${id}`);
        try {
            await data.serialPort.open(options);
            ret = true;
        }
        catch (err) {
            console.log(err);
        }
    }
    return ret;
}

export async function close(id) {
    let ret = false;
    const data = Data.get(id);
    if (data.serialPort !== null) {
        console.log(`close serial port: ${id}`)
        try {
            await data.serialPort.close();
            ret = true;
        }
        catch (err) {
            console.log(err);
        }
    }
    return ret;
}

export async function write(id, data) {
    let ret = false;
    const port = Data.get(id);
    const { serialPort } = port;
    if (serialPort && serialPort.writable) {
        const writer = serialPort.writable.getWriter()
        const payload = new Uint8Array([...data])
        await writer.write(payload)
        writer.releaseLock();
        ret = true;
    }
    return ret;
}

async function writeData(data) {
    if (!serialPort || !serialPort.writable) {
        addLogErr('请先打开串口再发送数据')
        return
    }
    const writer = serialPort.writable.getWriter()
    if (toolOptions.addCRLF) {
        data = new Uint8Array([...data, 0x0d, 0x0a])
    }
    await writer.write(data)
    writer.releaseLock()
    addLog(data, false)
}

export function dispose(id) {

}
