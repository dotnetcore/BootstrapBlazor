import Data from "./data.js"

export async function init() {
    return navigator.bluetooth !== void 0;
}

export async function getAvailability() {
    let ret = false;
    if (navigator.bluetooth) {
        ret = await navigator.bluetooth.getAvailability();
    }
    return ret;
}

export async function requestDevice(id, options, invoke, method) {
    let ret = await getAvailability();
    if (ret === false) {
        return null;
    }

    let device = null;
    const bt = { device: null };
    Data.set(id, bt);
    try {
        const ret = await navigator.bluetooth.requestDevice(options ?? {
            acceptAllDevices: true
        });
        bt.device = ret;
        device = [ret.name, ret.id];
    }
    catch (err) {
        invoke.invokeMethodAsync(method, err.toString());
        console.log(err);
    }
    return device;
}

export async function connect(id, invoke, method) {
    let ret = false;
    const bt = Data.get(id);
    if (bt === null) {
        return ret;
    }

    try {
        const { device } = bt;
        if (device.gatt.connected === false) {
            await device.gatt.connect();
        }
        ret = true;
    }
    catch (err) {
        invoke.invokeMethodAsync(method, err.toString());
        console.log(err);
    }
    return ret;
}

export async function readValue(id, serviceName, characteristicName, invoke, method) {
    let ret = null;
    const bt = Data.get(id);
    if (bt === null) {
        return ret;
    }

    try {
        const { device } = bt;
        const server = device.gatt;
        if (server.connected === false) {
            await server.connect();
        }
        const service = await server.getPrimaryService(serviceName);
        const characteristic = await service.getCharacteristic(characteristicName);
        const dv = await characteristic.readValue();
        ret = new Uint8Array(dv.byteLength);
        for (let index = 0; index < dv.byteLength; index++) {
            ret[index] = dv.getUint8(index);
        }
    }
    catch (err) {
        invoke.invokeMethodAsync(method, err.toString());
        console.log(err);
    }
    return ret;
}

export async function disconnect(id, invoke, method) {
    let ret = false;
    const bt = Data.get(id);
    if (bt === null) {
        return ret;
    }

    try {
        const { device } = bt;
        if (device.gatt.connected === true) {
            device.gatt.disconnect();
        }
        ret = true;
    }
    catch (err) {
        invoke.invokeMethodAsync(method, err.toString());
        console.log(err);
    }
    return ret;
}

export async function dispose(id) {
    await disconnect(id);
    Data.remove(id);
}
