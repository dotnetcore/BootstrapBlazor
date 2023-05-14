import { copy, getDescribedElement, addLink, addScript, getHeight } from "../../BootstrapBlazor/modules/utility.js?v=$version"
import Data from "../../BootstrapBlazor/modules/data.js?v=$version"
import EventHandler from "../../BootstrapBlazor/modules/event-handler.js?v=$version"

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
            pre.handler = setInterval(() => {
                if (hljs) {
                    clearInterval(pre.handler)
                    delete pre.handler

                    hljs.highlightBlock(el.querySelector('code'))
                }
            }, 30)
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

export function execute(id, method) {
    const pre = Data.get(id)

    if (pre && method === 'highlight') {
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
