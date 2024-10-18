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

export async function open(id, invoke, method, options) {
    let ret = false;
    const data = Data.get(id);
    if (data.serialPort !== null) {
        console.log(`open serial port: ${id}`);
        try {
            await data.serialPort.open(options);
            read(data, invoke, method);
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
    const { reader, serialPort } = data;
    if (serialPort !== null) {
        console.log(`close serial port: ${id}`)
        try {
            if (reader) {
                await reader.cancel();
            }
            await serialPort.close();
            ret = true;
        }
        catch (err) {
            console.log(err);
        }
    }
    return ret;
}

export async function read(serial, invoke, method) {
    if (invoke === null) {
        return;
    }

    const { serialPort } = serial;
    if (serialPort && serialPort.readable) {
        serial.reader = serialPort.readable.getReader()
        try {
            while (true) {
                const { value, done } = await serial.reader.read();
                if (done) {
                    break
                }
                console.log([...value]);
                invoke.invokeMethodAsync(method, value);
            }
        } catch (error) {
            console.log(error);
        } finally {
            serial.reader.releaseLock()
        }
    }
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

export function dispose(id) {

}
