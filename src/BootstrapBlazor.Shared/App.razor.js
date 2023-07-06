export function init(error, reload) {
    var wasm = document.getElementById('loading')
    if (wasm) {
        wasm.classList.add("is-done")
        var handler = window.setTimeout(function () {
            window.clearTimeout(handler)
            wasm.remove()
            document.body.classList.remove('overflow-hidden')
        }, 600);
    }

    const errorElement = document.querySelector('#blazor-error-ui > span')
    if (errorElement) {
        errorElement.textContent = error
    }

    const reloadElement = document.querySelector('.reload')
    if (reloadElement) {
        reloadElement.textContent = reload
    }
}
