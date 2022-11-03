export class Ajax {
    static async execute(option) {
        option = {
            method: 'POST',
            url: null,
            data: null,
            ...option
        }

        if (option.url === null)
        {
            window.error('url not allow null')
            return null
        }

        const response = await fetch(option.url, {
            method: option.method,
            body: JSON.stringify(option.data),
            headers: {
                'Content-Type': 'application/json'
            }
        })
        const json = await response.json()
        return JSON.stringify(json)
    }

    static goto(url) {
        window.location.href = url
    }
}
