const connections = new Map();

export function init(id, interop) {
    dispose(id);
    const connection = new EventSource(new URL('api/site-notifications', document.baseURI));
    let pending = Promise.resolve();
    connection.onmessage = event => {
        const notification = JSON.parse(event.data);
        pending = pending.then(() => {
            if (connections.get(id) === connection) {
                return interop.invokeMethodAsync('Receive', notification);
            }
        }).catch(error => console.error('Unable to display a site notification.', error));
    };
    connection.onerror = () => console.warn('Site notifications disconnected; the browser will retry automatically.');
    connections.set(id, connection);
}

export function dispose(id) {
    const connection = connections.get(id);
    if (connection) {
        connection.close();
        connections.delete(id);
    }
}
