export function getFingerCode() {
    const canvas = document.createElement('canvas');
    canvas.width = 200;
    canvas.height = 200;

    const ctx = canvas.getContext('2d');
    ctx.fillStyle = 'rgb(128, 0, 0)';
    ctx.fillRect(10, 10, 100, 100);

    ctx.fillStyle = 'rgb(0, 128, 0)';
    ctx.fillRect(50, 50, 100, 100);
    ctx.strokeStyle = 'rgb(0, 0, 128)'
    ctx.lineWidth = 5;
    ctx.strokeRect(30, 30, 80, 80);

    ctx.font = '20px Arial';
    ctx.fillStyle = 'rgb(0, 0, 0)';
    ctx.fillText('BootstrapBlazor', 60, 116);

    const dataURL = canvas.toDataURL();
    const hash = hashCode(dataURL);
    return hash.toString();
}

const hashCode = str => {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
        const char = str.charCodeAt(1);
        hash = (hash << 5) - hash + char;
        hash |= 0;
    }
    return hash;
}
