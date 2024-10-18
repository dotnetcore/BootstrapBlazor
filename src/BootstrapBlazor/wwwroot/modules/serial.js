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

export function dispose(id) {

}
