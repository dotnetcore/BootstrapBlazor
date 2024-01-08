let canvas, ctx, lineThickness, drawingColor, x, y, savedImageData;
let isPressed = false;

function drawCircle(ctx, x, y) {
    ctx.beginPath();
    ctx.arc(x, y, lineThickness, 0, Math.PI * 2);
    ctx.fillStyle = drawingColor;
    ctx.fill();
}

function drawLine(ctx, x1, y1, x2, y2) {
    ctx.beginPath();
    ctx.moveTo(x1, y1);
    ctx.lineTo(x2, y2);
    ctx.strokeStyle = drawingColor;
    ctx.lineWidth = lineThickness * 2;
    ctx.stroke();
}

function draw(x2, y2) {
    drawCircle(ctx, x2, y2);
    drawLine(ctx, x, y, x2, y2);

    x = x2;
    y = y2;
    savedImageData = canvas.toDataURL();
}

function isMobileDevice() {
    return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
}

function handleTouchStart(e) {
    e.preventDefault();
    isPressed = true;

    x = e.touches[0].clientX - canvas.offsetLeft;
    y = e.touches[0].clientY - canvas.offsetTop;
}

function handleTouchMove(e) {
    if (isPressed && ctx) {
        const x2 = e.touches[0].clientX - canvas.offsetLeft;
        const y2 = e.touches[0].clientY - canvas.offsetTop;
        draw(x2, y2);
    }
}

function handleMouseDown(e) {
    isPressed = true;
    x = e.offsetX;
    y = e.offsetY;
}

function handlePressed(e) {
    isPressed = false;
    x = undefined;
    y = undefined;
}

function handleMouseMove(e) {
    if (isPressed && ctx) {
        const x2 = e.offsetX;
        const y2 = e.offsetY;

        draw(x2, y2);
    }
}

export const changeSize = (val) => {
    lineThickness = val;
}

export const changeColor = (val) => {
    drawingColor = val;
}

export const clearRect = () => {
    if (canvas) {
        savedImageData = canvas.toDataURL();
        const ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }
}

export const exportImage = () => {
    if (canvas) {
        return canvas.toDataURL('image/jpeg');
    }
}

export const init = (id, thickness, color) => {
    lineThickness = thickness;
    drawingColor = color;

    canvas = document.getElementById(id);
    if (!canvas) {
        console.error('Canvas element not found');
        return;
    }

    const style = getComputedStyle(canvas);
    canvas.width = parseInt(style.width, 10);
    canvas.height = parseInt(style.height, 10);
    ctx = canvas.getContext('2d');

    if (isMobileDevice()) {
        canvas.addEventListener('touchstart', handleTouchStart);
        canvas.addEventListener('touchmove', handleTouchMove);
        canvas.addEventListener('touchend', handlePressed);
    } else {
        window.addEventListener('resize', () => {
            const style = getComputedStyle(canvas);
            const width = parseInt(style.width, 10);
            const height = parseInt(style.height, 10);

            canvas.width = width;
            canvas.height = height;

            if (savedImageData) {
                const img = new Image();
                img.src = savedImageData;
                img.onload = () => {
                    ctx.drawImage(img, 0, 0);
                };
            }
        });

        canvas.addEventListener('mousedown', handleMouseDown);
        canvas.addEventListener('mouseup', handlePressed);
        canvas.addEventListener('mousemove', handleMouseMove);
    }
};
