export async function execute(option) {
    option = {
        method: 'POST',
        url: null,
        data: null,
        toJson: true,
        ...option
    }

    if (option.url === null) {
        window.error('url not allow null')
        return null
    }

    let result = null;
    try {
        const { toJson, url, method, data } = option;
        result = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: method === 'POST' ? JSON.stringify(data) : null
        });
        if (toJson === true) {
            result = await result.json()
        }
    }
    catch (e) {
        console.info(e);
    }
    return result
}

export function goto(url) {
    window.location.href = url
}
