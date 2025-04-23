export async function execute(option) {
    option = {
        method: 'POST',
        url: null,
        data: null,
        toJson: true,
        headers: { 'Content-Type': 'application/json; charset=utf-8' },
        ...option
    }

    if (option.url === null) {
        window.error('url not allow null')
        return null
    }

    let result = null;
    try {
        const { toJson, url, method, data, headers } = option;
        if (headers['Content-Type'] === void 0) {
            headers['Content-Type'] = 'application/json; charset=utf-8'
        }
        const contentType = headers['Content-Type'];
        let postData = JSON.stringify(data);
        if (contentType.indexOf('application/x-www-form-urlencoded') > -1) {
            postData = new URLSearchParams(data).toString();
        }
        result = await fetch(url, {
            method: method,
            headers: headers,
            body: postData
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
