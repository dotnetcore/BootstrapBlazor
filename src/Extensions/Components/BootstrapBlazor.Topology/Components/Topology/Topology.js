var topology = undefined;

export function init(id, data, method, obj) {
    if (window.Topology) {
        Topology.prototype.lock = function (status) {
            this.store.data.locked = status;
            this.finishDrawLine(!0);
            this.canvas.drawingLineName = "";
            this.stopPencil();
        }

        topology = new Topology(id);
        topology.connectSocket = function () {
        };
        topology.open(JSON.parse(data));
        topology.lock(1);
        obj.invokeMethodAsync(method);
    }
}

export function push_data(id, data) {
    if (topology !== undefined) {
        topology.doSocket(JSON.stringify(data));
    }
}
