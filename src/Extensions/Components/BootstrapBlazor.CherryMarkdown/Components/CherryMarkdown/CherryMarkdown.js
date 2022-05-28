export function bb_cherry_markdown(el, value, method, obj) {
    BootstrapBlazorModules.addLink('_content/BootstrapBlazor.CherryMarkdown/css/bootstrap.blazor.cherrymarkdown.min.css');
    var $el = $(el);
    if (method === "init") {
        if (value.toolbars.toolbar === null) {
            delete value.toolbars.toolbar;
        }
        if (value.toolbars.bubble === null) {
            delete value.toolbars.bubble;
        }
        if (value.toolbars.float === null) {
            delete value.toolbars.float;
        }
        var handler = window.setInterval(function () {

            if ($el.is(':visible')) {
                window.clearInterval(handler);
                var editor = new Cherry({
                    el: el,
                    value: value.value,
                    fileUpload(file, callback) {
                        var id = $.getUID('md');
                        if (window.cherryMarkdownUploadFiles === undefined) {
                            window.cherryMarkdownUploadFiles = {};
                        }
                        window.cherryMarkdownUploadFiles[id] = file;
                        obj.invokeMethodAsync('Upload', id, {
                            fileName: file.name,
                            fileSize: file.size,
                            contentType: file.type,
                            lastModified: new Date(file.lastModified).toISOString(),
                        }).then(data => {
                            if (data !== "") {
                                callback(data);
                            }
                        })
                    },
                    editor: value.editor,
                    toolbars: value.toolbars,
                    callback: {
                        afterChange: function (markdown, html) {
                            obj.invokeMethodAsync('Update', [markdown, html]);
                        }
                    }
                });
                $.data(el, 'bb_cherry_md_editor', editor);
            }
        }, 100);
    } else if (method === 'setMarkdown') {
        var editor = $.data(el, 'bb_cherry_md_editor');
        editor.setMarkdown(value, true);
    }
}

export function bb_cherry_markdown_file(id) {
    var file = window.cherryMarkdownUploadFiles[id];
    delete window.cherryMarkdownUploadFiles[id];
    return file
}

export function bb_cherry_markdown_method(el, method, parameter, obj) {
    var md = $.data(el, 'bb_cherry_md_editor');
    if (md) {
        if (method.indexOf('.') < 0) {
            md[method](...parameter)
        } else {
            var methods = method.split('.');
            var m = md[methods[0]];
            for (let i = 1; i < methods.length; i++) {
                m = m[methods[i]]
            }
            m(...parameter);
        }
        var val = md.getMarkdown();
        var html = md.getHtml();
        obj.invokeMethodAsync('Update', [val, html]);
    }
}

