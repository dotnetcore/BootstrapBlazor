import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js"
import { addLink } from "../../../_content/BootstrapBlazor/modules/base/utility.js"
import { isVisible } from "../../../_content/BootstrapBlazor/modules/base/index.js"

export class Markdown extends BlazorComponent {
    _init() {
        addLink('_content/BootstrapBlazor.Markdown/css/bootstrap.blazor.markdown.min.css')

        this._invoker = this._config.arguments[0]
        this._options = this._config.arguments[1]
        this._invokerMethod = this._config.arguments[2]

        this._createEditor()
    }

    _createEditor() {
        // 修复弹窗内初始化值不正确问题
        const handler = window.setInterval(() => {
            if (isVisible(this._element)) {
                window.clearInterval(handler)
                this._options.el = this._element
                this._options.plugins = [];
                if (this._options.enableHighlight) {
                    this._options.plugins.push(toastui.Editor.plugin.codeSyntaxHighlight)
                }
                this._editor = toastui.Editor.factory(this._options)
                this._setListeners()
            }
        }, 100)
    }

    _setListeners() {
        this._editor.on('blur', () => {
            const val = this._editor.getMarkdown()
            const html = this._editor.getHTML()
            this._invoker.invokeMethodAsync(this._invokerMethod, [val, html])
        })
    }

    _execute(args) {
        const method = args[1]
        if (method === 'update') {
            this._update(args[2])
        } else if (method === 'do') {
            this._do(args[2], args[3])
        }
    }

    _update(val) {
        if (this._editor) {
            this._editor.setMarkdown(val)
        }
    }

    _do(method, parameters = {}) {
        if (this._editor) {
            this._editor[method](...parameters);
            const val = this._editor.getMarkdown()
            const html = this._editor.getHTML()
            this._invoker.invokeMethodAsync('Update', [val, html])
        }
    }

    _dispose() {
        if (this._editor) {
            this._editor.off('blur')
            this._editor.destroy()
        }
    }
}
