export function reconnect() {
    const reconnectHandler = setInterval(async () => {
        const com = document.getElementById('components-reconnect-modal')
        if (com) {
            const cls = com.getAttribute("class");
            if (cls === 'components-reconnect-show') {
                clearInterval(reconnectHandler)

                await attemptReload()
                setInterval(attemptReload, 5000)
            }
        }
    }, 2000)
}

async function attemptReload() {
    await fetch('')
    location.reload()
}
