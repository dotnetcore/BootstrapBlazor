import '../../js/summernote-bs5.min.js'
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

const html2edit = (editor, options) => {
    var $this = $el
    var op = typeof options == 'object' && options

    //$.bb_lang()

    op = {
        ...{ focus: true, height: 80, dialogsInBody: true },
        ...op
    }

    if (/destroy|hide/.test(options)) {
        return $this.toggleClass('open').summernote(op)
    }
    else if (typeof options == 'string') {
        return $this.hasClass('open') ? $this.summernote(op) : $this.html()
    }

    const editorLangConfig = $.summernote.lang[options.lang].bb_editor
    let title = ''
    let tooltip = ''
    if (editorLangConfig) {
        title = editorLangConfig.submit
        tooltip = editorLangConfig.tooltip
    }

    // div 点击事件
    $this.on('click', op, function (event, args) {
        var $this = $(this).tooltip('hide')
        var op = $.extend({ placeholder: $this.attr('placeholder') }, event.data, args || {})
        op.obj.invokeMethodAsync('GetToolBar').then(result => {
            var $toolbar = $this.toggleClass('open').summernote($.extend({
                callbacks: {
                    onChange: function (htmlString) {
                        op.obj.invokeMethodAsync(op.callback, htmlString)
                    }
                },
                toolbar: result
            }, op))
                .next().find('.note-toolbar')
                .on('click', 'button[data-method]', { note: $this, op: op }, function (event) {
                    var $btn = $(this)
                    switch ($btn.attr('data-method')) {
                        case 'submit':
                            $btn.tooltip('dispose')
                            var $note = event.data.note.toggleClass('open')
                            var htmlString = $note.summernote('code')
                            $note.summernote('destroy')
                            event.data.op.obj.invokeMethodAsync(event.data.op.callback, htmlString)
                            break
                    }
                })

            var $done = $('<div class="note-btn-group btn-group note-view note-right"><button type="button" class="note-btn btn btn-sm note-btn-close" tabindex="-1" data-method="submit" data-bs-placement="bottom"><i class="fa-solid fa-check"></i></button></div>').appendTo($toolbar).find('button').tooltip({
                title: title,
                container: 'body'
            })
            $('body').find('.note-group-select-from-files [accept="image/*"]').attr('accept', 'image/bmp,image/png,image/jpg,image/jpeg,image/gif')
        })

    }).tooltip({ title: tooltip })

    if (op.value) $this.html(op.value)
    if ($this.hasClass('open')) {
        // 初始化为 editor
        $this.trigger('click', { focus: false })
    }
    return this
}

export async function init(el, invoker, methodGetPluginAttrs, methodClickPluginItem, callback, height, value, lang) {
    await addLink('_content/BootstrapBlazor.SummerNote/css/bootstrap.blazor.editor.min.css')

    const initEditor = () => {
        const editor = el.querySelector(".editor-body")
        const option = { obj: obj, callback: callback, height: height, lang }
        if (value) {
            option.value = value
        }
        html2edit(editor, option)
    }
    
    if (methodGetPluginAttrs) {
        const result = await obj.invokeMethodAsync(methodGetPluginAttrs)
        for (var i in result) {
            (function (plugin, pluginName) {
                if (pluginName == null) {
                    return
                }
                var pluginObj = {}
                pluginObj[pluginName] = context => {
                    var ui = $.summernote.ui
                    context.memo('button.' + pluginName,
                        function () {
                            var button = ui.button({
                                contents: '<i class="' + plugin.iconClass + '"></i>',
                                container: "body",
                                tooltip: plugin.tooltip,
                                click: async () => {
                                    const html = await obj.invokeMethodAsync(methodClickPluginItem, pluginName)
                                    context.invoke('editor.pasteHTML', html)
                                }
                            })
                            return button.render()
                        })
                }
                $.extend($.summernote.plugins, pluginObj)
            })(result[i], result[i].buttonName)
        }
    }
    initEditor()
}

export function bb_editor_code(el, obj, value) {
    var $editor = $(el).find(".editor-body")
    if ($editor.hasClass('open')) {
        $editor.summernote('code', value)
    }
    else {
        $editor.html(value)
    }
}

export function bb_editor_method(el, obj, method, parameter) {
    var $editor = $(el).find(".editor-body")
    $editor.toggleClass('open').summernote(method, ...parameter)
}

(function ($) {
    $.extend({
        bb_lang: function () {
            $.extend($.summernote.lang, {
                "zh-CN":
                {
                    font:
                    {
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
                        image: "图片", insert: "插入图片", resizeFull: "缩放至 100%", resizeHalf: "缩放至 50%", resizeQuarter: "缩放至 25%", floatLeft: "靠左浮动", floatRight: "靠右浮动", floatNone: "取消浮动", shapeRounded: "形状: 圆角", shapeCircle: "形状: 圆", shapeThumbnail: "形状: 缩略图", shapeNone: "形状: 无", dragImageHere: "将图片拖拽至此处", dropImage: "拖拽图片或文本", selectFromFiles: "从本地上传", maximumFileSize: "文件大小最大值", maximumFileSizeError: "文件大小超出最大值。", url: "图片地址", remove: "移除图片", original: "原始图片"
                    },
                    video: {
                        video: "视频", videoLink: "视频链接", insert: "插入视频", url: "视频地址", providers: "(优酷, 腾讯, Instagram, DailyMotion, Youtube等)"
                    },
                    link: {
                        link: "链接", insert: "插入链接", unlink: "去除链接", edit: "编辑链接", textToDisplay: "显示文本", url: "链接地址", openInNewWindow: "在新窗口打开"
                    },
                    table: {
                        table: "表格", addRowAbove: "在上方插入行", addRowBelow: "在下方插入行", addColLeft: "在左侧插入列", addColRight: "在右侧插入列", delRow: "删除行", delCol: "删除列", delTable: "删除表格"
                    },
                    hr: {
                        insert: "水平线"
                    },
                    style: {
                        style: "样式", p: "普通", blockquote: "引用", pre: "代码", h1: "标题 1", h2: "标题 2", h3: "标题 3", h4: "标题 4", h5: "标题 5", h6: "标题 6"
                    },
                    lists: {
                        unordered: "无序列表", ordered: "有序列表"
                    },
                    options: {
                        help: "帮助", fullscreen: "全屏", codeview: "源代码"
                    },
                    paragraph: {
                        paragraph: "段落", outdent: "减少缩进", indent: "增加缩进", left: "左对齐", center: "居中对齐", right: "右对齐", justify: "两端对齐"
                    },
                    color: {
                        recent: "最近使用", more: "更多", background: "背景", foreground: "前景", transparent: "透明", setTransparent: "透明", reset: "重置", resetToDefault: "默认"
                    },
                    shortcut: {
                        shortcuts: "快捷键", close: "关闭", textFormatting: "文本格式", action: "动作", paragraphFormatting: "段落格式", documentStyle: "文档样式", extraKeys: "额外按键"
                    },
                    help: {
                        insertParagraph: "插入段落", undo: "撤销", redo: "重做", tab: "增加缩进", untab: "减少缩进", bold: "粗体", italic: "斜体", underline: "下划线", strikethrough: "删除线", removeFormat: "清除格式", justifyLeft: "左对齐", justifyCenter: "居中对齐", justifyRight: "右对齐", justifyFull: "两端对齐", insertUnorderedList: "无序列表", insertOrderedList: "有序列表", outdent: "减少缩进", indent: "增加缩进", formatPara: "设置选中内容样式为 普通", formatH1: "设置选中内容样式为 标题1", formatH2: "设置选中内容样式为 标题2", formatH3: "设置选中内容样式为 标题3", formatH4: "设置选中内容样式为 标题4", formatH5: "设置选中内容样式为 标题5", formatH6: "设置选中内容样式为 标题6", insertHorizontalRule: "插入水平线", "linkDialog.show": "显示链接对话框"
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
            })
            $.extend(true, $.summernote.lang, {
                "en-US":
                {
                    bb_editor: {
                        tooltip: "Click to edit",
                        submit: "submit"
                    }
                }
            })
        }
    })
})(jQuery)
