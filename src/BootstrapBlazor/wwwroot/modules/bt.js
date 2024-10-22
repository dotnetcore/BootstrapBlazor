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

export async function getDeviceInfo(id, invoke, method) {
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
        const services = await server.getPrimaryServices();
        console.log(services);

        const service = await server.getPrimaryService('device_information');
        const characteristics = await service.getCharacteristics();
        const decoder = new TextDecoder('utf-8');
        ret = {};
        let dv = null;
        for (const characteristic of characteristics) {
            switch (characteristic.uuid) {
                case BluetoothUUID.getCharacteristic('manufacturer_name_string'):
                    dv = await characteristic.readValue();
                    ret.ManufacturerName = decoder.decode(dv);
                    break;

                case BluetoothUUID.getCharacteristic('model_number_string'):
                    dv = await characteristic.readValue();
                    ret.ModelNumber = decoder.decode(dv);
                    break;

                case BluetoothUUID.getCharacteristic('hardware_revision_string'):
                    dv = await characteristic.readValue();
                    ret.HardwareRevision = decoder.decode(dv);
                    break;

                case BluetoothUUID.getCharacteristic('firmware_revision_string'):
                    dv = await characteristic.readValue();
                    ret.FirmwareRevision = decoder.decode(dv);
                    break;

                case BluetoothUUID.getCharacteristic('software_revision_string'):
                    dv = await characteristic.readValue();
                    ret.SoftwareRevision = decoder.decode(dv);
                    break;

                case BluetoothUUID.getCharacteristic('system_id'):
                    dv = await characteristic.readValue();
                    ret.SystemId = {
                        ManufacturerIdentifier: padHex(dv.getUint8(4)) + padHex(dv.getUint8(3)) +
                            padHex(dv.getUint8(2)) + padHex(dv.getUint8(1)) +
                            padHex(dv.getUint8(0)),
                        OrganizationallyUniqueIdentifier: padHex(dv.getUint8(7)) + padHex(dv.getUint8(6)) +
                            padHex(dv.getUint8(5))
                    }
                    break;

                case BluetoothUUID.getCharacteristic('ieee_11073-20601_regulatory_certification_data_list'):
                    dv = await characteristic.readValue();
                    ret.IEEERegulatoryCertificationDataList = decoder.decode(dv);
                    break;

                case BluetoothUUID.getCharacteristic('pnp_id'):
                    dv = await characteristic.readValue();
                    ret.PnPID = {
                        VendorIdSource: dv.getUint8(0) === 1 ? 'Bluetooth' : 'USB',
                        ProductId: dv.getUint8(3) | dv.getUint8(4) << 8,
                        ProductVersion: dv.getUint8(5) | dv.getUint8(6) << 8,
                    }
                    break;

                default:
                    console.log('Unknown Characteristic: ' + characteristic.uuid);
            }
        }
    }
    catch (err) {
        invoke.invokeMethodAsync(method, err.toString());
        console.log(err);
    }
    return ret;
}

const padHex = value => {
    return ('00' + value.toString(16).toUpperCase()).slice(-2);
}

const getUsbVendorName = value => {
    return value + (value in valueToUsbVendorName ? ' (' + valueToUsbVendorName[value] + ')' : '');
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
