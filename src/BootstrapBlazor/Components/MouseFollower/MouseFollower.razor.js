import { addLink, addScript } from "../../modules/utility.js?v=$version"

export async function init() {
    await addLink("_content/BootstrapBlazor/lib/mouseFollower/mouse-follower.min.css")
    await addScript("_content/BootstrapBlazor/lib/mouseFollower/gsap.min.js");
    await addScript("_content/BootstrapBlazor/lib/mouseFollower/mouse-follower.min.js");

    const cursor = new MouseFollower();
    cursor.show();
}
