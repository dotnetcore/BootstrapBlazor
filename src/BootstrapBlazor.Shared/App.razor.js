export function init(error, reload) {
    const errorElement = document.querySelector('#blazor-error-ui > span')
    if (errorElement) {
        errorElement.textContent = error
    }

    const reloadElement = document.querySelector('.reload')
    if (reloadElement) {
        reloadElement.textContent = reload
    }
}
