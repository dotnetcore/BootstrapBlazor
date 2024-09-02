import './plyr.js';
import './hls.js';
import { addLink, addScript } from '../BootstrapBlazor/modules/utility.js';
import Data from '../BootstrapBlazor/modules/data.js';

export async function init(id, invoke, method, options) {
    await addLink('./_content/BootstrapBlazor.Player/plyr.css');
    await addScript('./_content/BootstrapBlazor.Player/plyr-plugin-thumbnail.js');

    const el = document.getElementById(id);
    const p = { el, invoke, method };
    Data.set(id, p);

    if (options.language === 'zh-CN') {
        setLang(options);
    }
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

const setLang = (option) => {
    option.i18n = {
        restart: '重启',
        rewind: '后退 {seektime}s',
        play: '播放',
        pause: '暂停',
        fastForward: '前进 {seektime}s',
        seek: '进度',
        seekLabel: '{currentTime} of {duration}',
        played: '已播放',
        buffered: '已缓冲',
        currentTime: '当前时间',
        duration: '时长',
        volume: '音量',
        mute: '静音',
        unmute: '取消静音',
        enableCaptions: '开启字幕',
        disableCaptions: '禁用字幕',
        download: '下载',
        enterFullscreen: '全屏',
        exitFullscreen: '退出全屏',
        frameTitle: 'Player for {title}',
        captions: '字幕',
        settings: '设置',
        menuBack: '返回上级菜单',
        speed: '倍速',
        normal: '正常',
        quality: '清晰度',
        loop: '循环',
        start: '开始',
        end: '结束',
        all: '所有',
        reset: '重置',
        disabled: '禁用',
        enabled: '启用',
        advertisement: '广告',
        qualityBadge: {
            2160: '4K',
            1440: 'HD',
            1080: 'HD',
            720: 'HD',
            576: 'SD',
            480: 'SD',
        }
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
