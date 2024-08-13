import { driver } from "../driver.js"
import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import Data from "../../BootstrapBlazor/modules/data.js"

export async function init(id, invoke) {
    await addLink('./_content/BootstrapBlazor.DriverJs/driver.css')

    Data.set(id, { invoke });
}

export function start(id, options, config) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const d = Data.get(id);
    if (d) {
        d.config = config;
        const { autoDrive, index } = config;
        const { hookDestroyStarted, hookDestroyed } = options;
        if (hookDestroyStarted) {
            delete options.hookDestroyStarted;
            options.onDestroyStarted = async (el, step, { state }) => {
                const content = await d.invoke.invokeMethodAsync("OnBeforeDestroy", state.activeIndex);
                if (content === null || confirm(content)) {
                    d.driver.destroy();
                }
            }
        }
        if (hookDestroyed) {
            delete options.hookDestroyed;
            options.onDestroyed = () => {
                d.invoke.invokeMethodAsync("OnDestroyed");
            };
        }
        const driverObj = driver(options);
        d.driver = driverObj;

        if (autoDrive) {
            driverObj.drive(index);
        }
    }
}

export function dispose(id) {
    const d = Data.get(id);
    Data.remove(id);

    if (d && d.driver) {
        d.driver.destroy();
    }
}

export function drive(id, index = 0) {
    const d = Data.get(id);
    if (d) {
        d.driver.drive(index);
    }
}

export function moveNext(id) {
    const d = Data.get(id);
    if (d) {
        d.driver.moveNext();
    }
}

export function movePrevious(id) {
    const d = Data.get(id);
    if (d) {
        d.driver.movePrevious();
    }
}

export function moveTo(id, index = 0) {
    const d = Data.get(id);
    if (d) {
        d.driver.moveTo(index);
    }
}

export function hasNextStep(id) {
    let ret = false;
    const d = Data.get(id);
    if (d) {
        d.driver.hasNextStep();
    }
    return ret;
}

export function hasPreviousStep(id) {
    let ret = false;
    const d = Data.get(id);
    if (d) {
        d.driver.hasPreviousStep();
    }
    return ret;
}

export function isFirstStep(id) {
    let ret = false;
    const d = Data.get(id);
    if (d) {
        ret = d.driver.isFirstStep();
    }
    return ret;
}

export function isLastStep(id) {
    let ret = false;
    const d = Data.get(id);
    if (d) {
        ret = d.driver.isLastStep();
    }
    return ret;
}

export function getActiveIndex(id) {
    let index = -1;
    const d = Data.get(id);
    if (d) {
        index = d.driver.getActiveIndex();
    }
    return index;
}

export function isActive(id) {
    let ret = false;
    const d = Data.get(id);
    if (d) {
        ret = d.driver.isActive();
    }
    return ret;
}

export function refresh(id) {
    const d = Data.get(id);
    if (d) {
        d.driver.refresh();
    }
}

export function highlight(id, options, config) {
    const driverObj = driver(options);
    driverObj.highlight(config);
}
