﻿import { copy, getDescribedElement, addLink, removeLink, addScript, getHeight, getPreferredTheme } from "../../../BootstrapBlazor/modules/utility.js"
import EventHandler from "../../../BootstrapBlazor/modules/event-handler.js"

export async function init(id, title) {
    const el = document.getElementById(id);
    if (el === null) {
        return
    }

    await addScript('./_content/BootstrapBlazor.Shared/lib/highlight/highlight.min.js')
    await addScript('./_content/BootstrapBlazor.Shared/lib/highlight/cshtml-razor.min.js')
    await switchTheme(getPreferredTheme());

    const preElement = el.querySelector('pre')
    const code = el.querySelector('pre > code')

    if (preElement) {
        EventHandler.on(el, 'click', '.btn-copy', e => {
            const text = code.textContent;
            copy(text)

            const tooltip = getDescribedElement(e.delegateTarget)
            if (tooltip) {
                tooltip.querySelector('.tooltip-inner').innerHTML = title
            }
        })

        EventHandler.on(el, 'click', '.btn-plus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(preElement)
            const codeHeight = getHeight(code)
            if (preHeight < codeHeight) {
                preHeight = Math.min(codeHeight, preHeight + 100)
            }
            preElement.style.maxHeight = `${preHeight}px`
        })

        EventHandler.on(el, 'click', '.btn-minus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(preElement)
            if (preHeight > 260) {
                preHeight = Math.max(260, preHeight - 100)
            }
            preElement.style.maxHeight = `${preHeight}px`
        })
    }
}

export async function highlight(id) {
    const el = document.getElementById(id);

    if (el) {
        const invoke = () => {
            hljs.highlightElement(el.querySelector('code'));
            el.querySelector('.loading').classList.add('d-none')
            el.classList.remove('loaded')
        }

        const check = () => new Promise((resolve, reject) => {
            const handler = setInterval(() => {
                const done = window.hljs !== void 0;
                if (done) {
                    hljs.configure({
                        ignoreUnescapedHTML: true
                    });
                    clearInterval(handler)
                    resolve()
                }
            }, 20)
        })

        await check();
        invoke();
    }
}

export async function switchTheme(theme) {
    if (theme === 'dark') {
        removeLink('./_content/BootstrapBlazor.Shared/lib/highlight/vs.min.css')
        await addLink('./_content/BootstrapBlazor.Shared/lib/highlight/vs2015.min.css')
    }
    else {
        removeLink('./_content/BootstrapBlazor.Shared/lib/highlight/vs2015.min.css');
        await addLink('./_content/BootstrapBlazor.Shared/lib/highlight/vs.min.css')
    }
}

export function dispose(id) {
    const el = document.getElementById(id);

    if (el === null) {
        return
    }

    EventHandler.off(el, 'click', '.btn-copy')
    EventHandler.off(el, 'click', '.btn-plus')
    EventHandler.off(el, 'click', '.btn-minus')
}
