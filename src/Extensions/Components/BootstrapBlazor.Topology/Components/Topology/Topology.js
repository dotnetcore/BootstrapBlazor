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
            const isSupportTouch = this._config.supportTouch === true;
            const isFitView = this._config.fitView === true;
            const isCenterView = this._config.centerView === true;
            this._topology = new Topology(this._element, {}, isSupportTouch)
            this._topology.connectSocket = function () {
            }
            this._topology.open(JSON.parse(data))
            this._topology.lock(1)
            this._topology.canvas.dirty = true
            if (isFitView) {
                this._topology.fitView()
            }
            if (isCenterView) {
                this._topology.centerView()
            }
            obj.invokeMethodAsync(method)
        }
    }

    _execute(args) {
        if (this._topology) {
            if (args[1] === "reset") {
                this._topology.fitView()
                this._topology.centerView()
            }
            else if (args[1] === "scale") {
                if (args.length > 2) {
                    var t = args[2]
                    if (t > 0) {
                        this._topology.scale(t)
                        this._topology.centerView()
                    }
                }
            } else if (args[1] == "resize") {
                this._topology.resize()
                this._topology.fitView()
                this._topology.centerView()
            } else {
                const data = args[1];
                this._topology.doSocket(JSON.stringify(data))
            }
        }
    }

    _dispose() {
        if (this._topology) {
            this._topology.destroy()
            this._topology = null
        }
    }
}
