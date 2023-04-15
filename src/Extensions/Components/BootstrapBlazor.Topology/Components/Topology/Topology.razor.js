import '../../topology.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import EventHandler from "../../../BootstrapBlazor/modules/event-handler.js"

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
            var methodName = args[1];
            if (methodName === "reset") {
                this._topology.fitView()
                this._topology.centerView()
            }
            else if (methodName === "scale" && args.length > 2) {
                var t = args[2]
                if (t > 0) {
                    this._topology.scale(t)
                    this._topology.centerView()
                }
            }
            else if (methodName == "resize") {
                this._topology.canvas.dirty = true
                if (args.lengh > 4) {
                    const width = args[2]
                    const height = args[3]
                    this._topology.resize(width, height)
                }
                else {
                    this._topology.resize()
                }
                this._topology.fitView()
                this._topology.centerView()
            }
            else {
                this._topology.doSocket(JSON.stringify(methodName))
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
