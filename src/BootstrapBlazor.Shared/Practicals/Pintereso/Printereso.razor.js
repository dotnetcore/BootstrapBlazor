export function init(invoke, method) {
    window.onscroll = function () {
        var windowH = document.documentElement.clientHeight;//可视区窗口高度；
        var scrollH = document.documentElement.scrollTop || document.body.scrollTop;//滚动条的上边距；
        var documentH = document.documentElement.scrollHeight || document.body.scrollHeight;//滚动条的高度；
        var h1 = windowH + scrollH;
        var h2 = documentH;
        if (Math.abs(h1 - h2) < 50) {
            invoke.invokeMethodAsync(method);
            window.scrollTo(0, scrollH - 40);//把滚动条往上滚一点
        }
    }
}
