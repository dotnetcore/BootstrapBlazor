import Data from "./data.js"

export async function init() {
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

export async function requestDevice(id, optionalServices) {
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
        device = { name: ret.name, id: ret.id };
    }
    catch (err) {
        console.error(err);
    }
    return device;
}

export async function connect(id) {
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
        console.error(err);
    }
    return ret;
}

export async function getBatteryValue(id) {
    let ret = null;
    const bt = Data.get(id);
    if (bt === null) {
        return ret;
    }

    try {
        const { device } = bt;
        const gattServer = device.gatt;
        const server = await gattServer.getPrimaryService('battery_service');
        const characters = await server.getCharacteristics('battery_level');
        if (characters.length > 0) {
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
        console.error(err);
    }
    return ret;
}

export async function dispose(id) {
    await disconnect(id);
    Data.remove(id);
}
