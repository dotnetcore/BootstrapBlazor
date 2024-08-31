import './plyr.js';
import './hls.js';
import { addLink } from '../BootstrapBlazor/modules/utility.js';
import Data from '../BootstrapBlazor/modules/data.js';

export async function init(id, invoke, method, options) {
    await addLink('./_content/BootstrapBlazor.VideoPlayer/plyr.css');

    const el = document.getElementById(id);
    const p = { el, invoke, method };
    Data.set(id, p);

    //const type = options.sources[0].type;
    //if (player.supports(type)) {
    //    const { poster } = options;
    //    player.source = {
    //        type: 'video',
    //        title: 'Example title',
    //        poster: poster,
    //        sources: options.sources
    //    };
    //}
    //else
    if (Hls.isSupported()) {
        const hls = new Hls();
        hls.loadSource(options.sources[0].src)
        hls.attachMedia(el);
        p.hls = hls;

        hls.on(Hls.Events.MANIFEST_PARSED, function (event, data) {
            console.log('parsed', event, data)
            const player = new Plyr(el);
            player.on('languagechange', () => {
                console.log('languagechange');
                setTimeout(() => hls.subtitleTrack = player.currentTrack, 300);
            });
            p.player = player;
        });
    }
}

export function setPoster(id, poster) {
    execute(id, p => {
        const { player } = p;
        player.poster = poster;
    });
}

export function reload(id, url, type, poster) {
    execute(id, p => {
        const { player, hls } = p;
        if (player.supports(type)) {
            player.source = {
                type: 'video',
                title: 'Example title',
                poster: poster,
                sources: [
                    {
                        src: url,
                        type: type
                    }
                ]
            };
        }
        else if (Hls.isSupported()) {
            player.stop();
            delete player.source;

            hls.loadSource(url);
        }
    });
}

const execute = (id, callback) => {
    const p = Data.get(id);
    if (p) {
        callback(p);
    }
}

export function dispose(id) {
    const p = Data.get(id);
    Data.remove(id);

    execute(id, player => {
        player.destroy();
        player = null;
    });
}
