export function copy(id) {
    const el = document.querySelector(id);
    bootstrap.EventHandler.on(el, 'click', function () {
        const text = this.previousElementSibling.querySelector('code').textContent;
        bb.Utility.copy(text);

        const tooltip = bb.Utility.getDescribedElement(this);
        tooltip.querySelector('.tooltip-inner').innerHTML = '拷贝代码成功';
    });
}

export function dispose(id) {
    const el = document.querySelector(id);
    bootstrap.EventHandler.off(el, 'click');
}
