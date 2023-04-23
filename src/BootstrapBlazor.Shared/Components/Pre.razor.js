import { copy, getDescribedElement, addLink, addScript, getHeight } from "../../BootstrapBlazor/modules/utility.js"
import Data from "../../BootstrapBlazor/modules/data.js"
import EventHandler from "../../BootstrapBlazor/modules/event-handler.js"

export async function init(id, title) {
    await addScript('_content/BootstrapBlazor.Shared/lib/highlight/highlight.min.js')
    await addLink('_content/BootstrapBlazor.Shared/lib/highlight/vs.min.css')

    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const pre = {
        element: el,
        preElement: el.querySelector('pre'),
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
            const text = e.delegateTarget.parentNode.querySelector('code').textContent;
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

    if (pre) {
        if (method === 'highlight') {
            pre.highlight()
        }
    }
}

export function dispose(id) {
    const pre = Data.get(id)
    Data.remove(id)

    if (pre) {
        if (pre.handler) {
            clearInterval(pre.handler)
        }

        EventHandler.off(pre.el, 'click', '.btn-copy')
        EventHandler.off(pre.el, 'click', '.btn-plus')
        EventHandler.off(pre.el, 'click', '.btn-minus')
    }
}
