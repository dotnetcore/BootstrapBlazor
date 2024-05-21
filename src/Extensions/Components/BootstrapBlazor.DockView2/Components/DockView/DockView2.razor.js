import "../../js/dockview-core.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'


class DefaultPanel {

    _element;
    get element() {
        return this._element;
    }

    constructor() {
        this._element = document.createElement('div');
    }

    init(params) {
        //
    }
}


export async function init(id, config) {
    await addLink("./_content/BootstrapBlazor.DockView2/css/dockview.css")

    let ele = document.getElementById(id);
    const dock = new dockview.DockviewComponent({
        components: {
            default: DefaultPanel,
        },
        parentElement: ele,
    });


    const panel1 = dock.addPanel({
        id: 'panel_1',
        title: 'Panel 1',
        component: 'default',
    });

    const panel2 = dock.addPanel({
        id: 'panel_2',
        title: 'Panel 2',
        component: 'default',
        position: {
            referencePanel: panel1,
            direction: 'right',
        },
    });

    const panel3 = dock.addPanel({
        id: 'panel_3',
        title: 'Panel 3',
        component: 'default',
        position: {
            referenceGroup: panel2.group,
        },
    });

    const pane4 = dock.addPanel({
        id: 'panel_4',
        title: 'Panel 4',
        component: 'default',
        position: {
            direction: 'below',
        },
    });

}
