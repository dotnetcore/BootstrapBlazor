export function scrollToBottom(id) {
    let el = document.getElementById(id);
    const start = el.scrollTop;
    const end = el.scrollHeight;
    const distance = end - start;
    const duration = 500; // 动画持续时间（毫秒）
    const startTime = performance.now();

    function scrollStep(currentTime) {
        const elapsed = currentTime - startTime;
        const progress = Math.min(elapsed / duration, 1); // 确保进度不超过1
        el.scrollTop = start + distance * progress;

        if (progress < 1) {
            window.requestAnimationFrame(scrollStep);
        }
    }

    window.requestAnimationFrame(scrollStep);
}

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {

    }
}
