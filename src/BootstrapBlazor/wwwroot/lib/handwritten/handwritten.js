(function () {
    window.handwritten = {
        start: function (autostop, wrapper) {
            console.log('start handwritten');



            /**
             * 格式化日期.
             */
            Date.prototype.format = function (fmt) {
                var o = {
                    "M+": this.getMonth() + 1, //月份
                    "d+": this.getDate(), //日
                    "h+": this.getHours(), //小时
                    "m+": this.getMinutes(), //分
                    "s+": this.getSeconds(), //秒
                    "q+": Math.floor((this.getMonth() + 3) / 3), //季度
                    "S": this.getMilliseconds() //毫秒
                };
                if (/(y+)/.test(fmt)) {
                    fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
                }
                for (var k in o) {
                    if (new RegExp("(" + k + ")").test(fmt)) {
                        fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ?
                            (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                    }
                }
                return fmt;
            }

            /**
             * 获取URL参数
             */
            function getQueryString(name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
            /**
            * 是否数字
            */
            function isNumeric(n) {
                return !isNaN(parseFloat(n)) && isFinite(n);
            }

            function myRedirect(nextw) {
                event.returnValue = false;//加这句
                this.location.href = nextw;
            }
            //当页面高度超过设备可见高度时，阻止掉touchmove事件。
            document.body.addEventListener('touchmove', function (e) {
                e.preventDefault(); //阻止默认的处理方式(阻止下拉滑动的效果)
            }, { passive: false }); //passive 参数不能省略，用来兼容ios和android 


            new lineCanvas({
                el: document.getElementById("canvas"), //绘制canvas的父级div
                clearEl: document.getElementById("clearCanvas"), //清除按钮
                saveEl: document.getElementById("saveCanvas"), //保存按钮
                //      linewidth:1,//线条粗细，选填
                //      color:"black",//线条颜色，选填
                //      background:"#ffffff"//线条背景，选填
            });

            function lineCanvas(obj) {
                this.linewidth = 1;
                this.color = "#000000";
                this.background = "#ffffff";
                for (var i in obj) {
                    this[i] = obj[i];
                };
                this.canvas = document.createElement("canvas");
                this.el.appendChild(this.canvas);
                this.cxt = this.canvas.getContext("2d");
                this.canvas.width = this.el.clientWidth;
                this.canvas.height = this.el.clientHeight;

                this.cxt.fillStyle = this.background;
                this.cxt.fillRect(0, 0, this.canvas.width, this.canvas.height);

                //this.cxt.fillStyle = "red";
                //this.cxt.font = "16px verdana";
                //this.cxt.textAlign = "left";

                ////fillText("要添加的文字",x0坐标，y0坐标)
                //var orderedtime = new Date().getTime();
                //orderedtime = (new Date(orderedtime)).format("yyyy-MM-dd  hh:mm");
                //this.cxt.fillText(orderedtime, 30, 30);

                this.cxt.fillStyle = this.background;
                this.cxt.strokeStyle = this.color;
                this.cxt.lineWidth = this.linewidth;
                this.cxt.lineCap = "round";
                //开始绘制
                this.canvas.addEventListener("touchstart", function (e) {
                    this.cxt.beginPath();
                    this.cxt.moveTo(e.changedTouches[0].pageX, e.changedTouches[0].pageY);
                }.bind(this), false);
                //绘制中
                this.canvas.addEventListener("touchmove", function (e) {
                    this.cxt.lineTo(e.changedTouches[0].pageX, e.changedTouches[0].pageY);
                    this.cxt.stroke();
                }.bind(this), false);
                //结束绘制
                this.canvas.addEventListener("touchend", function () {
                    this.cxt.closePath();
                }.bind(this), false);
                //清除画布
                this.clearEl.addEventListener("click", function () {
                    this.cxt.clearRect(0, 0, this.canvas.width, this.canvas.height);
                }.bind(this), false);
                //保存图片，直接转base64
                this.saveEl.addEventListener("click", function () {
                    var imgBase64 = this.canvas.toDataURL();
                    //console.log(imgBase64);
                    return wrapper.invokeMethodAsync("invokeFromJS", imgBase64);
                }.bind(this), false);
                //添加日期时间
                function adddatetime() {
                    this.cxt.fillStyle = "red";
                    this.cxt.font = "12px '微软雅黑'";
                    this.cxt.textAlign = "left";
                    //fillText("要添加的文字",x0坐标，y0坐标)
                    var orderedtime = new Date().getTime();
                    orderedtime = (new Date(orderedtime)).format("yyyy-MM-dd  hh:mm");
                    this.cxt.strokeText(orderedtime, 50, 100);
                }
            };


        }
    };
})();