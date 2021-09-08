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
