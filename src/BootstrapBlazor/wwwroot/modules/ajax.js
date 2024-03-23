export async function execute(option) {
    option = {
        method: 'POST',
        url: null,
        data: null,
        ...option
    }

    if (option.url === null) {
        window.error('url not allow null')
        return null
    }

    const init = {
        method: option.method,
        headers: {
            'Content-Type': 'application/json'
        }
    }

    if (option.method === 'POST' && option.data) {
        init.body = JSON.stringify(option.data)
    }

    let json = null;
    try {

        const response = await fetch(option.url, init)
        json = await response.json()
    }
    catch (e) {
        console.info(e);
    }
    return json
}

export function goto(url) {
    window.location.href = url
}
