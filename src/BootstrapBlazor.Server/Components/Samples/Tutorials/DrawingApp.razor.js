let size, color, x, y, savedImageData;
let isPressed = false;

function drawCircle(ctx, x, y) {
    ctx.beginPath();
    ctx.arc(x, y, size, 0, Math.PI * 2);
    ctx.fillStyle = color;
    ctx.fill();
}

function drawLine(ctx, x1, y1, x2, y2) {
    ctx.beginPath();
    ctx.moveTo(x1, y1);
    ctx.lineTo(x2, y2);
    ctx.strokeStyle = color;
    ctx.lineWidth = size * 2;
    ctx.stroke();
}

export const changeSize = (val) => {
    size = val;
}

export const changeColor = (val) => {
    color = val;
}

export const clearRect = (id) => {
    const canvas = document.getElementById(id);
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    savedImageData = canvas.toDataURL();
}

export const exportImage = (id) => {
    const canvas = document.getElementById(id);
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    return canvas.toDataURL('image/jpeg');
}

export const init = (id, lineSize, drawColor) => {
    size = lineSize;
    color = drawColor;

    const canvas = document.getElementById(id);
    const style = getComputedStyle(canvas);
    canvas.width = parseInt(style.width, 10);
    canvas.height = parseInt(style.height, 10);
    const ctx = canvas.getContext('2d');

    window.addEventListener('resize', () => {
        const canvas = document.getElementById(id);
        const style = getComputedStyle(canvas);
        const width = parseInt(style.width, 10);
        const height = parseInt(style.height, 10);

        canvas.width = width;
        canvas.height = height;

        const img = new Image();
        img.src = savedImageData;
        img.onload = function () {
            ctx.drawImage(img, 0, 0);
        };
    });

    canvas.addEventListener('mousedown', (e) => {
        isPressed = true;

        x = e.offsetX;
        y = e.offsetY;
    })

    canvas.addEventListener('mouseup', (e) => {
        isPressed = false;

        x = undefined;
        y = undefined;
    })

    canvas.addEventListener('mousemove', (e) => {
        if (isPressed) {
            const x2 = e.offsetX;
            const y2 = e.offsetY;

            drawCircle(ctx, x2, y2);
            drawLine(ctx, x, y, x2, y2);

            x = x2;
            y = y2;
            savedImageData = canvas.toDataURL();
        }
    })
}
