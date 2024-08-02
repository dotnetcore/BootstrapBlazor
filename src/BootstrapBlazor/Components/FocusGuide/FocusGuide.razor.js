import { driver } from "../../lib/driver/driver.js"
import { addLink } from '../../modules/utility.js'
import Data from "../../modules/data.js"

export async function init(id) {
    await addLink('./_content/BootstrapBlazor/lib/driver/driver.css')

}

export function start(id) {
    const driverObj = driver({
        showProgress: true,
        steps: [
            { element: '.bb-guid1', popover: { title: 'Animated Tour Example', description: 'Here is the code example showing animated tour. Let\'s walk you through it.', side: "left", align: 'start' } },
            { element: '.bb-guid1 .input-group', popover: { title: 'Import the Library', description: 'It works the same in vanilla JavaScript as well as frameworks.', side: "bottom", align: 'start' } },
            { element: '.bb-guid1 .input-group .form-control', popover: { title: 'Importing CSS', description: 'Import the CSS which gives you the default styling for popover and overlay.', side: "bottom", align: 'start' } },
            { element: '.bb-guid1 .input-group button', popover: { title: 'Create Driver', description: 'Simply call the driver function to create a driver.js instance', side: "left", align: 'start' } }
        ]
    });
    Data.set(id, driverObj);

    driverObj.drive();
}

export function dispose(id) {
    const driverObj = Data.get(id);;
    Data.remove(id);

    if (driverObj) {
        driverObj.destroy();
    }
}
