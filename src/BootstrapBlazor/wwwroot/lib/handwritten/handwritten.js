(function () {
    window.handwrittenv2 = {
        start: function (autostop, wrapper) {
            console.log('start handwritten v2');

            //当页面高度超过设备可见高度时，阻止掉touchmove事件。
            document.body.addEventListener('touchmove', function (e) {
                e.preventDefault(); //阻止默认的处理方式(阻止下拉滑动的效果)
            }, { passive: false }); //passive 参数不能省略，用来兼容ios和android 


            new lineCanvas({
                el: document.getElementById("canvas"), //绘制canvas的父级div
                clearEl: document.getElementById("clearCanvas"), //清除按钮
                saveEl: document.getElementById("saveCanvas"), //保存按钮
            });

            function lineCanvas(obj) {
                this.linewidth = 1;
                this.color = "#000000";
                this.background = "#22ffff";
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
 
                //开始绘制
                this.canvas.addEventListener("touchstart", function (e) {
                    this.cxt.beginPath();
                    var parentLeft = e.target.offsetParent.offsetLeft;
                    var parentTop = e.target.offsetParent.offsetTop; 
                    this.cxt.moveTo(e.changedTouches[0].pageX + 2 - parentLeft, e.changedTouches[0].pageY + 2 - parentTop );
                }.bind(this), false);
                //绘制中
                this.canvas.addEventListener("touchmove", function (e) {
                    var parentLeft = e.target.offsetParent.offsetLeft;
                    var parentTop = e.target.offsetParent.offsetTop;
                    this.cxt.lineTo(e.changedTouches[0].pageX + 2 - parentLeft, e.changedTouches[0].pageY + 2 - parentTop );
                    this.cxt.stroke();
                }.bind(this), false);
                //结束绘制
                this.canvas.addEventListener("touchend", function () {
                    this.cxt.closePath();
                }.bind(this), false);
                //清除画布
                this.clearEl.addEventListener("click", function () {
                    this.cxt.clearRect(2, 2, this.canvas.width, this.canvas.height);
                }.bind(this), false);
                //保存图片，直接转base64
                this.saveEl.addEventListener("click", function () {
                    var imgBase64 = this.canvas.toDataURL();
                    //console.log(imgBase64);
                    return wrapper.invokeMethodAsync("invokeFromJS", imgBase64);
                }.bind(this), false);
            };


        }
    };
})();
