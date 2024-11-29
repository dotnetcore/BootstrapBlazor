export function play(id) {
    const video = document.getElementById(id);
    video.play();
}

export function pause(id) {
    const video = document.getElementById(id);
    video.pause();
}
