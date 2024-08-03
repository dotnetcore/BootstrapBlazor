import { driver } from "../../lib/driver/driver.js"
import { addLink } from '../../modules/utility.js'
import Data from "../../modules/data.js"

export async function init(id, invoke) {
    await addLink('./_content/BootstrapBlazor/lib/driver/driver.css')

    Data.set(id, { invoke });
}

export function start(id, options) {
    const d = Data.get(id);
    if (d) {
        const driverObj = driver(options);
        d.driver = driverObj;
        driverObj.drive();
    }
}

export function dispose(id) {
    const d = Data.get(id);;
    Data.remove(id);

    if (d && d.driver) {
        d.driver.destroy();
    }
}
