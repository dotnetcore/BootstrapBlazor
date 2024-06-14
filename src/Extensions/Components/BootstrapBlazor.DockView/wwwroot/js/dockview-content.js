export class DockviewPanelContent {
    constructor(option) {
        this.option = option
    }

    init(parameter) {
        const { params, api: { panel, accessor: { params: { template } } } } = parameter;
        const { titleClass, titleWidth, class: panelClass, key, title } = params;
        const { tab, content } = panel.view

        if (template) {
            this._element = key
                ? template.querySelector(`[data-bb-key=${key}]`)
                : (template.querySelector(`#${this.option.id}`) ?? template.querySelector(`[data-bb-title=${title}]`))
        }

        if (titleClass) {
            tab._content.classList.add(titleClass);
        }
        if (titleWidth) {
            tab._content.style.width = `${titleWidth}px`;
        }
        if (panelClass) {
            content.element.classList.add(panelClass);
        }
    }

    get element() {
        return this._element;
    }
}
