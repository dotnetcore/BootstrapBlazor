export async function enumerateDevices() {
    let ret = null;
    if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia || !navigator.mediaDevices.enumerateDevices) {
        console.log("enumerateDevices() not supported.");
    }
    else {
        const stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
        console.log(stream);

        const devices = await navigator.mediaDevices.enumerateDevices();
        devices.forEach(d => {
            console.log(d);
        });
        ret = devices;
    }
    return ret;
}
