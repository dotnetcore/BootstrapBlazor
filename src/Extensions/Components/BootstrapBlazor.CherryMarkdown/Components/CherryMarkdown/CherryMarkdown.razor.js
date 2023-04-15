import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import { addLink } from "../../../_content/BootstrapBlazor/modules/base/utility.js"
import { isVisible, getElementById } from "../../../_content/BootstrapBlazor/modules/base/index.js"

export class CherryMarkdown extends BlazorComponent {
    _init() {
        addLink('_content/BootstrapBlazor.CherryMarkdown/css/bootstrap.blazor.cherrymarkdown.min.css')

        this._invoker = this._config.arguments[0]
        this._options = this._config.arguments[1]
        this._invokerMethod = this._config.arguments[2]

        if (this._options.toolbars.toolbar === null) {
            delete this._options.toolbars.toolbar
        }
        if (this._options.toolbars.bubble === null) {
            delete this._options.toolbars.bubble
        }
        if (this._options.toolbars.float === null) {
            delete this._options.toolbars.float
        }

        this._createEditor()
    }

    _createEditor() {
        const fileUpload = (file, callback) => {
            this._file = file
            this._invoker.invokeMethodAsync(this._invokerMethod, {
                fileName: file.name,
                fileSize: file.size,
                contentType: file.type,
                lastModified: new Date(file.lastModified).toISOString(),
            }).then(data => {
                if (data !== "") {
                    callback(data)
                }
            })
        }

        this._handler = window.setInterval(() => {
            if (isVisible(this._element)) {
                window.clearInterval(this._handler)
                this._handler = null
                this._editor = new Cherry({
                    el: this._element,
                    value: this._options.value,
                    editor: this._options.editor,
                    toolbars: this._options.toolbars,
                    callback: {
                        afterChange: (markdown, html) => {
                            this._invoker.invokeMethodAsync('Update', [markdown, html])
                        }
                    },
                    fileUpload: fileUpload
                });
            }
        }, 100)
    }

    _execute(args) {
        this._editor.setMarkdown(args[1], true)
    }

    _invoke(args) {
        const invoker = args[0]
        const method = args[1]
        const parameter = args[2]
        if (this._editor) {
            if (method.indexOf('.') < 0) {
                this._editor[method](...parameter)
            } else {
                var methods = method.split('.');
                var m = this._editor[methods[0]];
                for (let i = 1; i < methods.length; i++) {
                    m = m[methods[i]]
                }
                m(...parameter);
            }
            var val = this._editor.getMarkdown();
            var html = this._editor.getHtml();
            invoker.invokeMethodAsync('Update', [val, html]);
        }
    }

    _fetch() {
        return this._file
    }

    _dispose() {
        if (this._handler) {
            window.clearInterval(this._handler)
            this._handler = null
        }
    }

    static invoke(element) {
        element = getElementById(element)
        if (element) {
            const instance = this.getInstance(element)
            instance._invoke([].slice.call(arguments, 1))
        }
    }

    static fetch(element) {
        element = getElementById(element)
        if (element) {
            const instance = this.getInstance(element)
            return instance._fetch()
        }
    }
}
