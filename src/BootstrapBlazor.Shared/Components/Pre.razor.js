import EventHandler from "../../BootstrapBlazor/modules/event-handler.js"
import Data from "../../BootstrapBlazor/modules/data.js"
import { copy, getDescribedElement, addLink, addScript, getHeight } from "../../BootstrapBlazor/modules/utility.js"

export async function init(id, title) {
    await addLink('_content/BootstrapBlazor.Shared/lib/highlight/vs.css')
    await addScript('_content/BootstrapBlazor.Shared/lib/highlight/highlight.min.js')
    const element = document.getElementById(id);
    const pre = {
        el: element,
        preelement: element.querySelector('pre'),
        code: element.querySelector('code')
    }
    if (pre.preelement) {
        EventHandler.on(pre.el, 'click', '.btn-copy', e => {
            const text = e.delegateTarget.parentNode.querySelector('code').textContent;
            copy(text)

            const tooltip = getDescribedElement(e.delegateTarget)
            if (tooltip) {
                tooltip.querySelector('.tooltip-inner').innerHTML = title
            }
        })

        EventHandler.on(pre.el, 'click', '.btn-plus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(pre.preelement)
            const codeHeight = getHeight(pre.code)
            if (preHeight < codeHeight) {
                preHeight = Math.min(codeHeight, preHeight + 100)
            }
            pre.preelement.style.maxHeight = `${preHeight}px`
        })

        EventHandler.on(pre.el, 'click', '.btn-minus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(pre.preelement)
            if (preHeight > 260) {
                preHeight = Math.max(260, preHeight - 100)
            }
            pre.preelement.style.maxHeight = `${preHeight}px`
        })
    }

    Data.set(id, pre)
}

export function execute(id, method) {
    if (method === 'highlight') {
        if (window.hljs) {
            highlight(id)
        }
        else {
            const hadnler = window.setInterval(() => {
                if (window.hljs) {
                    clearInterval()
                    highlight(id)
                }
            }, 100)
            Data.set("Interval", hadnler)
        }
    }
}

function highlight(id) {
    const data = Data.get(id)
    if (data.el) {
        const code = data.el.querySelector('code')
        if (code) {
            window.hljs.highlightBlock(code)
        }
    }
}

function clearInterval() {
    const handler = Data.get("Interval")
    if (handler) {
        window.clearInterval(handler)
    }
}

export function dispose(id) {
    clearInterval()
    const data = Data.get(id)
    EventHandler.off(data.el, 'click', '.btn-copy')
    EventHandler.off(data.el, 'click', '.btn-plus')
    EventHandler.off(data.el, 'click', '.btn-minus')
    Data.remove(id)
    Data.remove("Interval")
}
