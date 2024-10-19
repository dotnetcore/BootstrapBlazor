import Data from "./data.js"

export async function init(id) {
    Data.set(id, { serialPort: null });
    return navigator.bluetooth !== void 0;
}

export async function getAvailability(id) {
    Data.set(id, {});
    let ret = false;
    try {
        if (navigator.bluetooth) {
            ret = await navigator.bluetooth.getAvailability();
        }
    }
    catch (err) {
        console.error(err);
    }
    return ret;
}

export async function requestDevice(id) {
    let device = null;
    try {
        if (navigator.bluetooth) {
            const ret = await navigator.bluetooth.requestDevice({
                acceptAllDevices: true,
                optionalServices: [
                    0x180D,
                    0x180F
                ]
            });
            device = { name: ret.name, id: ret.id };

            const bt = Data.get(id);
            bt.device = ret;
        }
    }
    catch (err) {
        console.error(err);
    }
    return device;
}

export async function getDevices(id) {
    let ret = false;
    try {
        if (navigator.bluetooth) {
            ret = await navigator.bluetooth.getDevices();
        }
    }
    catch (err) {
        console.error(err);
    }
    return ret;
}

export async function connect(id) {
    let ret = false;
    try {
        if (navigator.bluetooth) {
            const bt = Data.get(id);
            const { device } = bt;
            if (device.gatt.connected === false) {
                await device.gatt.connect();
            }
            ret = true;
        }
    }
    catch (err) {
        console.error(err);
    }
    return ret;
}

export async function getBatteryValue(id) {
    let ret = null;
    try {
        const bt = Data.get(id);
        const { device } = bt;
        const gattServer = device.gatt;
        const server = await gattServer.getPrimaryService('battery_service');
        const characters = await server.getCharacteristics('battery_level');
        if(characters.length > 0) {
            const uuid = characters[0].uuid;
            const characteristic = await server.getCharacteristic(uuid);
            const v = await characteristic.readValue();
            ret = `${v.getUint8(0)}%`;
        }
    }
    catch (err) {
        console.error(err);
    }
    return ret;
}

export async function disconnect(id) {
    let ret = false;
    try {
        if (navigator.bluetooth) {
            const bt = Data.get(id);
            const { device } = bt;
            if (device.gatt.connected === true) {
                device.gatt.disconnect();
            }
            ret = true;
        }
    }
    catch (err) {
        console.error(err);
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
