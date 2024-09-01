import './plyr.js';
import './hls.js';
import { addLink } from '../BootstrapBlazor/modules/utility.js';
import Data from '../BootstrapBlazor/modules/data.js';

export async function init(id, invoke, method, options) {
    await addLink('./_content/BootstrapBlazor.Player/plyr.css');

    const el = document.getElementById(id);
    const p = { el, invoke, method };
    Data.set(id, p);

    const source = options.source;
    delete options.source;
    p.player = new Plyr(el, options);
    if (source.sources.length === 0) {
        return;
    }

    if (source.sources[0].type !== 'application/x-mpegURL') {
        p.player.source = source;
    }
    else if (Hls.isSupported()) {
        const hls = new Hls();
        hls.loadSource(options.source[0].src)
        hls.attachMedia(el);
        p.hls = hls;

        hls.on(Hls.Events.MANIFEST_PARSED, function (event, data) {
            const player = new Plyr(el);
            player.on('languagechange', () => {
                setTimeout(() => hls.subtitleTrack = player.currentTrack, 300);
            });
            p.player = player;
        });

    }
}

export function update(id, options) {
    const p = Data.get(id);
    if (p === null) {
        return;
    }
    const { player, el } = p;
    const source = options.source;
    delete options.source;
    if (source.sources[0].type !== 'application/x-mpegURL') {
        player.source = source;
    }
    else if (Hls.isSupported()) {
        player.stop();
        player.source = source;

        if (p.hls === void 0) {
            p.hls = new Hls();
            p.hls.attachMedia(el);
        }
        p.hls.loadSource(source.sources[0].src)
        el.load();
        el.play();

        p.hls.on(Hls.Events.MANIFEST_PARSED, function (event, data) {
            player.on('languagechange', () => {
                setTimeout(() => hls.subtitleTrack = player.currentTrack, 300);
            });
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
