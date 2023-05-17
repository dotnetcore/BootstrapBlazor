export function reconnect(interval) {
    const reconnectHandler = setInterval(async () => {
        const com = document.getElementById('components-reconnect-modal')
        if (com) {
            if (com.classList.length === 0 || com.classList.contains('components-reconnect-hide')) {
                return
            }
            else {
                clearInterval(reconnectHandler)

                async function attemptReload() {
                    try {
                        await fetch('')
                        if (reloadHandler) {
                            clearInterval(reloadHandler)
                        }
                        location.reload()
                    }
                    catch { }
                }
                await attemptReload()
                const reloadHandler = setInterval(attemptReload, interval)
            }
        }
    }, interval)
}
