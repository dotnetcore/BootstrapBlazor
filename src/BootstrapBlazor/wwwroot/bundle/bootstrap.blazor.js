/**
 * 浏览器解析，浏览器、Node.js皆可
 * https://github.com/mumuy/browser
 */

;(function (root, factory) {
    if (typeof define === 'function' && (define.amd||define.cmd)) {
        // AMD&CMD
        define(function(){
            return factory(root);
        });
    } else if (typeof exports === 'object') {
        // Node, CommonJS-like
        module.exports = factory(root);
    } else {
        // Browser globals (root is window)
        root.Browser = factory(root);
    }
}(typeof self !== 'undefined' ? self : this, function (root) {
    var _window = root||{};
    var _navigator = typeof root.navigator!='undefined'?root.navigator:{};
    var _mime = function (option, value) {
        var mimeTypes = _navigator.mimeTypes;
        for (var mt in mimeTypes) {
            if (mimeTypes[mt][option] == value) {
                return true;
            }
        }
        return false;
    };

    return function (userAgent) {
        var u = userAgent || _navigator.userAgent||{};
        var _this = this;

        var match = {
            //内核
            'Trident': u.indexOf('Trident') > -1 || u.indexOf('NET CLR') > -1,
            'Presto': u.indexOf('Presto') > -1,
            'WebKit': u.indexOf('AppleWebKit') > -1,
            'Gecko': u.indexOf('Gecko/') > -1,
            'KHTML': u.indexOf('KHTML/') > -1,
            //浏览器
            'Safari': u.indexOf('Safari') > -1,
            'Chrome': u.indexOf('Chrome') > -1 || u.indexOf('CriOS') > -1,
            'IE': u.indexOf('MSIE') > -1 || u.indexOf('Trident') > -1,
            'Edge': u.indexOf('Edge') > -1||u.indexOf('Edg/') > -1||u.indexOf('EdgA') > -1||u.indexOf('EdgiOS') > -1,
            'Firefox': u.indexOf('Firefox') > -1 || u.indexOf('FxiOS') > -1,
            'Firefox Focus': u.indexOf('Focus') > -1,
            'Chromium': u.indexOf('Chromium') > -1,
            'Opera': u.indexOf('Opera') > -1 || u.indexOf('OPR') > -1,
            'Vivaldi': u.indexOf('Vivaldi') > -1,
            'Yandex': u.indexOf('YaBrowser') > -1,
            'Arora': u.indexOf('Arora') > -1,
            'Lunascape': u.indexOf('Lunascape') > -1,
            'QupZilla': u.indexOf('QupZilla') > -1,
            'Coc Coc': u.indexOf('coc_coc_browser') > -1,
            'Kindle': u.indexOf('Kindle') > -1 || u.indexOf('Silk/') > -1,
            'Iceweasel': u.indexOf('Iceweasel') > -1,
            'Konqueror': u.indexOf('Konqueror') > -1,
            'Iceape': u.indexOf('Iceape') > -1,
            'SeaMonkey': u.indexOf('SeaMonkey') > -1,
            'Epiphany': u.indexOf('Epiphany') > -1,
            '360': u.indexOf('QihooBrowser') > -1||u.indexOf('QHBrowser') > -1,
            '360EE': u.indexOf('360EE') > -1,
            '360SE': u.indexOf('360SE') > -1,
            'UC': u.indexOf('UCBrowser') > -1 || u.indexOf(' UBrowser') > -1 || u.indexOf('UCWEB') > -1,
            'QQBrowser': u.indexOf('QQBrowser') > -1,
            'QQ': u.indexOf('QQ/') > -1,
            'Baidu': u.indexOf('Baidu') > -1 || u.indexOf('BIDUBrowser') > -1 || u.indexOf('baidubrowser') > -1|| u.indexOf('baiduboxapp') > -1 || u.indexOf('BaiduHD') > -1,
            'Maxthon': u.indexOf('Maxthon') > -1,
            'Sogou': u.indexOf('MetaSr') > -1 || u.indexOf('Sogou') > -1,
            'Liebao': u.indexOf('LBBROWSER') > -1|| u.indexOf('LieBaoFast') > -1,
            '2345Explorer': u.indexOf('2345Explorer') > -1||u.indexOf('Mb2345Browser') > -1||u.indexOf('2345chrome') > -1,
            '115Browser': u.indexOf('115Browser') > -1,
            'TheWorld': u.indexOf('TheWorld') > -1,
            'XiaoMi': u.indexOf('MiuiBrowser') > -1,
            'Quark': u.indexOf('Quark') > -1,
            'Qiyu': u.indexOf('Qiyu') > -1,
            'Wechat': u.indexOf('MicroMessenger') > -1,
            'WechatWork': u.indexOf('wxwork/') > -1,
            'Taobao': u.indexOf('AliApp(TB') > -1,
            'Alipay': u.indexOf('AliApp(AP') > -1,
            'Weibo': u.indexOf('Weibo') > -1,
            'Douban': u.indexOf('com.douban.frodo') > -1,
            'Suning': u.indexOf('SNEBUY-APP') > -1,
            'iQiYi': u.indexOf('IqiyiApp') > -1,
            'DingTalk': u.indexOf('DingTalk') > -1,
            'Huawei': u.indexOf('HuaweiBrowser') > -1||u.indexOf('HUAWEI/') > -1||u.indexOf('HONOR') > -1||u.indexOf('HBPC/') > -1,
            'Vivo': u.indexOf('VivoBrowser') > -1,
            //系统或平台
            'Windows': u.indexOf('Windows') > -1,
            'Linux': u.indexOf('Linux') > -1 || u.indexOf('X11') > -1,
            'Mac OS': u.indexOf('Macintosh') > -1,
            'Android': u.indexOf('Android') > -1 || u.indexOf('Adr') > -1,
            'HarmonyOS': u.indexOf('HarmonyOS') > -1,
            'Ubuntu': u.indexOf('Ubuntu') > -1,
            'FreeBSD': u.indexOf('FreeBSD') > -1,
            'Debian': u.indexOf('Debian') > -1,
            'Windows Phone': u.indexOf('IEMobile') > -1 || u.indexOf('Windows Phone')>-1,
            'BlackBerry': u.indexOf('BlackBerry') > -1 || u.indexOf('RIM') > -1,
            'MeeGo': u.indexOf('MeeGo') > -1,
            'Symbian': u.indexOf('Symbian') > -1,
            'iOS': u.indexOf('like Mac OS X') > -1,
            'Chrome OS': u.indexOf('CrOS') > -1,
            'WebOS': u.indexOf('hpwOS') > -1,
            //设备
            'Mobile': u.indexOf('Mobi') > -1 || u.indexOf('iPh') > -1 || u.indexOf('480') > -1,
            'Tablet': u.indexOf('Tablet') > -1 || u.indexOf('Pad') > -1 || u.indexOf('Nexus 7') > -1 || (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1),
            //环境
            'isWebview': u.indexOf('; wv)')>-1
        };
        var is360 = false;
        if(_window.chrome){
            var chrome_version = u.replace(/^.*Chrome\/([\d]+).*$/, '$1');
            if(_window.chrome.adblock2345||_window.chrome.common2345){
                match['2345Explorer'] = true;
            }else if(_mime("type", "application/360softmgrplugin")||_mime("type", "application/mozilla-npqihooquicklogin")){
                is360 = true;
            }else if(chrome_version>36&&_window.showModalDialog){
                is360 = true;
            }else if(chrome_version>45){
                is360 = _mime("type", "application/vnd.chromium.remoting-viewer");
                if(!is360&&chrome_version>=69){
                    is360 = _mime("type", "application/hwepass2001.installepass2001")||_mime("type", "application/asx");
                }
            }
        }
        //修正
        if (match['Mobile']) {
            match['Mobile'] = !(u.indexOf('iPad') > -1);
        } else if (is360) {
            if(_mime("type", "application/gameplugin")){
                match['360SE'] = true;
            }else if(_navigator && typeof _navigator['connection'] !== 'undefined' && typeof _navigator['connection']['saveData'] == 'undefined'){
                match['360SE'] = true;
            }else{
                match['360EE'] = true;
            }
        }
        if(match['Baidu']&&match['Opera']){
            match['Baidu'] = false;
        }else if(match['iOS']){
            match['Safari'] = true;
        }
        //基本信息
        var hash = {
            engine: ['WebKit', 'Trident', 'Gecko', 'Presto', 'KHTML'],
            browser: ['Safari', 'Chrome', 'Edge', 'IE', 'Firefox', 'Firefox Focus', 'Chromium', 'Opera', 'Vivaldi', 'Yandex', 'Arora', 'Lunascape', 'QupZilla', 'Coc Coc', 'Kindle', 'Iceweasel', 'Konqueror', 'Iceape', 'SeaMonkey', 'Epiphany', 'XiaoMi','Vivo', '360', '360SE', '360EE', 'UC', 'QQBrowser', 'QQ', 'Huawei', 'Baidu', 'Maxthon', 'Sogou', 'Liebao', '2345Explorer', '115Browser', 'TheWorld', 'Quark', 'Qiyu', 'Wechat', 'WechatWork', 'Taobao', 'Alipay', 'Weibo', 'Douban','Suning', 'iQiYi', 'DingTalk'],
            os: ['Windows', 'Linux', 'Mac OS', 'Android', 'HarmonyOS', 'Ubuntu', 'FreeBSD', 'Debian', 'iOS', 'Windows Phone', 'BlackBerry', 'MeeGo', 'Symbian', 'Chrome OS', 'WebOS'],
            device: ['Mobile', 'Tablet']
        };
        _this.device = 'PC';
        _this.language = (function () {
            var g = (_navigator.browserLanguage || _navigator.language);
            var arr = g.split('-');
            if (arr[1]) {
                arr[1] = arr[1].toUpperCase();
            }
            return arr.join('_');
        })();
        for (var s in hash) {
            for (var i = 0; i < hash[s].length; i++) {
                var value = hash[s][i];
                if (match[value]) {
                    _this[s] = value;
                }
            }
        }
        //系统版本信息
        var osVersion = {
            'Windows': function () {
                var v = u.replace(/^Mozilla\/\d.0 \(Windows NT ([\d.]+)[;)].*$/, '$1');
                var hash = {
                    '10':'10',
                    '6.4': '10',
                    '6.3': '8.1',
                    '6.2': '8',
                    '6.1': '7',
                    '6.0': 'Vista',
                    '5.2': 'XP',
                    '5.1': 'XP',
                    '5.0': '2000'
                };
                return hash[v] || v;
            },
            'Android': function () {
                return u.replace(/^.*Android ([\d.]+);.*$/, '$1');
            },
            'HarmonyOS': function () {
                var v = u.replace(/^Mozilla.*Android ([\d.]+)[;)].*$/, '$1');
                var hash = {
                    '10':'2',
                };
                return hash[v] || '';
            },
            'iOS': function () {
                return u.replace(/^.*OS ([\d_]+) like.*$/, '$1').replace(/_/g, '.');
            },
            'Debian': function () {
                return u.replace(/^.*Debian\/([\d.]+).*$/, '$1');
            },
            'Windows Phone': function () {
                return u.replace(/^.*Windows Phone( OS)? ([\d.]+);.*$/, '$2');
            },
            'Mac OS': function () {
                return u.replace(/^.*Mac OS X ([\d_]+).*$/, '$1').replace(/_/g, '.');
            },
            'WebOS': function () {
                return u.replace(/^.*hpwOS\/([\d.]+);.*$/, '$1');
            }
        };
        _this.osVersion = '';
        if (osVersion[_this.os]) {
            _this.osVersion = osVersion[_this.os]();
            if (_this.osVersion == u) {
                _this.osVersion = '';
            }
        }
        //支持 win11
        if (_this.osVersion === '10.0' && _navigator.userAgentData) {
            _navigator.userAgentData.getHighEntropyValues(["platformVersion"])
                .then(ua => {
                    const majorPlatformVersion = parseInt(ua.platformVersion.split('.')[0]);
                    if (majorPlatformVersion >= 13) {
                        _this.osVersion = '11.0'
                    }
                });
        }
        _this.isWebview = match['isWebview'];
        //浏览器版本信息
        var version = {
            'Safari': function () {
                return u.replace(/^.*Version\/([\d.]+).*$/, '$1');
            },
            'Chrome': function () {
                return u.replace(/^.*Chrome\/([\d.]+).*$/, '$1').replace(/^.*CriOS\/([\d.]+).*$/, '$1');
            },
            'IE': function () {
                return u.replace(/^.*MSIE ([\d.]+).*$/, '$1').replace(/^.*rv:([\d.]+).*$/, '$1');
            },
            'Edge': function () {
                return u.replace(/^.*Edge\/([\d.]+).*$/, '$1').replace(/^.*Edg\/([\d.]+).*$/, '$1').replace(/^.*EdgA\/([\d.]+).*$/, '$1').replace(/^.*EdgiOS\/([\d.]+).*$/, '$1');
            },
            'Firefox': function () {
                return u.replace(/^.*Firefox\/([\d.]+).*$/, '$1').replace(/^.*FxiOS\/([\d.]+).*$/, '$1');
            },
            'Firefox Focus': function () {
                return u.replace(/^.*Focus\/([\d.]+).*$/, '$1');
            },
            'Chromium': function () {
                return u.replace(/^.*Chromium\/([\d.]+).*$/, '$1');
            },
            'Opera': function () {
                return u.replace(/^.*Opera\/([\d.]+).*$/, '$1').replace(/^.*OPR\/([\d.]+).*$/, '$1');
            },
            'Vivaldi': function () {
                return u.replace(/^.*Vivaldi\/([\d.]+).*$/, '$1');
            },
            'Yandex': function () {
                return u.replace(/^.*YaBrowser\/([\d.]+).*$/, '$1');
            },
            'Arora': function () {
                return u.replace(/^.*Arora\/([\d.]+).*$/, '$1');
            },
            'Lunascape': function(){
                return u.replace(/^.*Lunascape[\/\s]([\d.]+).*$/, '$1');
            },
            'QupZilla': function(){
                return u.replace(/^.*QupZilla[\/\s]([\d.]+).*$/, '$1');
            },
            'Coc Coc': function(){
                return u.replace(/^.*coc_coc_browser\/([\d.]+).*$/, '$1');
            },
            'Kindle': function () {
                return u.replace(/^.*Version\/([\d.]+).*$/, '$1');
            },
            'Iceweasel': function () {
                return u.replace(/^.*Iceweasel\/([\d.]+).*$/, '$1');
            },
            'Konqueror': function () {
                return u.replace(/^.*Konqueror\/([\d.]+).*$/, '$1');
            },
            'Iceape': function () {
                return u.replace(/^.*Iceape\/([\d.]+).*$/, '$1');
            },
            'SeaMonkey': function () {
                return u.replace(/^.*SeaMonkey\/([\d.]+).*$/, '$1');
            },
            'Epiphany': function () {
                return u.replace(/^.*Epiphany\/([\d.]+).*$/, '$1');
            },
            '360': function(){
                return u.replace(/^.*QihooBrowser\/([\d.]+).*$/, '$1');
            },
            '360SE': function(){
                var hash = {'86':'13.0','78':'12.0','69':'11.0','63':'10.0','55':'9.1','45':'8.1','42':'8.0','31':'7.0','21':'6.3'};
                var chrome_version = u.replace(/^.*Chrome\/([\d]+).*$/, '$1');
                return hash[chrome_version]||'';
            },
            '360EE': function(){
                var hash = {'95':'21','86':'13.0','78':'12.0','69':'11.0','63':'9.5','55':'9.0','50':'8.7','30':'7.5'};
                var chrome_version = u.replace(/^.*Chrome\/([\d]+).*$/, '$1');
                return hash[chrome_version]||'';
            },
            'Maxthon': function () {
                return u.replace(/^.*Maxthon\/([\d.]+).*$/, '$1');
            },
            'QQBrowser': function () {
                return u.replace(/^.*QQBrowser\/([\d.]+).*$/, '$1');
            },
            'QQ': function () {
                return u.replace(/^.*QQ\/([\d.]+).*$/, '$1');
            },
            'Baidu': function () {
                return u.replace(/^.*BIDUBrowser[\s\/]([\d.]+).*$/, '$1').replace(/^.*baiduboxapp\/([\d.]+).*$/, '$1');
            },
            'UC': function () {
                return u.replace(/^.*UC?Browser\/([\d.]+).*$/, '$1');
            },
            'Sogou': function () {
                return u.replace(/^.*SE ([\d.X]+).*$/, '$1').replace(/^.*SogouMobileBrowser\/([\d.]+).*$/, '$1');
            },
            'Liebao': function(){
                var version = ''
                if(u.indexOf('LieBaoFast')>-1){
                    version = u.replace(/^.*LieBaoFast\/([\d.]+).*$/, '$1');
                }
                var hash = {'57':'6.5','49':'6.0','46':'5.9','42':'5.3','39':'5.2','34':'5.0','29':'4.5','21':'4.0'};
                var chrome_version = u.replace(/^.*Chrome\/([\d]+).*$/, '$1');
                return version||hash[chrome_version]||'';
            },
            '2345Explorer': function () {
                var hash = {'69':'10.0','55':'9.9'};
                var chrome_version = navigator.userAgent.replace(/^.*Chrome\/([\d]+).*$/, '$1');
                return hash[chrome_version]||u.replace(/^.*2345Explorer\/([\d.]+).*$/, '$1').replace(/^.*Mb2345Browser\/([\d.]+).*$/, '$1');
            },
            '115Browser': function(){
                return u.replace(/^.*115Browser\/([\d.]+).*$/, '$1');
            },
            'TheWorld': function () {
                return u.replace(/^.*TheWorld ([\d.]+).*$/, '$1');
            },
            'XiaoMi': function () {
                return u.replace(/^.*MiuiBrowser\/([\d.]+).*$/, '$1');
            },
            'Vivo': function(){
                return u.replace(/^.*VivoBrowser\/([\d.]+).*$/, '$1');
            },
            'Quark': function () {
                return u.replace(/^.*Quark\/([\d.]+).*$/, '$1');
            },
            'Qiyu': function () {
                return u.replace(/^.*Qiyu\/([\d.]+).*$/, '$1');
            },
            'Wechat': function () {
                return u.replace(/^.*MicroMessenger\/([\d.]+).*$/, '$1');
            },
            'WechatWork': function () {
                return u.replace(/^.*wxwork\/([\d.]+).*$/, '$1');
            },
            'Taobao': function () {
                return u.replace(/^.*AliApp\(TB\/([\d.]+).*$/, '$1');
            },
            'Alipay': function () {
                return u.replace(/^.*AliApp\(AP\/([\d.]+).*$/, '$1');
            },
            'Weibo': function () {
                return u.replace(/^.*weibo__([\d.]+).*$/, '$1');
            },
            'Douban': function () {
                return u.replace(/^.*com.douban.frodo\/([\d.]+).*$/, '$1');
            },
            'Suning': function () {
                return u.replace(/^.*SNEBUY-APP([\d.]+).*$/, '$1');
            },
            'iQiYi': function () {
                return u.replace(/^.*IqiyiVersion\/([\d.]+).*$/, '$1');
            },
            'DingTalk': function () {
                return u.replace(/^.*DingTalk\/([\d.]+).*$/, '$1');
            },
            'Huawei': function () {
                return u.replace(/^.*Version\/([\d.]+).*$/, '$1').replace(/^.*HuaweiBrowser\/([\d.]+).*$/, '$1').replace(/^.*HBPC\/([\d.]+).*$/, '$1');
            }
        };
        _this.version = '';
        if (version[_this.browser]) {
            _this.version = version[_this.browser]();
            if (_this.version == u) {
                _this.version = '';
            }
        }
        //修正
        if(_this.browser == 'Chrome'&&u.match(/\S+Browser/)){
            _this.browser = u.match(/\S+Browser/)[0];
            _this.version = u.replace(/^.*Browser\/([\d.]+).*$/, '$1');
        }
        if (_this.browser == 'Edge') {
            _this.engine = parseInt(_this.version)>75?'Blink':'EdgeHTML';
        } else if (match['Chrome']&& _this.engine=='WebKit' && parseInt(version['Chrome']()) > 27) {
            _this.engine = 'Blink';
        } else if (_this.browser == 'Opera' && parseInt(_this.version) > 12) {
            _this.engine = 'Blink';
        } else if (_this.browser == 'Yandex') {
            _this.engine = 'Blink';
        }
    };
}));

(function ($) {
    var SliderCaptcha = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, options);
        this.init();
    };

    SliderCaptcha.VERSION = '5.1.0';
    SliderCaptcha.Author = 'argo@163.com';
    SliderCaptcha.DATA_KEY = "lgb.SliderCaptcha";

    var _proto = SliderCaptcha.prototype;
    _proto.init = function () {
        this.initDOM();
        this.initImg();
        this.bindEvents();
    };

    _proto.initDOM = function () {
        var $canvas = this.$element.find("canvas:first")[0].getContext('2d');
        var $block = this.$element.find("canvas:last")[0];
        var $bar = $block.getContext('2d');
        var $load = this.$element.find(".captcha-load");
        var $footer = this.$element.find('.captcha-footer');
        var $barLeft = $footer.find('.captcha-bar-bg');
        var $slider = this.$element.find('.captcha-bar');
        var $barText = this.$element.find('.captcha-bar-text');
        var $refresh = this.$element.find('.captcha-refresh');
        var barText = $barText.attr('data-text');
        $.extend(this, {
            canvas: $canvas,
            block: $block,
            bar: $bar,
            $load: $load,
            $footer: $footer,
            $barLeft: $barLeft,
            $slider: $slider,
            $barText: $barText,
            $refresh: $refresh,
            barText: barText
        });
    };

    _proto.initImg = function () {
        var drawImg = function (ctx, operation) {
            var l = this.options.sideLength;
            var r = this.options.diameter;
            var PI = Math.PI;
            var x = this.options.offsetX;
            var y = this.options.offsetY;
            ctx.beginPath();
            ctx.moveTo(x, y);
            ctx.arc(x + l / 2, y - r + 2, r, 0.72 * PI, 2.26 * PI);
            ctx.lineTo(x + l, y);
            ctx.arc(x + l + r - 2, y + l / 2, r, 1.21 * PI, 2.78 * PI);
            ctx.lineTo(x + l, y + l);
            ctx.lineTo(x, y + l);
            ctx.arc(x + r - 2, y + l / 2, r + 0.4, 2.76 * PI, 1.24 * PI, true);
            ctx.lineTo(x, y);
            ctx.lineWidth = 2;
            ctx.fillStyle = 'rgba(255, 255, 255, 0.7)';
            ctx.strokeStyle = 'rgba(255, 255, 255, 0.7)';
            ctx.stroke();
            ctx[operation]();
            ctx.globalCompositeOperation = 'destination-over';
        };

        var img = new Image();
        img.src = this.options.imageUrl;
        var that = this;
        img.onload = function () {
            drawImg.call(that, that.canvas, 'fill');
            drawImg.call(that, that.bar, 'clip');

            that.canvas.drawImage(img, 0, 0, that.options.width, that.options.height);
            that.bar.drawImage(img, 0, 0, that.options.width, that.options.height);

            var y = that.options.offsetY - that.options.diameter * 2 - 1;
            var ImageData = that.bar.getImageData(that.options.offsetX - 3, y, that.options.barWidth, that.options.barWidth);
            that.block.width = that.options.barWidth;
            that.bar.putImageData(ImageData, 0, y);
        };
        img.onerror = function () {
            that.$load.text($load.attr('data-failed')).addClass('text-danger');
        }
    };

    _proto.bindEvents = function () {
        var that = this;
        var originX = 0;
        var originY = 0;
        var trail = [];

        this.$slider.drag(
            function (e) {
                that.$barText.addClass('d-none');
                originX = e.clientX || e.touches[0].clientX;
                originY = e.clientY || e.touches[0].clientY;
            },
            function (e) {
                var eventX = e.clientX || e.touches[0].clientX;
                var eventY = e.clientY || e.touches[0].clientY;
                var moveX = eventX - originX;
                var moveY = eventY - originY;
                if (moveX < 0 || moveX + 40 > that.options.width) return false;

                that.$slider.css({ 'left': (moveX - 1) + 'px' });
                var blockLeft = (that.options.width - 40 - 20) / (that.options.width - 40) * moveX;
                that.block.style.left = blockLeft + 'px';

                that.$footer.addClass('is-move');
                that.$barLeft.css({ 'width': (moveX + 4) + 'px' });
                trail.push(Math.round(moveY));
            },
            function (e) {
                var eventX = e.clientX || e.changedTouches[0].clientX;
                that.$footer.removeClass('is-move');

                var offset = Math.ceil((that.options.width - 40 - 20) / (that.options.width - 40) * (eventX - originX) + 3);
                that.verify(offset, trail);
            }
        );

        this.$refresh.on('click', function () {
            that.options.barText = that.$barText.attr('data-text');
        });
    };

    _proto.verify = function (offset, trails) {
        var remoteObj = this.options.remoteObj.obj;
        var method = this.options.remoteObj.method;
        var that = this;
        remoteObj.invokeMethodAsync(method, offset, trails).then(function (data) {
            if (data) {
                that.$footer.addClass('is-valid');
                that.options.barText = that.$barText.attr('data-text');
            } else {
                that.$footer.addClass('is-invalid');
                setTimeout(function () {
                    that.$refresh.trigger('click');
                    that.options.barText = that.$barText.attr('data-try')
                }, 1000);
            }
        });
    };

    _proto.update = function (option) {
        $.extend(this.options, option);
        this.resetCanvas();
        this.initImg();
        this.resetBar();
    }

    _proto.resetCanvas = function () {
        this.canvas.clearRect(0, 0, this.options.width, this.options.height);
        this.bar.clearRect(0, 0, this.options.width, this.options.height);
        this.block.width = this.options.width;
        this.block.style.left = 0;
        this.$load.text(this.$load.attr('data-load')).removeClass('text-danger');
    };

    _proto.resetBar = function () {
        this.$footer.removeClass('is-invalid is-valid');
        this.$barText.text(this.options.barText).removeClass('d-none');
        this.$slider.css({ 'left': '0px' });
        this.$barLeft.css({ 'width': '0px' });
    };

    function CaptchaPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(SliderCaptcha.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(SliderCaptcha.DATA_KEY, data = new SliderCaptcha(this, options));
            else data.update(options);
        });
    }

    $.fn.sliderCaptcha = CaptchaPlugin;
    $.fn.sliderCaptcha.Constructor = SliderCaptcha;

    $.extend({
        captcha: function (el, obj, method, options) {
            options.remoteObj = { obj, method };
            $(el).sliderCaptcha(options);
        }
    });
})(jQuery);

(function ($) {
    if (!$.isFunction(Date.prototype.format)) {
        Date.prototype.format = function (format) {
            var o = {
                "M+": this.getMonth() + 1,
                "d+": this.getDate(),
                "h+": this.getHours() % 12 === 0 ? 12 : this.getHours() % 12,
                "H+": this.getHours(),
                "m+": this.getMinutes(),
                "s+": this.getSeconds(),
                "q+": Math.floor((this.getMonth() + 3) / 3),
                "S": this.getMilliseconds()
            };
            var week = {
                0: "日",
                1: "一",
                2: "二",
                3: "三",
                4: "四",
                5: "五",
                6: "六"
            };

            if (/(y+)/.test(format))
                format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));

            if (/(E+)/.test(format))
                format = format.replace(RegExp.$1, (RegExp.$1.length > 1 ? RegExp.$1.length > 2 ? "星期" : "周" : "") + week[this.getDay()]);

            for (var k in o)
                if (new RegExp("(" + k + ")").test(format))
                    format = format.replace(RegExp.$1, RegExp.$1.length === 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            return format;
        };
    }

    $.browser = {
        versions: function () {
            var u = navigator.userAgent;
            return {         //移动终端浏览器版本信息
                trident: u.indexOf('Trident') > -1, //IE内核
                presto: u.indexOf('Presto') > -1, //opera内核
                webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') === -1, //火狐内核
                mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端
                ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器
                iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器
                iPod: u.indexOf('iPod') > -1, //是否为iPod或者QQHD浏览器
                iPad: u.indexOf('iPad') > -1, //是否iPad
                mac: u.indexOf('Macintosh') > -1,
                webApp: u.indexOf('Safari') === -1 //是否web应该程序，没有头部与底部
            };
        }(),
        language: (navigator.browserLanguage || navigator.language).toLowerCase()
    };

    Array.prototype.indexOf = function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) return i;
        }
        return -1;
    };

    Array.prototype.remove = function (val) {
        var index = this.indexOf(val);
        if (index > -1) {
            this.splice(index, 1);
        }
    };

    $.extend({
        format: function (source, params) {
            if (params === undefined || params === null) {
                return null;
            }
            if (arguments.length > 2 && params.constructor !== Array) {
                params = $.makeArray(arguments).slice(1);
            }
            if (params.constructor !== Array) {
                params = [params];
            }
            $.each(params, function (i, n) {
                source = source.replace(new RegExp("\\{" + i + "\\}", "g"), function () {
                    return n;
                });
            });
            return source;
        },
        getUID: function (prefix) {
            if (!prefix) prefix = 'b';
            do prefix += ~~(Math.random() * 1000000);
            while (document.getElementById(prefix));
            return prefix;
        },
        webClient: function (obj, url, method) {
            var data = {};
            var browser = new Browser();
            data.Browser = browser.browser + ' ' + browser.version;
            data.Device = browser.device;
            data.Language = browser.language;
            data.Engine = browser.engine;
            data.UserAgent = navigator.userAgent;

            $.ajax({
                type: "GET",
                url: url,
                success: function (result) {
                    data.Os = browser.os + ' ' + browser.osVersion;
                    obj.invokeMethodAsync(method, result.Id, result.Ip, data.Os, data.Browser, data.Device, data.Language, data.Engine, data.UserAgent);
                },
                error: function (xhr, state, errorThrown) {
                    console.error('Please add UseBootstrapBlazor middleware');
                    obj.invokeMethodAsync(method, '', '', data.Os, data.Browser, data.Device, data.Language, data.Engine, data.UserAgent);
                }
            });
        }
    });

    $.fn.extend({
        drag: function (start, move, end) {
            var $this = $(this);

            var handleDragStart = function (e) {
                e.preventDefault();
                e.stopPropagation();

                document.addEventListener('mousemove', handleDragMove);
                document.addEventListener('touchmove', handleDragMove);
                document.addEventListener('mouseup', handleDragEnd);
                document.addEventListener('touchend', handleDragEnd);

                if ($.isFunction(start)) {
                    start.call($this, e);
                }
            };

            var handleDragMove = function (e) {
                if (e.touches && e.touches.length > 1) {
                    return;
                }

                if ($.isFunction(move)) {
                    move.call($this, e);
                }
            };

            var handleDragEnd = function (e) {
                // 结束拖动
                if ($.isFunction(end)) {
                    end.call($this, e);
                }

                window.setTimeout(function () {
                    document.removeEventListener('mousemove', handleDragMove);
                    document.removeEventListener('touchmove', handleDragMove);
                    document.removeEventListener('mouseup', handleDragEnd);
                    document.removeEventListener('touchend', handleDragEnd);
                }, 100);
            };

            $this.on('mousedown', handleDragStart);
            $this.on('touchstart', handleDragStart);
        }
    });

    var load = function (targetName, callback, interval) {
        if (!interval) {
            interval = 100;
        }
        if (!window[targetName]) {
            var handler = window.setInterval(function () {
                if (!!window[targetName]) {
                    window.clearInterval(handler);

                    callback();
                }
            }, interval);
        }
        else {
            callback();
        }
    };

    var addScript = function (content) {
        // content 文件名
        const links = [...document.getElementsByTagName('script')];
        var link = links.filter(function (link) {
            return link.src.indexOf(content) > -1;
        });
        if (link.length === 0) {
            link = document.createElement('script');
            link.setAttribute('src', content);
            document.body.appendChild(link);
        }
    };

    var removeScript = function (content) {
        const links = [...document.getElementsByTagName('script')];
        var nodes = links.filter(function (link) {
            return link.src.indexOf(content) > -1;
        });
        for (var index = 0; index < nodes.length; index++) {
            document.body.removeChild(nodes[index]);
        }
    }

    var addLink = function (href) {
        const links = [...document.getElementsByTagName('link')];
        var link = links.filter(function (link) {
            return link.href.indexOf(href) > -1;
        });
        if (link.length === 0) {
            link = document.createElement('link');
            link.setAttribute('href', href);
            link.setAttribute("rel", "stylesheet");
            document.getElementsByTagName("head")[0].appendChild(link);
        }
    }

    var removeLink = function (href) {
        const links = [...document.getElementsByTagName('link')];
        var nodes = links.filter(function (link) {
            return link.href.indexOf(content) > -1;
        });
        for (var index = 0; index < nodes.length; index++) {
            document.getElementsByTagName("head")[0].removeChild(nodes[index]);
        }
    }

    window.BootstrapBlazorModules = {
        load: load,
        addScript: addScript,
        removeScript: removeScript,
        addLink: addLink,
        removeLink: removeLink
    };
})(jQuery);

(function ($) {
    /**
     * Grid
     * @param {any} element
     * @param {any} options
     */
    var Grid = function (element, options) {
        this.$element = $(element);
        var colSpan = this._getColSpan(this.$element);
        var rowType = this.$element.data('type');
        var itemsPerRow = parseInt(this.$element.data('items'));
        if (isNaN(itemsPerRow)) itemsPerRow = 12;

        this.options = $.extend({ rowType, itemsPerRow, colSpan }, options);
        this.layout();
    };

    Grid.VERSION = "5.1.0";
    Grid.Author = 'argo@163.com';
    Grid.DATA_KEY = "lgb.grid";

    $.extend(Grid.prototype, {
        layout: function () {
            this._layout_column(null);
            this.$element.removeClass('d-none');
        },
        _layout_column: function ($target) {
            var $el = this.$element;
            var rowType = this.options.rowType;
            var itemsPerRow = this.options.itemsPerRow;
            var isLabel = false;
            var $groupCell = null;
            var that = this;
            var $div = $('<div class="row g-3"></div>');
            if (rowType === "inline") $div.addClass('form-inline');

            $el.children().each(function (index, ele) {
                var $ele = $(ele);
                var isRow = $ele.data('toggle') === 'row';
                var colSpan = that._getColSpan($ele);
                if (isRow) {
                    $('<div></div>').addClass(that._calc(colSpan)).appendTo($div).append($ele);
                }
                else {
                    isLabel = $ele.prop('tagName') === 'LABEL';

                    // 如果有 Label 表示在表单内
                    if (isLabel) {
                        if ($groupCell === null) {
                            $groupCell = $('<div></div>').addClass(that._calc(colSpan));
                        }
                        $groupCell.append($ele);
                    }
                    else {
                        isLabel = false;
                        if ($groupCell == null) {
                            $groupCell = $('<div></div>').addClass(that._calc(colSpan));
                        }
                        $groupCell.append($ele);
                        if ($target == null) $groupCell.appendTo($div);
                        else $groupCell.appendTo($target);
                        $groupCell = null;
                    }
                }
            });

            if ($target == null) {
                $el.append($div);
            }
        },
        _layout_parent_row: function () {
            var uid = this.$element.data('target');
            var $target = $('[data-uid="' + uid + '"]');
            var $row = $('<div class="row"></div>').appendTo($target);
            this._layout_column($row);
        },
        _calc: function (colSpan) {
            var itemsPerRow = this.options.itemsPerRow;
            if (colSpan > 0) itemsPerRow = itemsPerRow * colSpan;
            var ret = "col-12";
            if (itemsPerRow !== 12) {
                ret = "col-12 col-sm-" + itemsPerRow;
            }
            return ret;
        },
        _getColSpan: function ($el) {
            var colSpan = parseInt($el.data('colspan'));
            if (isNaN(colSpan)) colSpan = 0;
            return colSpan;
        }
    });

    function GridPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(Grid.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(Grid.DATA_KEY, data = new Grid(this, options));
        });
    }

    $.fn.grid = GridPlugin;
    $.fn.grid.Constructor = Grid;
})(jQuery);

(function ($) {
    /**
     * Tab
     * @param {any} element
     * @param {any} options
     */
    var Tab = function (element, options) {
        this.$element = $(element);
        this.$header = this.$element.children('.tabs-header');
        this.$wrap = this.$header.children('.tabs-nav-wrap');
        this.$scroll = this.$wrap.children('.tabs-nav-scroll');
        this.$tab = this.$scroll.children('.tabs-nav');
        this.options = $.extend({}, options);
        this.init();
    };

    Tab.VERSION = "5.1.0";
    Tab.Author = 'argo@163.com';
    Tab.DATA_KEY = "lgb.Tab";

    var _proto = Tab.prototype;
    _proto.init = function () {
        var that = this;
        $(window).on('resize', function () {
            that.resize();
        });
        this.active();
    };

    _proto.fixSize = function () {
        var height = this.$element.height();
        var width = this.$element.width();
        this.$element.css({ 'height': height + 'px', 'width': width + 'px' });
    }

    _proto.resize = function () {
        this.vertical = this.$element.hasClass('tabs-left') || this.$element.hasClass('tabs-right');
        this.horizontal = this.$element.hasClass('tabs-top') || this.$element.hasClass('tabs-bottom');

        var $lastItem = this.$tab.find('.tabs-item:last');
        if ($lastItem.length > 0) {
            if (this.vertical) {
                this.$wrap.css({ 'height': this.$element.height() + 'px' });
                var tabHeight = this.$tab.height();
                var itemHeight = $lastItem.position().top + $lastItem.outerHeight();
                if (itemHeight < tabHeight) this.$wrap.removeClass("is-scrollable");
                else this.$wrap.addClass('is-scrollable');
            }
            else {
                this.$wrap.removeAttr('style');
                var tabWidth = this.$tab.width();
                var itemWidth = $lastItem.position().left + $lastItem.outerWidth();
                if (itemWidth < tabWidth) this.$wrap.removeClass("is-scrollable");
                else this.$wrap.addClass('is-scrollable');
            }
        }
    }

    _proto.active = function () {
        // check scrollable
        this.resize();

        var $bar = this.$tab.children('.tabs-active-bar');
        var $activeTab = this.$tab.children('.tabs-item.active');
        if ($activeTab.length === 0) return;

        if (this.vertical) {
            //scroll
            var top = $activeTab.position().top;
            var itemHeight = top + $activeTab.outerHeight();
            var scrollTop = this.$scroll.scrollTop();
            var scrollHeight = this.$scroll.outerHeight();
            var marginTop = itemHeight - scrollTop - scrollHeight;
            if (marginTop > 0) {
                this.$scroll.scrollTop(scrollTop + marginTop);
            }
            else {
                var marginBottom = top - scrollTop;
                if (marginBottom < 0) {
                    this.$scroll.scrollTop(scrollTop + marginBottom);
                }
            }
            $bar.css({ 'width': '2px', 'transform': 'translateY(' + top + 'px)' });
        }
        else {
            // scroll
            var left = $activeTab.position().left;
            var itemWidth = left + $activeTab.outerWidth();
            var scrollLeft = this.$scroll.scrollLeft();
            var scrollWidth = this.$scroll.width();
            var marginLeft = itemWidth - scrollLeft - scrollWidth;
            if (marginLeft > 0) {
                this.$scroll.scrollLeft(scrollLeft + marginLeft);
            }
            else {
                var marginRight = left - scrollLeft;
                if (marginRight < 0) {
                    this.$scroll.scrollLeft(scrollLeft + marginRight);
                }
            }
            var width = $activeTab.width();
            var itemLeft = left + parseInt($activeTab.css('paddingLeft'));
            $bar.css({ 'width': width + 'px', 'transform': 'translateX(' + itemLeft + 'px)' });
        }
    };

    function TabPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(Tab.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(Tab.DATA_KEY, data = new Tab(this, options));
            if (typeof option === 'string') {
                if (/active/.test(option))
                    data[option].apply(data);
            }
        });
    }

    $.fn.lgbTab = TabPlugin;
    $.fn.lgbTab.Constructor = Tab;
})(jQuery);

(function ($) {
    $.extend({
        bb_autoScrollItem: function (el, index) {
            var $el = $(el);
            var $menu = $el.find('.dropdown-menu');
            var maxHeight = parseInt($menu.css('max-height').replace('px', '')) / 2;
            var itemHeight = $menu.children('li:first').outerHeight();
            var height = itemHeight * index;
            var count = Math.floor(maxHeight / itemHeight);

            $menu.children().removeClass('active');
            var len = $menu.children().length;
            if (index < len) {
                $menu.children()[index].classList.add('active');
            }

            if (height > maxHeight) {
                $menu.scrollTop(itemHeight * (index > count ? index - count : index));
            }
            else if (index <= count) {
                $menu.scrollTop(0);
            }
        },
        bb_composition: function (el, obj, method) {
            var isInputZh = false;
            var $el = $(el);
            $el.on('compositionstart', function (e) {
                isInputZh = true;
            });
            $el.on('compositionend', function (e) {
                isInputZh = false;
            });
            $el.on('input', function (e) {
                if (isInputZh) {
                    e.stopPropagation();
                    e.preventDefault();
                    setTimeout(function () {
                        if (!isInputZh) {
                            obj.invokeMethodAsync(method, $el.val());
                        }
                    }, 15);
                }
            });
        },
        bb_setDebounce: function (el, waitMs) {
            // ReaZhuang贡献
            var $el = $(el);
            let timer;
            var allowKeys = ['ArrowUp', 'ArrowDown', 'Escape', 'Enter'];

            $el.on('keyup', function (event) {
                if (allowKeys.indexOf(event.key) < 1 && timer) {
                    // 清空计时器的方法
                    clearTimeout(timer);
                    // 阻止事件冒泡，使之不能进入到c#
                    event.stopPropagation();

                    // 创建一个计时器，开始倒计时，倒计时结束后执行内部的方法
                    timer = setTimeout(function () {
                        // 清除计时器，使下次事件不能进入到if中
                        timer = null;
                        // 手动激发冒泡事件
                        event.target.dispatchEvent(event.originalEvent);
                    }, waitMs);
                } else {
                    // 创建一个空的计时器，在倒计时期间内，接收的事件将全部进入到if中
                    timer = setTimeout(function () { }, waitMs);
                }
            });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_camera: function (el, obj, method, auto, videoWidth, videoHeight) {
            var $el = $(el);

            var stop = function (video, track) {
                video.pause();
                video.srcObject = null;
                track.stop();
            };

            if (method === 'stop') {
                var video = $el.find('video')[0];
                var track = $el.data('bb_video_track');
                if (track) {
                    stop(video, track);
                }
                return;
            }

            navigator.mediaDevices.enumerateDevices().then(function (videoInputDevices) {
                var videoInputs = videoInputDevices.filter(function (device) {
                    return device.kind === 'videoinput';
                });
                obj.invokeMethodAsync("InitDevices", videoInputs).then(() => {
                    if (auto && videoInputs.length > 0) {
                        $el.find('button[data-method="play"]').trigger('click');
                    }
                });

                // handler button click event
                var video = $el.find('video')[0];
                var canvas = $el.find('canvas')[0];
                canvas.width = videoWidth;
                canvas.height = videoHeight;
                var context = canvas.getContext('2d');
                var mediaStreamTrack;

                $el.on('click', 'button[data-method]', async function () {
                    var data_method = $(this).attr('data-method');
                    if (data_method === 'play') {
                        var front = $(this).attr('data-camera');
                        var deviceId = $el.find('.dropdown-item.active').attr('data-val');
                        var constrains = { video: { facingMode: front, width: videoWidth, height: videoHeight }, audio: false };
                        if (deviceId !== "") {
                            constrains.video.deviceId = { exact: deviceId };
                        }
                        navigator.mediaDevices.getUserMedia(constrains).then(stream => {
                            video.srcObject = stream;
                            video.play();
                            mediaStreamTrack = stream.getTracks()[0];
                            $el.data('bb_video_track', mediaStreamTrack);
                            obj.invokeMethodAsync("Start");
                        }).catch(err => {
                            console.log(err)
                            obj.invokeMethodAsync("GetError", err.message)
                        });
                    }
                    else if (data_method === 'stop') {
                        stop(video, mediaStreamTrack);
                        obj.invokeMethodAsync("Stop");
                    }
                    else if (data_method === 'capture') {
                        context.drawImage(video, 0, 0, videoWidth, videoHeight);
                        var url = canvas.toDataURL();
                        var maxLength = 30 * 1024;
                        while (url.length > maxLength) {
                            var data = url.substr(0, maxLength);
                            console.log(data);
                            await obj.invokeMethodAsync("Capture", data);
                            url = url.substr(data.length);
                        }

                        if (url.length > 0) {
                            await obj.invokeMethodAsync("Capture", url);
                        }
                        await obj.invokeMethodAsync("Capture", "__BB__%END%__BB__");
                    }
                });
            });
        }
    });
})(jQuery);

(function () {
    $.extend({
        bb_cascader_hide: function (el) {
            const dropdownEl = document.getElementById(el);
            const dropdown = bootstrap.Dropdown.getInstance(dropdownEl);
            dropdown.hide();
        }
    });
})();

(function ($) {
    $.extend({
        bb_collapse: function (el) {
            var $el = $(el);
            var parent = null;
            // check accordion
            if ($el.hasClass('is-accordion')) {
                parent = '[' + el.getAttributeNames().pop() + ']';
            }

            $.each($el.children('.accordion-item'), function () {
                var $item = $(this);
                var $body = $item.children('.accordion-collapse');
                var id = $body.attr('id');
                if (!id) {
                    id = $.getUID();
                    $body.attr('id', id);
                    if (parent != null) {
                        $body.attr('data-bs-parent', parent);
                    }

                    var $button = $item.find('[data-bs-toggle="collapse"]');
                    $button.attr('data-bs-target', '#' + id).attr('aria-controls', id);
                }
            });

            $el.find('.tree .tree-item > .fa').on('click', function (e) {
                var $parent = $(this).parent();
                $parent.find('[data-bs-toggle="collapse"]').trigger('click');
            });

            // support menu component
            if ($el.parent().hasClass('menu')) {
                $el.on('click', '.nav-link:not(.collapse)', function () {
                    var $this = $(this);
                    $el.find('.active').removeClass('active');
                    $this.addClass('active');

                    // parent
                    var $card = $this.closest('.accordion');
                    while ($card.length > 0) {
                        $card.children('.accordion-header').find('.nav-link').addClass('active');
                        $card = $card.parent().closest('.accordion');
                    }
                });
            }
        }
    });
})(jQuery);

(function ($) {
    Number.prototype.toRadians = function () {
        return this * Math.PI / 180;
    }

    $.extend({
        bb_geo_distance: function (latitude1, longitude1, latitude2, longitude2) {
            // R is the radius of the earth in kilometers
            var R = 6371;

            var deltaLatitude = (latitude2 - latitude1).toRadians();
            var deltaLongitude = (longitude2 - longitude1).toRadians();
            latitude1 = latitude1.toRadians();
            latitude2 = latitude2.toRadians();

            var a = Math.sin(deltaLatitude / 2) *
                Math.sin(deltaLatitude / 2) +
                Math.cos(latitude1) *
                Math.cos(latitude2) *
                Math.sin(deltaLongitude / 2) *
                Math.sin(deltaLongitude / 2);

            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = R * c;
            return d;
        },
        bb_geo_updateLocaltion: function (position, currentDistance = 0.0, totalDistance = 0.0, lastLat, lastLong) {
            // 纬度
            var latitude = position.coords.latitude;
            // 经度
            var longitude = position.coords.longitude;
            // 位置精度
            var accuracy = position.coords.accuracy;
            // 海拔高度
            var altitude = position.coords.altitude;
            // 位置的海拔精度
            var altitudeAccuracy = position.coords.altitudeAccuracy;
            // 方向，从正北开始以度计
            var heading = position.coords.heading;
            // 速度
            var speed = position.coords.speed;
            // 响应的日期/时间
            var timestamp = position.timestamp;

            // sanity test... don't calculate distance if accuracy
            // value too large
            if (accuracy >= 500) {
                console.warn("Need more accurate values to calculate distance.");
            }

            // calculate distance
            if (lastLat != null && lastLong != null) {
                currentDistance = $.bb_geo_distance(latitude, longitude, lastLat, lastLong);
                totalDistance += currentDistance;
            }

            lastLat = latitude;
            lastLong = longitude;

            if (altitude == null) {
                altitude = 0;
            }
            if (altitudeAccuracy == null) {
                altitudeAccuracy = 0;
            }
            if (heading == null) {
                heading = 0;
            }
            if (speed == null) {
                speed = 0;
            }
            return {
                latitude,
                longitude,
                accuracy,
                altitude,
                altitudeAccuracy,
                heading,
                speed,
                timestamp,
                currentDistance,
                totalDistance,
                lastLat,
                lastLong,
            };
        },
        bb_geo_handleLocationError: function (error) {
            switch (error.code) {
                case 0:
                    console.error("There was an error while retrieving your location: " + error.message);
                    break;
                case 1:
                    console.error("The user prevented this page from retrieving a location.");
                    break;
                case 2:
                    console.error("The browser was unable to determine your location: " + error.message);
                    break;
                case 3:
                    console.error("The browser timed out before retrieving the location.");
                    break;
            }
        },
        bb_geo_getCurrnetPosition: function (obj, method) {
            var ret = false;
            if (navigator.geolocation) {
                ret = true;
                navigator.geolocation.getCurrentPosition(position => {
                    var info = $.bb_geo_updateLocaltion(position);
                    obj.invokeMethodAsync(method, info);
                }, $.bb_geo_handleLocationError);
            }
            else {
                console.warn("HTML5 Geolocation is not supported in your browser");
            }
            return ret;
        },
        bb_geo_watchPosition: function (obj, method) {
            var id = 0;
            var currentDistance = 0.0;
            var totalDistance = 0.0;
            var lastLat;
            var lastLong;
            if (navigator.geolocation) {
                id = navigator.geolocation.watchPosition(position => {
                    var info = $.bb_geo_updateLocaltion(position, currentDistance, totalDistance, lastLat, lastLong);
                    currentDistance = info.currentDistance;
                    totalDistance = info.totalDistance;
                    lastLat = info.lastLat;
                    lastLong = info.lastLong;
                    obj.invokeMethodAsync(method, info);
                }, $.bb_geo_handleLocationError, {
                    maximumAge: 20000
                });
            }
            else {
                console.warn("HTML5 Geolocation is not supported in your browser");
            }
            return id;
        },
        bb_geo_clearWatchLocation: function (id) {
            var ret = false;
            if (navigator.geolocation) {
                ret = true;
                navigator.geolocation.clearWatch(id);
            }
            return ret;
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_gotop: function (el, target) {
            var $el = $(el);
            var tooltip = $el.tooltip();
            $el.on('click', function (e) {
                e.preventDefault();
                $(target || window).scrollTop(0);
                tooltip.tooltip('hide');
            });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_handwritten: function (el, obj, autoStop, method) {
            //当页面高度超过设备可见高度时，阻止掉touchmove事件。
            document.body.addEventListener('touchmove', function (e) {
                e.preventDefault(); //阻止默认的处理方式(阻止下拉滑动的效果)
            }, { passive: false }); //passive 参数不能省略，用来兼容ios和android

            new lineCanvas({
                el: el.getElementsByClassName("hw-body")[0], //绘制canvas的父级div
                clearEl: el.getElementsByClassName('btn-secondary')[0], //清除按钮
                saveEl: el.getElementsByClassName('btn-primary')[0], //保存按钮
                obj: obj
            });

            function lineCanvas(obj) {
                this.linewidth = 1;
                this.color = "#000000";
                this.background = "#fff";
                for (var i in obj) {
                    this[i] = obj[i];
                };
                this.canvas = document.createElement("canvas");
                this.el.appendChild(this.canvas);
                this.cxt = this.canvas.getContext("2d");
                this.canvas.clientTop = this.el.clientWidth;
                this.canvas.width = this.el.clientWidth;
                this.canvas.height = this.el.clientHeight;

                this.cxt.fillStyle = this.background;
                this.cxt.fillRect(2, 2, this.canvas.width, this.canvas.height);

                this.cxt.fillStyle = this.background;
                this.cxt.strokeStyle = this.color;
                this.cxt.lineWidth = this.linewidth;
                this.cxt.lineCap = "round";

                var isSupportTouch = 'ontouchend' in window;
                var that = this;
                var isStart = false;

                //开始绘制
                var hw_star = function (e) {
                    isStart = true;
                    this.cxt.beginPath();
                    var parentLeft = e.target.offsetParent.offsetLeft;
                    var parentTop = e.target.offsetParent.offsetTop;
                    if (isSupportTouch) {
                        this.cxt.moveTo(e.changedTouches[0].pageX + 2 - parentLeft, e.changedTouches[0].pageY + 2 - parentTop);
                    }
                    else {
                        this.cxt.moveTo(e.pageX + 2 - parentLeft, e.pageY + 2 - parentTop);
                    }
                };
                if (isSupportTouch) {
                    this.canvas.addEventListener("touchstart", hw_star.bind(this), false);
                }
                else {
                    this.canvas.addEventListener('mousedown', hw_star.bind(this), false);
                }

                //绘制中
                var hw_move = function (e) {
                    if (isStart) {
                        var parentLeft = e.target.offsetParent.offsetLeft;
                        var parentTop = e.target.offsetParent.offsetTop;
                        if (isSupportTouch) {
                            this.cxt.lineTo(e.changedTouches[0].pageX + 2 - parentLeft, e.changedTouches[0].pageY + 2 - parentTop);
                        }
                        else {
                            this.cxt.lineTo(e.pageX + 2 - parentLeft, e.pageY + 2 - parentTop);
                        }
                        this.cxt.stroke();
                    }
                };
                if (isSupportTouch) {
                    this.canvas.addEventListener("touchmove", hw_move.bind(this), false);
                }
                else {
                    this.canvas.addEventListener('mousedown', hw_move.bind(this), false);
                }

                //结束绘制
                var hw_end = function () {
                    isStart = false;
                    this.cxt.closePath();
                };
                if (isSupportTouch) {
                    this.canvas.addEventListener("touchend", hw_end.bind(this), false);
                }
                else {
                    this.canvas.addEventListener('mousedown', hw_end.bind(this), false);
                }

                //清除画布
                this.clearEl.addEventListener("click", function () {
                    this.cxt.clearRect(2, 2, this.canvas.width, this.canvas.height);
                }.bind(this), false);
                //保存图片，直接转base64
                this.saveEl.addEventListener("click", function () {
                    var imgBase64 = this.canvas.toDataURL();
                    //console.log(imgBase64);
                    return this.obj.invokeMethodAsync(method, imgBase64);
                }.bind(this), false);
            };
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_input: function (el, obj, enter, enterCallbackMethod, esc, escCallbackMethod) {
            var $el = $(el);
            $el.on('keyup', function (e) {
                if (enter && e.key === 'Enter') {
                    obj.invokeMethodAsync(enterCallbackMethod, $el.val());
                }
                else if (esc && e.key === 'Escape') {
                    obj.invokeMethodAsync(escCallbackMethod);
                }
            });
        },
        bb_input_selectAll_focus: function (el) {
            var $el = $(el);
            $el.on('focus', function () {
                $(this).select();
            });
        },
        bb_input_selectAll_enter: function (el) {
            var $el = $(el);
            $el.on('keyup', function (e) {
                if (e.key === 'Enter') {
                    $(this).select();
                }
            });
        },
        bb_input_selectAll: function (el) {
            $(el).select();
        }
    });
})(jQuery);

(function ($) {
    // private functions
    var backup = function (index) {
        var input = this;
        if (index !== undefined) {
            input.prevValues[index] = $($(input).find(".ipv4-cell")[index]).val();
        } else {
            $(input).find(".ipv4-cell").each(function (i, cell) {
                input.prevValues[i] = $(cell).val();
            });
        }
    };

    var revert = function (index) {
        var input = this;
        if (index !== undefined) {
            $($(input).find(".ipv4-cell")[index]).val(input.prevValues[index]);
        } else {
            $(input).find(".ipv4-cell").each(function (i, cell) {
                $(cell).val(input.prevValues);
            });
        }
    };

    var selectCell = function (index) {
        var input = this;
        if (index === undefined && index < 0 && index > 3) return;
        $($(input).find(".ipv4-cell")[index]).focus();
    };

    var isValidIPStr = function (ipString) {
        if (typeof ipString !== "string") return false;

        var ipStrArray = ipString.split(".");
        if (ipStrArray.length !== 4) return false;

        return ipStrArray.reduce(function (prev, cur) {
            if (prev === false || cur.length === 0) return false;
            return (Number(cur) >= 0 && Number(cur) <= 255) ? true : false;
        }, true);
    };

    var getCurIPStr = function () {
        var str = "";
        this.find(".ipv4-cell").each(function (index, element) {
            str += (index == 0) ? $(element).val() : "." + $(element).val();
        });
        return str;
    };

    // function for text input cell
    var getCursorPosition = function () {
        var cell = this;
        if ('selectionStart' in cell) {
            // Standard-compliant browsers
            return cell.selectionStart;
        } else if (document.selection) {
            // IE
            cell.focus();
            var sel = document.selection.createRange();
            var selLen = document.selection.createRange().text.length;
            sel.moveStart('character', -cell.value.length);
            return sel.text.length - selLen;
        }
        throw new Error("cell is not an input");
    };

    $.extend({
        bb_ipv4_input: function (ele) {
            var $ele = $(ele);
            ele.prevValues = [];

            $ele.find(".ipv4-cell").focus(function () {
                $(this).select(); // input select all when tab in
                $ele.toggleClass("selected", true);
            });

            $ele.find(".ipv4-cell").focusout(function () {
                $ele.toggleClass("selected", false);
            });

            $ele.find(".ipv4-cell").each(function (index, cell) {
                $(cell).keydown(function (e) {
                    if (e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode >= 96 && e.keyCode <= 105) {	// numbers, backup last status
                        backup.call(ele, index);
                    }
                    else if (e.keyCode == 37 || e.keyCode == 39) {	// left key ,right key
                        if (e.keyCode == 37 && getCursorPosition.call(cell) === 0) {
                            selectCell.call(ele, index - 1);
                            e.preventDefault();
                        }
                        else if (e.keyCode == 39 && getCursorPosition.call(cell) === $(cell).val().length) {
                            selectCell.call(ele, index + 1);
                            e.preventDefault();
                        }
                    }
                    else if (e.keyCode == 9) {	// allow tab
                    }
                    else if (e.keyCode == 8 || e.keyCode == 46) {	// allow backspace, delete
                    }
                    else {
                        e.preventDefault();
                    }
                });

                $(cell).keyup(function (e) {
                    if (e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode >= 96 && e.keyCode <= 105) {	// numbers
                        var val = $(this).val();
                        var num = Number(val);

                        if (num > 255)
                            revert.call(ele, index);
                        else if (val.length > 1 && val[0] === "0")
                            revert.call(ele, index);
                        else if (val.length === 3)
                            selectCell.call(ele, index + 1)
                    }
                    if (e.key === 'Backspace') {
                        if ($(this).val() === '') {
                            $(this).val('0');
                            var i = index - 1;
                            if (i > -1) {
                                selectCell.call(ele, i)
                            }
                            else {
                                selectCell.call(ele, 0)
                            }
                        }
                    }
                    if (e.key === '.') {
                        selectCell.call(ele, index + 1)
                    }
                    if (e.key === 'ArrowRight') {
                        selectCell.call(ele, index + 1)
                    }
                    if (e.key === 'ArrowLeft') {
                        selectCell.call(ele, index - 1)
                    }
                });
            });
        }
    });
}(jQuery));

(function(e){var t=function(t){var n=this;t!==undefined?n.prevValues[t]=e(e(n).find(".ipv4-cell")[t]).val():e(n).find(".ipv4-cell").each(function(t,r){n.prevValues[t]=e(r).val()})},n=function(t){var n=this;t!==undefined?e(e(n).find(".ipv4-cell")[t]).val(n.prevValues[t]):e(n).find(".ipv4-cell").each(function(t,r){e(r).val(n.prevValues)})},r=function(t){var n=this;if(t===undefined&&t<0&&t>3)return;e(e(n).find(".ipv4-cell")[t]).focus()},i=function(e){if(typeof e!="string")return!1;var t=e.split(".");return t.length!==4?!1:t.reduce(function(e,t){return e===!1||t.length===0?!1:Number(t)>=0&&Number(t)<=255?!0:!1},!0)},s=function(){var t="";return this.find(".ipv4-cell").each(function(n,r){t+=n==0?e(r).val():"."+e(r).val()}),t},o=function(){var e=this;if("selectionStart"in e)return e.selectionStart;if(document.selection){e.focus();var t=document.selection.createRange(),n=document.selection.createRange().text.length;return t.moveStart("character",-e.value.length),t.text.length-n}throw new Error("cell is not an input")};e.fn.ipv4_input=function(u,a){this.each(function(){if(e(this).hasClass("ipv4-input"))return;var i=this;i.prevValues=[],e(i).toggleClass("ipv4-input",!0),e(i).html('<input type="text" class="ipv4-cell" /><label class="ipv4-dot">.</label><input type="text" class="ipv4-cell" /><label class="ipv4-dot">.</label><input type="text" class="ipv4-cell" /><label class="ipv4-dot">.</label><input type="text" class="ipv4-cell" />'),e(this).find(".ipv4-cell").focus(function(){e(this).select(),e(i).toggleClass("selected",!0)}),e(this).find(".ipv4-cell").focusout(function(){e(i).toggleClass("selected",!1)}),e(this).find(".ipv4-cell").each(function(s,u){e(u).keydown(function(n){n.keyCode>=48&&n.keyCode<=57||n.keyCode>=96&&n.keyCode<=105?t.call(i,s):n.keyCode==37||n.keyCode==39?n.keyCode==37&&o.call(u)===0?(r.call(i,s-1),n.preventDefault()):n.keyCode==39&&o.call(u)===e(u).val().length&&(r.call(i,s+1),n.preventDefault()):n.keyCode!=9&&n.keyCode!=8&&n.keyCode!=46&&n.preventDefault()}),e(u).keyup(function(t){if(t.keyCode>=48&&t.keyCode<=57||t.keyCode>=96&&t.keyCode<=105){var o=e(this).val(),u=Number(o);u>255?n.call(i,s):o.length>1&&o[0]==="0"?n.call(i,s):o.length===3&&r.call(i,s+1)}})})});var f=function(t,n){t=="rwd"&&(n===undefined?this.toggleClass("rwd"):this.toggleClass("rwd",n));if(t=="value"){if(n===undefined)return s.call(this);if(!i(n))throw new Error("invalid ip address");var r=n.split(".");this.find(".ipv4-cell").each(function(t,n){e(n).val(r[t])})}return t=="valid"?i(s.call(this)):(t=="clear"&&this.find(".ipv4-cell").each(function(t,n){e(n).val("")}),this)},l=this;if(e.type(u)==="object"){var c=u;for(var h in c)f.call(this,h,u[h])}else l=f.call(this,u,a);return l}})(jQuery);
(function ($) {
    $.extend({
        bb_layout: function (refObj, method) {
            if (method === 'dispose') {
                $(window).off('resize');
                return;
            }
            var calcWindow = function () {
                var width = $(window).width();
                refObj.invokeMethodAsync(method, width);
            }

            $('.layout-header').find('[data-bs-toggle="tooltip"]').tooltip();

            calcWindow();

            $(window).on('resize', function () {
                calcWindow();
            });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_message: function (el, obj, method) {
            if (!window.Messages) window.Messages = [];
            Messages.push(el);

            var $el = $(el);
            var autoHide = $el.attr('data-autohide') === 'true';
            var delay = parseInt($el.attr('data-delay'));
            var autoHideHandler = null;

            var showHandler = window.setTimeout(function () {
                window.clearTimeout(showHandler);
                if (autoHide) {
                    // auto close
                    autoHideHandler = window.setTimeout(function () {
                        window.clearTimeout(autoHideHandler);
                        $el.close();
                    }, delay);
                }
                $el.addClass('show');
            }, 50);

            $el.close = function () {
                if (autoHideHandler != null) {
                    window.clearTimeout(autoHideHandler);
                }
                $el.removeClass('show');
                var hideHandler = window.setTimeout(function () {
                    window.clearTimeout(hideHandler);

                    // remove Id
                    Messages.remove(el);
                    if (Messages.length === 0) {
                        // call server method prepare remove dom
                        obj.invokeMethodAsync(method);
                    }
                }, 500);
            };

            $el.on('click', '.btn-close', function (e) {
                e.preventDefault();
                e.stopPropagation();

                $el.close();
            });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_notify_checkPermission: function (obj, method, requestPermission) {
            if ((!window.Notification && !navigator.mozNotification) || !window.FileReader || !window.history.pushState) {
                console.warn("Your browser does not support all features of this API");
                if (obj !== null && method !== '') obj.invokeMethodAsync(method, false);
            }
            else if (Notification.permission === "granted") {
                if (obj !== null && method !== '') obj.invokeMethodAsync(method, true);
            }
            else if (requestPermission && (Notification.permission !== 'denied' || Notification.permission === "default")) {
                Notification.requestPermission(function (permission) {
                    var granted = permission === "granted";
                    if (obj !== null && method !== '') obj.invokeMethodAsync(method, granted);
                });
            }
        },
        bb_notify_display: function (obj, method, model) {
            var ret = false;
            $.bb_notify_checkPermission(null, null, true);
            if (model.title !== null) {
                var onClickCallback = model.onClick;
                var options = {};
                if (model.message !== null) options.body = model.message.substr(0, 250);
                if (model.icon !== null) options.icon = model.icon;
                if (model.silent !== null) options.silent = model.silent;
                if (model.sound !== null) options.sound = model.sound;
                var notification = new Notification(model.title.substr(0, 100), options);
                if (obj !== null && onClickCallback !== null) {
                    notification.onclick = function (event) {
                        event.preventDefault();
                        obj.invokeMethodAsync(onClickCallback);
                    }
                }
                ret = true;
            }
            if (obj !== null && method !== null) obj.invokeMethodAsync(method, ret);
            return ret;
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_printview: function (el) {
            var $el = $(el);
            var $modalBody = $el.parentsUntil('.modal-content').parent().find('.modal-body');
            if ($modalBody.length > 0) {
                $modalBody.find(":text, :checkbox, :radio").each(function (index, el) {
                    var $el = $(el);
                    var id = $el.attr('id');
                    if (!id) {
                        $el.attr('id', $.getUID());
                    }
                });
                var printContenxt = $modalBody.html();
                var $body = $('body').addClass('bb-printview-open');
                var $dialog = $('<div></div>').addClass('bb-printview').html(printContenxt).appendTo($body);

                // assign value
                $dialog.find(":input").each(function (index, el) {
                    var $el = $(el);
                    var id = $el.attr('id');

                    if ($el.attr('type') === 'checkbox') {
                        $el.prop('checked', $('#' + id).prop('checked'));
                    }
                    else {
                        $el.val($('#' + id).val());
                    }
                });

                window.setTimeout(function () {
                    window.print();
                    $body.removeClass('bb-printview-open')
                    $dialog.remove();
                }, 50);
            }
            else {
                window.setTimeout(function () {
                    window.print();
                }, 50);
            }
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_reconnect: function () {
            var reconnectHandler = window.setInterval(function () {
                var $com = $('#components-reconnect-modal');
                if ($com.length > 0) {
                    var cls = $com.attr("class");
                    if (cls === 'components-reconnect-show') {
                        window.clearInterval(reconnectHandler);

                        async function attemptReload() {
                            await fetch('');
                            location.reload();
                        }
                        attemptReload();
                        setInterval(attemptReload, 5000);
                    }
                }
            }, 2000);
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_row: function (el) {
            var $el = $(el);
            $el.grid();
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_slider: function (el, slider, method) {
            var $slider = $(el);

            var isDisabled = $slider.find('.disabled').length > 0;
            if (!isDisabled) {
                var originX = 0;
                var curVal = 0;
                var newVal = 0;
                var slider_width = $slider.innerWidth();
                $slider.find('.slider-button-wrapper').drag(
                    function (e) {
                        slider_width = $slider.innerWidth();
                        originX = e.clientX || e.touches[0].clientX;
                        curVal = parseInt($slider.attr('aria-valuetext'));
                        $slider.find('.slider-button-wrapper, .slider-button').addClass('dragging');
                    },
                    function (e) {
                        var eventX = e.clientX || e.changedTouches[0].clientX;

                        newVal = Math.ceil((eventX - originX) * 100 / slider_width) + curVal;
                        if (isNaN(newVal)) newVal = 0;
                        if (newVal <= 0) newVal = 0;
                        if (newVal >= 100) newVal = 100;

                        $slider.find('.slider-bar').css({ "width": newVal.toString() + "%" });
                        $slider.find('.slider-button-wrapper').css({ "left": newVal.toString() + "%" });
                        $slider.attr('aria-valuetext', newVal.toString());
                        slider.invokeMethodAsync(method, newVal);
                    },
                    function (e) {
                        $slider.find('.slider-button-wrapper, .slider-button').removeClass('dragging');
                        slider.invokeMethodAsync(method, newVal);
                    });
            }
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_split: function (el) {
            var $split = $(el);

            var splitWidth = $split.innerWidth();
            var splitHeight = $split.innerHeight();
            var curVal = 0;
            var newVal = 0;
            var originX = 0;
            var originY = 0;
            var isVertical = !$split.children().hasClass('is-horizontal');

            $split.children().children('.split-bar').drag(
                function (e) {
                    splitWidth = $split.innerWidth();
                    splitHeight = $split.innerHeight();
                    if (isVertical) {
                        originY = e.clientY || e.touches[0].clientY;
                        curVal = $split.children().children('.split-left').innerHeight() * 100 / splitHeight;
                    }
                    else {
                        originX = e.clientX || e.touches[0].clientX;
                        curVal = $split.children().children('.split-left').innerWidth() * 100 / splitWidth;
                    }
                    $split.toggleClass('dragging');
                },
                function (e) {
                    if (isVertical) {
                        var eventY = e.clientY || e.changedTouches[0].clientY;
                        newVal = Math.ceil((eventY - originY) * 100 / splitHeight) + curVal;
                    }
                    else {
                        var eventX = e.clientX || e.changedTouches[0].clientX;
                        newVal = Math.ceil((eventX - originX) * 100 / splitWidth) + curVal;
                    }

                    if (newVal <= 0) newVal = 0;
                    if (newVal >= 100) newVal = 100;

                    $split.children().children('.split-left').css({ 'flex-basis': newVal.toString() + '%' });
                    $split.children().children('.split-right').css({ 'flex-basis': (100 - newVal).toString() + '%' });
                    $split.attr('data-split', newVal);
                },
                function (e) {
                    $split.toggleClass('dragging');
                });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_tab: function (el) {
            var $el = $(el);
            var handler = window.setInterval(function () {
                if ($el.is(':visible')) {
                    window.clearInterval(handler);
                    $el.lgbTab('active');
                }
            }, 200);
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_setTitle: function (title) {
            document.title = title;
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_transition: function (el, obj, method) {
            $(el).on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oAnimationEnd', function () {
                obj.invokeMethodAsync(method);
            });
        },
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_tree: function (el) {
            var $el = $(el);
            $el.find('.tree-content').hover(function () {
                $(this).parent().addClass('hover');
            }, function () {
                $(this).parent().removeClass('hover');
            });

            // 支持 Radio
            $el.on('click', '.tree-node', function () {
                var $node = $(this);
                var $prev = $node.prev();
                var $radio = $prev.find('[type="radio"]');
                if ($radio.attr('disabeld') !== 'disabled') {
                    $radio.trigger('click');
                }
            });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_tree_view: function (el) {
            var $el = $(el);
            $el.find('.tree-content').hover(function () {
                $(this).parent().addClass('hover');
            }, function () {
                $(this).parent().removeClass('hover');
            });

            // 支持 Radio
            $el.on('click', '.tree-node', function () {
                var $node = $(this);
                var $prev = $node.prev();
                var $radio = $prev.find('[type="radio"]');
                if ($radio.attr('disabeld') !== 'disabled') {
                    $radio.trigger('click');
                }
            });
        }
    });
})(jQuery);
