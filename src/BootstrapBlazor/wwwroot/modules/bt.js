import Data from "./data.js"

export async function init() {
    return navigator.bluetooth !== void 0;
}

export async function getAvailability(id) {
    Data.set(id, {});
    let ret = false;
    if (navigator.bluetooth) {
        ret = await navigator.bluetooth.getAvailability();
    }
    return ret;
}

export async function requestDevice(id, optionalServices, invoke, method) {
    let device = null;
    const bt = Data.get(id);
    if (bt === null) {
        return device;
    }

    try {
        const ret = await navigator.bluetooth.requestDevice({
            acceptAllDevices: true,
            optionalServices: optionalServices
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
        const gattServer = device.gatt;
        const server = await gattServer.getPrimaryService(serviceName);
        const characteristic = await server.getCharacteristic(characteristicName);
        const dv = await characteristic.readValue();
        ret = dv;
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
