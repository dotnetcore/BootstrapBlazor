import { addScript, debounce, isMobile } from "../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export async function init(id, options) {
    await addScript('../../lib/meilisearch/meilisearch.umd.min.js')

    const el = document.getElementById(id);
    const search = {
        el, options,
        searchText: 'searching ...',
        status: el.querySelector('.search-dialog-status'),
        list: el.querySelector('.search-dialog-list'),
        template: el.querySelector('.search-dialog-item-template'),
        blockTemplate: el.querySelector('.search-dialog-block-template'),
        emptyTemplate: el.querySelector('.search-dialog-empty-template'),
        dialog: el.querySelector('.search-dialog'),
        mask: el.querySelector('.search-dialog-mask')
    };
    Data.set(id, search);

    handlerClearButton(search);
    handlerSearch(search);
    handlerToggle(search);
    handlerMask(search);

    resetStatus(search);
}

export function dispose(id) {
    const search = Data.get(id);
    Data.remove(id);

    if (search) {
        const { el, dialog, clearButton, input } = search;
        EventHandler.off(clearButton, 'click');
        EventHandler.off(dialog, 'click');
        EventHandler.off(input, 'keyup');
        EventHandler.off(input, 'input');
        EventHandler.off(el, 'click');
        EventHandler.off(document, 'click');
    }
}

const handlerMask = search => {
    const { mask } = search;
    document.body.appendChild(mask);
}

const handlerToggle = search => {
    const { el, mask, dialog, input } = search;
    EventHandler.on(dialog, 'click', e => {
        e.stopPropagation();
    });
    EventHandler.on(el, 'click', e => {
        mask.classList.toggle('show');
        dialog.classList.toggle('show');
        if (dialog.classList.contains('show')) {
            input.focus();
            if (!isMobile()) {
                input.select();
            }
        }
    });
    EventHandler.on(document, 'click', e => {
        const element = e.target.closest('.bb-g-search');
        if (element === null) {
            dialog.classList.remove('show');
            mask.classList.remove('show');
        }
    });
}

const handlerClearButton = search => {
    const clearButton = search.el.querySelector('.search-dialog-clear');
    EventHandler.on(clearButton, 'click', () => {
        resetStatus(search);
    });
    search.clearButton = clearButton;
}

const handlerSearch = search => {
    const input = search.el.querySelector('.search-dialog-input > input');
    EventHandler.on(input, 'keyup', e => {
        if (e.key === 'Enter') {
            doSearch(search, input.value);
            if (!isMobile()) {
                input.select();
            }
        }
        else if (e.key === 'Escape') {
            resetStatus(search);
        }
    });
    const fn = debounce(doSearch);
    EventHandler.on(input, 'input', () => {
        fn(search, input.value);
    });
    search.input = input;
}

const doSearch = async (search, query) => {
    if (query) {
        search.status.innerHTML = search.searchText;
        const client = new MeiliSearch({
            host: search.options.host,
            apiKey: search.options.key,
        });
        var index = client.index(search.options.index);
        const results = await index.search(query);
        updateStatus(search, results.estimatedTotalHits, results.processingTimeMs);
        updateList(search, results);
    }
}

const updateList = (search, results) => {
    const { list, input, template, blockTemplate } = search;
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
                const url = block.url || hit.url;
                li.innerHTML = blockHtml.replace('{url}', url).replace('{title}', block.anchorText).replace('{intro}', block.pText);
                ul.appendChild(li.firstChild);
            });
            item.appendChild(ul);
        }
        list.appendChild(item);
    });
    input.focus();
}

const updateStatus = (search, hits, ms) => {
    const status = search.status;
    status.innerHTML = `Found ${hits} results in ${ms}ms`;
}

const resetStatus = search => {
    const { options, status, input, list, emptyTemplate } = search;
    status.innerHTML = options.searchStatus;
    input.value = '';
    list.innerHTML = emptyTemplate.outerHTML;
}
