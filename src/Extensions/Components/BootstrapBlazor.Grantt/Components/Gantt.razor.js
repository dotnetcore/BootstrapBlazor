import "../js/frappe-gantt.min.js"
import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import Data from '../../BootstrapBlazor/modules/data.js'

export async function init(id, tasks, view_mode, invoke) {
    var el = document.getElementById(id)
    if (el == null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.Gantt/css/frappe-gantt.min.css")
    var gantt = new Gantt(`#${id}`, tasks, {
        on_click: function (task) {
            task.dependencies = task.dependencies.toString()
            invoke.invokeMethodAsync("OnGanttClick", task)
        },
        on_date_change: function (task, start, end) {
            task.dependencies = task.dependencies.toString()
            invoke.invokeMethodAsync("OnGanttDataChange", task, start.toString(), end.toString())
        },
        on_progress_change: function (task, progress) {
            task.dependencies = task.dependencies.toString()
            invoke.invokeMethodAsync("OnGanttProgressChange", task, progress)
        },
        view_mode,
        language: "zh"
    });

    Data.set(id, gantt)
}

export function changeViewMode(id, view_mode) {
    const gantt = Data.get(id)
    if (gantt) {
        gantt.change_view_mode(view_mode)
    }
}
