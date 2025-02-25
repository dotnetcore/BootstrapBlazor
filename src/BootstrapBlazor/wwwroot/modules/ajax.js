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
        const toJson = option.toJson;
        delete option.toJson;
        result = await fetch(option.url, {
            method: option.method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: option.method === 'POST' ? JSON.stringify(option.data) : null
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
