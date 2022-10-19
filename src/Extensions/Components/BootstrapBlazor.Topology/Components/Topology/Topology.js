import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"

export class BlazorTopology extends BlazorComponent {
    _init() {
        if (window.Topology) {
            Topology.prototype.lock = function (status) {
                this.store.data.locked = status
                this.finishDrawLine(!0)
                this.canvas.drawingLineName = ""
                this.stopPencil()
            }

            const obj = this._config.arguments[0]
            const data = this._config.arguments[1]
            const method = this._config.arguments[2]

            this._topology = new Topology(this._element.getAttribute('id'))
            this._topology.connectSocket = function () {
            }
            this._topology.open(JSON.parse(data))
            this._topology.lock(1)
            obj.invokeMethodAsync(method)
        }
    }

    _execute(args) {
        const data = args[1];
        this._topology.doSocket(JSON.stringify(data))
    }
}
