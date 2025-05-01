export async function enumerateDevices() {
    let ret = null;
    if (!navigator.mediaDevices || !navigator.mediaDevices.enumerateDevices) {
        console.log("enumerateDevices() not supported.");
    }
    else {
        const devices = await navigator.mediaDevices.enumerateDevices();
        ret = devices;
    }
    return ret;
}
