import '../../js/summernote-bs5.min.js'
import { addLink, addScript } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import EventHandler from '../../../BootstrapBlazor/modules/event-handler.js'

export async function init(id, invoker, methodGetPluginAttrs, methodClickPluginItem, height, value, lang, langUrl) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink('./_content/BootstrapBlazor.SummerNote/css/bootstrap.blazor.editor.min.css')
    const editor = { el, invoker }
    Data.set(id, editor)

    editor.editorElement = el.querySelector('.editor-body')

    const initEditor = async () => {
        let option = { focus: true, dialogsInBody: true, height, lang, langUrl }
        if (value !== '') {
            option.value = value
        }

        await setLang(option)

        const editorLangConfig = $.summernote.lang[option.lang].bb_editor

        let title = ''
        let tooltip = ''
        if (editorLangConfig) {
            title = editorLangConfig.submit
            tooltip = editorLangConfig.tooltip
        }

        // div 点击事件
        EventHandler.on(editor.editorElement, 'click', async () => {
            editor.tooltip.hide()
            option.placeholder = editor.editorElement.getAttribute('placeholder')
            const toolbar = await editor.invoker.invokeMethodAsync('GetToolBar')
            editor.editorElement.classList.add('open')

            const showSubmit = el.getAttribute("data-bb-submit") === "true"
            if (!showSubmit) {
                option.callbacks = {
                    onChange: (contents, $editable) => {
                        editor.invoker.invokeMethodAsync('Update', contents)
                    }
                }
            }
            option.toolbar = toolbar
            editor.$editor = $(editor.editorElement).summernote(option)

            editor.editorToolbar = editor.el.querySelector('.note-toolbar')
            EventHandler.on(editor.editorToolbar, 'click', 'button[data-method]', e => {
                switch (e.delegateTarget.getAttribute('data-method')) {
                    case 'submit':
                        disposeTooltip(editor.submitTooltip)
                        delete editor.submitTooltip

                        offEvent(editor.editorToolbar)
                        editor.editorElement.classList.remove('open')
                        const code = editor.$editor.summernote('code')
                        disposeEditor(editor)
                        editor.invoker.invokeMethodAsync('Update', code)
                        break
                }
            })

            if (showSubmit) {
                editor.$submit = $('<div class="note-btn-group btn-group note-view note-right"><button type="button" class="note-btn btn btn-sm note-btn-close" tabindex="-1" data-method="submit" data-bs-placement="bottom"><i class="fa-solid fa-check"></i></button></div>').appendTo($(editor.editorToolbar)).find('button')
                editor.submitTooltip = new bootstrap.Tooltip(editor.$submit[0], {
                    title: title,
                    container: 'body'
                })
            }

            document.querySelector('.note-group-select-from-files [accept="image/*"]').setAttribute('accept', 'image/bmp,image/png,image/jpg,image/jpeg,image/gif')
        })

        editor.tooltip = new bootstrap.Tooltip(editor.editorElement, { title: tooltip })

        if (option.value) editor.editorElement.innerHTML = option.value
        if (editor.editorElement.classList.contains('open')) {
            const clickEvent = new Event('click')
            editor.editorElement.dispatchEvent(clickEvent)
        }
    }

    if (methodGetPluginAttrs) {
        const result = await editor.invoker.invokeMethodAsync(methodGetPluginAttrs)
        result.forEach(item => {
            const pluginObj = {}
            pluginObj[item.buttonName] = function (context) {
                let ui = $.summernote.ui
                context.memo(`button.${item.buttonName}`, function () {
                    const button = ui.button({
                        contents: `<i class="${item.iconClass}"></i>`,
                        container: "body",
                        tooltip: item.tooltip,
                        click: async () => {
                            const html = await editor.invoker.invokeMethodAsync(methodClickPluginItem, item.buttonName)
                            if (html.length > 0) {
                                context.invoke('editor.pasteHTML', html)
                            }
                        }
                    })
                    return button.render()
                })
            }
            $.summernote.plugins = {
                ...$.summernote.plugins,
                ...pluginObj
            }
        })
    }
    initEditor()
}

export function update(id, val) {
    const editor = Data.get(id)
    if (editor.$editor) {
        editor.$editor.summernote('code', val)
    }
    else {
        editor.editorElement.innerHTML = val
    }
}

export function invoke(id, method, parameter) {
    const editor = Data.get(id)
    editor.$editor.summernote(method, ...parameter)
}

export function reset(id) {
    const editor = Data.get(id)
    const context = editor.$editor.data('summernote')

    const showSubmit = editor.el.getAttribute("data-bb-submit") === "true"
    if (showSubmit) {
        const editorLangConfig = $.summernote.lang[context.options.lang].bb_editor
        let title = ''
        if (editorLangConfig) {
            title = editorLangConfig.submit
        }

        editor.$submit = $('<div class="note-btn-group btn-group note-view note-right"><button type="button" class="note-btn btn btn-sm note-btn-close" tabindex="-1" data-method="submit" data-bs-placement="bottom"><i class="fa-solid fa-check"></i></button></div>').appendTo($(editor.editorToolbar)).find('button')
        editor.submitTooltip = new bootstrap.Tooltip(editor.$submit[0], {
            title: title,
            container: 'body'
        })
        context.options.callbacks.onChange = null
    }
    else {
        editor.$submit.remove()
        context.options.callbacks.onChange = (contents, $editable) => {
            editor.invoker.invokeMethodAsync('Update', contents)
        }
    }
}

export function dispose(id) {
    const editor = Data.get(id)
    Data.remove(id)
    if (editor) {
        disposeTooltip(editor.submitTooltip)
        disposeTooltip(editor.tooltip)
        offEvent(editor.editorToolbar)
        offEvent(editor.editorElement)
        disposeEditor(editor)

        for (const propertyName of Object.getOwnPropertyNames(editor)) {
            editor[propertyName] = null
            delete editor[propertyName]
        }
    }
}

const offEvent = eventEl => {
    if (eventEl) {
        EventHandler.off(eventEl, 'click')
        eventEl = null
    }
}

const disposeTooltip = tip => {
    if (tip) {
        tip.dispose()
        tip = null
    }
}

const disposeEditor = editor => {
    if (editor.$editor) {
        editor.$editor.summernote('destroy')
        delete editor.$editor
    }
}

const setLang = async option => {
    if (option.langUrl) {
        await addScript(option.langUrl)
    }

    initLang();

    if ($.summernote.lang[option.lang] === undefined) {
        option.lang = 'en-US'
    }
    if ($.summernote.lang[option.lang].bb_editor === undefined) {
        $.summernote.lang[option.lang].bb_editor = {
            tooltip: "Click to edit",
            submit: "submit"
        }
    }
}

const initLang = () => {
    $.summernote.lang["zh-CN"] = {
        font: {
            bold: "粗体",
            italic: "斜体",
            underline: "下划线",
            clear: "清除格式",
            height: "行高",
            name: "字体",
            strikethrough: "删除线",
            subscript: "下标",
            superscript: "上标",
            size: "字号"
        },
        image: {
            image: "图片",
            insert: "插入图片",
            resizeFull: "缩放至 100%",
            resizeHalf: "缩放至 50%",
            resizeQuarter: "缩放至 25%",
            floatLeft: "靠左浮动",
            floatRight: "靠右浮动",
            floatNone: "取消浮动",
            shapeRounded: "形状: 圆角",
            shapeCircle: "形状: 圆",
            shapeThumbnail: "形状: 缩略图",
            shapeNone: "形状: 无",
            dragImageHere: "将图片拖拽至此处",
            dropImage: "拖拽图片或文本",
            selectFromFiles: "从本地上传",
            maximumFileSize: "文件大小最大值",
            maximumFileSizeError: "文件大小超出最大值。",
            url: "图片地址",
            remove: "移除图片",
            original: "原始图片"
        },
        video: {
            video: "视频",
            videoLink: "视频链接",
            insert: "插入视频",
            url: "视频地址",
            providers: "(优酷, 腾讯, Instagram, DailyMotion, Youtube等)"
        },
        link: {
            link: "链接",
            insert: "插入链接",
            unlink: "去除链接",
            edit: "编辑链接",
            textToDisplay: "显示文本",
            url: "链接地址",
            openInNewWindow: "在新窗口打开"
        },
        table: {
            table: "表格",
            addRowAbove: "在上方插入行",
            addRowBelow: "在下方插入行",
            addColLeft: "在左侧插入列",
            addColRight: "在右侧插入列",
            delRow: "删除行",
            delCol: "删除列",
            delTable: "删除表格"
        },
        hr: {
            insert: "水平线"
        },
        style: {
            style: "样式",
            p: "普通",
            blockquote: "引用",
            pre: "代码",
            h1: "标题 1",
            h2: "标题 2",
            h3: "标题 3",
            h4: "标题 4",
            h5: "标题 5",
            h6: "标题 6"
        },
        lists: {
            unordered: "无序列表", ordered: "有序列表"
        },
        options: {
            help: "帮助", fullscreen: "全屏", codeview: "源代码"
        },
        paragraph: {
            paragraph: "段落",
            outdent: "减少缩进",
            indent: "增加缩进",
            left: "左对齐",
            center: "居中对齐",
            right: "右对齐",
            justify: "两端对齐"
        },
        color: {
            recent: "最近使用",
            more: "更多",
            background: "背景",
            foreground: "前景",
            transparent: "透明",
            setTransparent: "透明",
            reset: "重置",
            resetToDefault: "默认"
        },
        shortcut: {
            shortcuts: "快捷键",
            close: "关闭",
            textFormatting: "文本格式",
            action: "动作",
            paragraphFormatting: "段落格式",
            documentStyle: "文档样式",
            extraKeys: "额外按键"
        },
        help: {
            insertParagraph: "插入段落",
            undo: "撤销",
            redo: "重做",
            tab: "增加缩进",
            untab: "减少缩进",
            bold: "粗体",
            italic: "斜体",
            underline: "下划线",
            strikethrough: "删除线",
            removeFormat: "清除格式",
            justifyLeft: "左对齐",
            justifyCenter: "居中对齐",
            justifyRight: "右对齐",
            justifyFull: "两端对齐",
            insertUnorderedList: "无序列表",
            insertOrderedList: "有序列表",
            outdent: "减少缩进",
            indent: "增加缩进",
            formatPara: "设置选中内容样式为 普通",
            formatH1: "设置选中内容样式为 标题1",
            formatH2: "设置选中内容样式为 标题2",
            formatH3: "设置选中内容样式为 标题3",
            formatH4: "设置选中内容样式为 标题4",
            formatH5: "设置选中内容样式为 标题5",
            formatH6: "设置选中内容样式为 标题6",
            insertHorizontalRule: "插入水平线",
            "linkDialog.show": "显示链接对话框"
        },
        history: {
            undo: "撤销", redo: "重做"
        },
        specialChar: {
            specialChar: "特殊字符", select: "选取特殊字符"
        },
        bb_editor: {
            tooltip: "点击展开编辑",
            submit: "完成"
        }
    }
    $.summernote.lang["en-US"].bb_editor = {
        tooltip: "Click to edit",
        submit: "submit"
    }
}
