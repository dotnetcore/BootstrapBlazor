import Data from "./data.js"

export async function init(id) {
    Data.set(id, { serialPort: null });
    return navigator.serial !== void 0;
}

export async function getPort(id) {
    let ret = false;
    try {
        if (navigator.serial) {
            close(id);
            const serialPort = await navigator.serial.requestPort();
            const data = Data.get(id);
            data.serialPort = serialPort;
            ret = true;
        }
    }
    catch (err) {
        console.error(err);
    }
    return ret;
}

export async function open(id, invoke, method, options) {
    let ret = false;
    const serial = Data.get(id);
    const { serialPort } = serial;
    if (serialPort !== null) {
        try {
            await close(id);
            await serialPort.open(options);
            read(serial, invoke, method);
            ret = true;
        }
        catch (err) {
            console.error(err);
        }
    }
    return ret;
}

export async function close(id) {
    let ret = false;
    const serial = Data.get(id);
    const { reader, serialPort } = serial;
    if (serialPort !== null) {
        try {
            if (reader) {
                await reader.cancel();
                delete serial.reader;
            }
            if (serialPort.readable || serialPort.writable) {
                await serialPort.close();
            }
            ret = true;
        }
        catch (err) {
            console.error(err);
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
                invoke.invokeMethodAsync(method, value);
            }
        } catch (error) {
            console.error(error);
        } finally {
            serial.reader.releaseLock()
        }
    }
}

export async function write(id, data) {
    let ret = false;
    const serial = Data.get(id);
    const { serialPort } = serial;
    if (serialPort && serialPort.writable) {
        const writer = serialPort.writable.getWriter()
        const payload = new Uint8Array([...data])
        await writer.write(payload)
        writer.releaseLock();
        ret = true;
    }
    return ret;
}

export async function getInfo(id) {
    const serial = Data.get(id);
    const { serialPort } = serial;
    if (serialPort) {
        const info = await serialPort.getInfo();
        console.log(info);
    }
}

export async function getSignals(id) {
    const serial = Data.get(id);
    const { serialPort } = serial;
    if (serialPort) {
        const info = await serialPort.getSignals();
        return info;
    }
}

export async function setSignals(id, options) {
    const serial = Data.get(id);
    const { serialPort } = serial;
    if (serialPort) {
        const info = await serialPort.setSignals(options);
        return info;
    }
}

export async function dispose(id) {
    await close(id);
    Data.remove(id);
}
