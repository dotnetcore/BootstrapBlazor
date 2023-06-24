import Data from '../../../BootstrapBlazor/modules/data.js'
import { addLink, addScript } from "../../modules/utility.js?v=$version"

await addLink("_content/BootstrapBlazor/lib/mouseFollower/mouse-follower.min.css")
await addScript("_content/BootstrapBlazor/lib/mouseFollower/gsap.min.js");
await addScript("_content/BootstrapBlazor/lib/mouseFollower/mouse-follower.min.js");

export async function init(globalMode, container, options) {
    options.container = globalMode ? document.body : container;
    const cursor = new MouseFollower(options);
    Data.set(container, cursor);
}

export function SetNormal(container, options) {
    const cursor = Data.get(container);
    container.addEventListener('mouseenter', () => {
        cursor.show();
    });

    container.addEventListener('mouseleave', () => {
        cursor.hide();
    });

    container.addEventListener('mousedown', () => {
        cursor.addState(options.activeState);
    });

    container.addEventListener('mouseup', () => {
        cursor.removeState(options.activeState);
    });

    container.addEventListener('mousemoveOnce', () => {
        cursor.show();
    });
}

export function SetText(container, text) {
    const cursor = Data.get(container);
    container.addEventListener('mouseenter', () => {
        cursor.setText(text);
    });

    container.addEventListener('mouseleave', () => {
        cursor.removeText();
    });
}

export function SetIcon(container, icon) {
    const cursor = Data.get(container);
    container.addEventListener('mouseenter', () => {
        cursor.setIcon(icon);
    });

    container.addEventListener('mouseleave', () => {
        cursor.removeIcon();
    });
}

export function SetImage(container, path) {
    const cursor = Data.get(container);
    container.addEventListener('mouseenter', () => {
        cursor.setImg(path);
    });

    container.addEventListener('mouseleave', () => {
        cursor.removeImg();
    });
}

export function SetVideo(container, path) {
    const cursor = Data.get(container);
    container.addEventListener('mouseenter', () => {
        cursor.setVideo(path);
    });

    container.addEventListener('mouseleave', () => {
        cursor.removeVideo();
    });
}

//Destroy the cursor completely and remove all event listeners.
export function destroy(container) {
    const cursor = Data.get(container);
    Data.remove(container);
    cursor.destroy();
}
