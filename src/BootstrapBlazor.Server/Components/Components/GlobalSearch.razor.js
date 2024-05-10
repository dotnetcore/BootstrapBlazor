import { addScript } from "../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export async function init(id, options) {
    await addScript('../../lib/meilisearch/meilisearch.umd.min.js')

    const el = document.getElementById(id);
    const search = {
        el, options,
        info: el.querySelector('.search-dialog-info'),
        list: el.querySelector('.search-dialog-list'),
        template: el.querySelector('.search-dialog-item-template'),
        blockTemplate: el.querySelector('.search-dialog-block-template'),
        dialog: el.querySelector('.search-dialog')
    };
    Data.set(id, search);

    handlerClearButton(search);
    handlerSearch(search);
    handlerToggle(search);
}

export function dispose(id) {
    const search = Data.get(id);
    Data.remove(id);

    if (search) {
        const { el, dialog, clearButton, input } = search;
        EventHandler.off(clearButton, 'click');
        EventHandler.off(dialog, 'click');
        EventHandler.off(input, 'keyup');
        EventHandler.off(el, 'click');
    }
}

const handlerToggle = search => {
    const { el, dialog } = search;
    EventHandler.on(dialog, 'click', e => {
        e.preventDefault();
        e.stopPropagation();
    });
    EventHandler.on(el, 'click', e => {
        dialog.classList.toggle('show');
    });
}

const handlerClearButton = search => {
    const clearButton = search.el.querySelector('.search-dialog-clear');
    EventHandler.on(clearButton, 'click', () => {
        resetInfo(search);
    });
    search.clearButton = clearButton;
}

const handlerSearch = search => {
    const input = search.el.querySelector('.search-dialog-input > input');
    EventHandler.on(input, 'keyup', e => {
        if (e.key === 'Enter') {
            doSearch(search, input.value);
        }
        else if (e.key === 'Escape') {
            resetInfo(search);
        }
    })
    search.input = input;
}

const doSearch = async (search, query) => {
    if (query) {
        const client = new MeiliSearch({
            host: search.options.host,
            apiKey: search.options.key,
        });
        var index = client.index(search.options.index);
        const results = await index.search(query);
        updateInfo(search, results.estimatedTotalHits, results.processingTimeMs);
        updateList(search, results)
    }
}

const updateList = (search, results) => {
    const { list, template, blockTemplate } = search;
    list.innerHTML = '';

    const html = template.innerHTML;
    const blockHtml = blockTemplate.innerHTML;
    results.hits.forEach(hit => {
        const div = document.createElement('div');
        div.innerHTML = html.replace('{url}', hit.url).replace('{title}', hit.title).replace('{sub-title}', hit.subTitle).replace('{count}', hit.demoBlocks.length);
        const item = div.firstChild;

        if (hit.demoBlocks) {
            const ul = document.createElement('ol');
            ul.classList.add('mb-0');
            ul.classList.add('mt-2')
            hit.demoBlocks.forEach(block => {
                const li = document.createElement('ul');
                li.innerHTML = blockHtml.replace('{url}', block.url).replace('{title}', block.anchorText).replace('{intro}', block.pText);
                ul.appendChild(li.firstChild);
            });
            item.appendChild(ul);
        }
        list.appendChild(item);
    });
}

const updateInfo = (search, hits, ms) => {
    const info = search.info;
    info.innerHTML = `Found ${hits} results in ${ms}ms`;
}

const resetInfo = search => {
    const { info, input, list } = search;
    info.innerHTML = `Powered by BootstrapBlazor`;
    input.value = '';
    list.innerHTML = ''
}
