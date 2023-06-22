import Data from '../../../BootstrapBlazor/modules/data.js'
import { addLink, addScript } from "../../modules/utility.js?v=$version"

await addLink("_content/BootstrapBlazor/lib/mouseFollower/mouse-follower.min.css")
await addScript("_content/BootstrapBlazor/lib/mouseFollower/gsap.min.js");
await addScript("_content/BootstrapBlazor/lib/mouseFollower/mouse-follower.min.js");

export async function init(globalMode, followerElement, container, options) {

    if (globalMode) {
        options.container = document.body;
    } else {
        options.container = container;
    }

    options.el = followerElement;

    const cursor = new MouseFollower(options);

    container.addEventListener('mouseenter', () => {
        cursor.show();
        if (options.text) {
            cursor.setText(options.text);
        }
    });

    container.addEventListener('mouseleave', () => {
        cursor.removeText();
        cursor.hide();
    });

    Data.set(container, cursor)
}

//Destroy the cursor completely and remove all event listeners.
export function destory(container) {
    const cursor = Data.get(container)
    Data.remove(container)
    cursor.destroy();
}
