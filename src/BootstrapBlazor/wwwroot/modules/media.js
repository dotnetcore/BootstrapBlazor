export async function enumerateDevices() {
    if (!navigator.mediaDevices || !navigator.mediaDevices.enumerateDevices) {
        console.log("enumerateDevices() not supported.");
    }
    else {
        const devices = await navigator.mediaDevices.enumerateDevices();
        devices.forEach(device => {
            console.log(`${device.kind}: ${device.label} id = ${device.deviceId} ${device.groupId}`);
        });
    }
}
