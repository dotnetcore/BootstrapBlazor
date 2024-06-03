import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import { cerateDockview } from '../js/dockview-utils.js'
import Data from '../../BootstrapBlazor/modules/data.js'

export async function init(id, invoke, options) {
    await addLink("./_content/BootstrapBlazor.DockView/css/dockview-bb.css")
    const el = document.getElementById(id);
    if (!el) {
        return;
    }

    options = {
        ...options,
        ...{
            gear: {
                show: true
            },
            rightControl: [
                {
                    name: 'lock',
                    icon: ['<i class="fas fa-unlock"></i>', '<i class="fas fa-lock"></i>']
                },
                {
                    name: 'packup/expand',
                    icon: ['<i class="fas fa-chevron-circle-up"></i>', '<i class="fas fa-chevron-circle-down"></i>']
                },
                {
                    name: 'float',
                    icon: ['<i class="far fa-window-restore"></i>']
                },
                {
                    name: 'maximize',
                    icon: ['<i class="fas fa-expand"></i>', '<i class="fas fa-compress"></i>']
                },
                {
                    name: 'close',
                    icon: ['<i class="fas fa-times"></i>']
                }
            ],
        }
    }

    const dockview = cerateDockview(el, options)
    Data.set(id, { el, dockview });

    dockview.on('initialized', () => {
        console.log('initialized');
    })
    dockview.on('lockChanged', isLock => {
        console.log(isLock, 'onLockChange');
    })
}

export function update(id, options) {
    const dock = Data.get(id)
    if (dock) {
        const { dockview } = dock;
        dockview.update(options);
    }
}

export function dispose(id) {
    const dock = Data.get(id)
    Data.remove(id);

    if (dock) {
        const { dockview } = dock;
        if (dockview) {
            dockview.dispose();
        }
    }
}
