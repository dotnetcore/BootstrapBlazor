import Data from "./data.js"
import EventHandler from "./event-handler.js"

export async function init(id) {
    Data.set(id, { serialPort: null });
    return navigator.serial !== void 0;
}

export async function getPort(id) {
    try {
        const port = await navigator.serial.requestPort();
        close(id);
        const data = Data.get(id);
        data.serialPort = port;
        return true;
    }
    catch {
        return false;
    }
}

export async function open(id) {
    const data = Data.get(id);
    if (data.serialPort !== null) {
        console.log(`open serial port: ${id}`)
    }
}

export async function close(id) {
    const data = Data.get(id);
    if (data.serialPort !== null) {
        console.log(`close serial port: ${id}`)
    }
}

export function dispose(id) {

}
