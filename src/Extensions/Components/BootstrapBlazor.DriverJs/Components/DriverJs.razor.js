import { driver } from "../driver.js"
import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import Data from "../../BootstrapBlazor/modules/data.js"

export async function init(id, invoke) {
    await addLink('./_content/BootstrapBlazor.DriverJs/driver.css')

    Data.set(id, { invoke });
}

export function start(id, options) {
    const el = document.getElementById(id);
    if(el === null) {
        return;
    }

    const d = Data.get(id);
    if (d) {
        const { onDestroyStartedAsync, onDestroyedAsync } = options;
        if (onDestroyStartedAsync) {
            options.onDestroyStarted = async (el, step, { state }) => {
                const content = await d.invoke.invokeMethodAsync(onDestroyStartedAsync, state.activeIndex);
                if (content === null || confirm(content)) {
                    dispose(id);
                }
            }
        }
        if (onDestroyedAsync) {
            options.onDestroyed = () => {
                d.invoke.invokeMethodAsync(onDestroyedAsync);
            };
        }
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
