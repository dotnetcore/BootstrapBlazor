import Data from '../../../BootstrapBlazor/modules/data.js'

export function init(id, interop, options) {
    // require is provided by loader.min.js.
    require.config({
        paths: { 'vs': options.path }
    });

    require(["vs/editor/editor.main"], () => {
        var container = document.getElementById(id);
        var body = container.querySelector(".code-editor-body");

        // Hide the Progress Ring
        monaco.editor.onDidCreateEditor((e) => {
            var progress = container.querySelector(".spinner");
            if (progress && progress.style) {
                progress.style.display = "none";
            }
        });

        const editor = {}

        // Create the Monaco Editor
        editor.editor = monaco.editor.create(body, {
            ariaLabel: "online code editor",
            value: options.value,
            language: options.language,
            theme: options.theme,
            lineNumbers: options.lineNumbers ? "on" : "off",
            readOnly: options.readOnly,
        });

        // Catch when the editor lost the focus (didType to immediate)
        editor.editor.onDidBlurEditorText((e) => {
            var code = editor.editor.getValue();
            interop.invokeMethodAsync("UpdateValueAsync", code);
        });

        editor.interop = interop;

        Data.set(id, editor)

        editor.editor.layout();

        window.addEventListener("resize", () => {
            editor.editor.layout();
        })

        window.editor = editor.editor;   // To debug
    });
}

// Update the editor options
export function monacoSetOptions(id, options) {
    var editor = Data.get(id);
    if (editor) {
        editor.editor.setValue(options.value);
        editor.editor.updateOptions({
            language: options.language,
            theme: options.theme
        });
    }
}

export function dispose(id) {
    Data.remove(id)
    window.removeEventListener("resize")
}
