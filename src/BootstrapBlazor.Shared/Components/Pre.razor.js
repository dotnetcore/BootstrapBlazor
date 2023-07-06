import { copy, getDescribedElement, addLink, addScript, getHeight } from "../../BootstrapBlazor/modules/utility.js?v=7.8.3"
import Data from "../../BootstrapBlazor/modules/data.js?v=7.8.3"
import EventHandler from "../../BootstrapBlazor/modules/event-handler.js?v=7.8.3"

export async function init(id, title) {
    const el = document.getElementById(id);
    if (el === null) {
        return
    }

    await addScript('_content/BootstrapBlazor.Shared/lib/highlight/highlight.min.js')
    await addLink('_content/BootstrapBlazor.Shared/lib/highlight/vs.min.css')

    const pre = {
        element: el,
        preElement: el.querySelector('pre'),
        code: el.querySelector('pre > code'),
        highlight: () => {
            let count = 0
            pre.handler = setInterval(() => {
                if (window.hljs) {
                    clearInterval(pre.handler)
                    delete pre.handler

                    window.hljs.highlightBlock(el.querySelector('code'))
                }
                else {
                    count++
                    if (count > 10) {
                        clearInterval(pre.handler)
                        delete pre.handler
                    }
                }
            }, 100)
        }
    }
    Data.set(id, pre)

    if (pre.preElement) {
        EventHandler.on(el, 'click', '.btn-copy', e => {
            const text = pre.code.textContent;
            copy(text)

            const tooltip = getDescribedElement(e.delegateTarget)
            if (tooltip) {
                tooltip.querySelector('.tooltip-inner').innerHTML = title
            }
        })

        EventHandler.on(el, 'click', '.btn-plus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(pre.preElement)
            const codeHeight = getHeight(pre.code)
            if (preHeight < codeHeight) {
                preHeight = Math.min(codeHeight, preHeight + 100)
            }
            pre.preElement.style.maxHeight = `${preHeight}px`
        })

        EventHandler.on(el, 'click', '.btn-minus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(pre.preElement)
            if (preHeight > 260) {
                preHeight = Math.max(260, preHeight - 100)
            }
            pre.preElement.style.maxHeight = `${preHeight}px`
        })
    }
}

export function highlight(id) {
    const pre = Data.get(id)

    if (pre) {
        pre.highlight()
    }
}

export function dispose(id) {
    const pre = Data.get(id)
    Data.remove(id)

    if (pre === null) {
        return
    }
    if (pre.handler) {
        clearInterval(pre.handler)
    }

    EventHandler.off(pre.el, 'click', '.btn-copy')
    EventHandler.off(pre.el, 'click', '.btn-plus')
    EventHandler.off(pre.el, 'click', '.btn-minus')
}
