export function bb_upload_drag_init(element, id) {
    var $el = $(element);
    $el.on('click', '.btn-browser', function (e) {
        var $this = $(this);
        var $file = $el.find(':file');
        $file.trigger('click');
    });

    var inputFile = document.getElementById(id);
    if (inputFile === null) {
        return;
    }

    //阻止浏览器默认行为
    document.addEventListener("dragleave", function (e) {
        e.preventDefault();
    });
    document.addEventListener("drop", function (e) {
        e.preventDefault();
    });
    document.addEventListener("dragenter", function (e) {
        e.preventDefault();
    });
    document.addEventListener("dragover", function (e) {
        e.preventDefault();
    });

    element.addEventListener("drop", function (e) {
        try {
            var fileList = e.dataTransfer.files; //获取文件对象
            //检测是否是拖拽文件到页面的操作
            if (fileList.length == 0) {
                return false;
            }

            inputFile.files = e.dataTransfer.files;
            const event = new Event('change', { bubbles: true });
            inputFile.dispatchEvent(event);
        }
        catch (e) {
            console.error(e);
        }
    });

    element.addEventListener('paste', function (e) {
        inputFile.files = e.clipboardData.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    });
}
