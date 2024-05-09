import { addScript } from "../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export async function init(id) {
    await addScript('https://cdn.jsdelivr.net/npm/meilisearch@latest/dist/bundles/meilisearch.umd.js')

    const client = new MeiliSearch({
        host: 'http://47.92.144.33:7700',
        apiKey: 'BootstrapBlazorSearch',
    });
    var index = client.index('bbsearch');
    const search = await index.search('table')
    console.log(search)

    //const el = document.getElementById(id);
    //const input = el.querySelector('input');
    //const list = el.querySelector('.search-list');
    //EventHandler.on(input, 'focus', e => {
    //    list.classList.add('show');
    //});

    Data.set(id, { el, input });
}

export function dispose(id) {
    const search = Data.get(id);
    Data.remove(id);

    //if (search) {
    //    EventHandler.off(search.input, 'focus');
    //}
}
