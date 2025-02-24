import { Tabs } from './Tabs.js'
import { VariableEdit } from './VariableEdit.js'
import { TableColumnEdit } from './TableColumnEdit.js'
import { TableFilterPeaks } from './TableFilterPeaks.js'
import { TableHeaderRowsEdit } from './TableHeaderRowsEdit.js'
import { TablePropertiesEdit } from './TablePropertiesEdit.js'

const { useState, useEffect, createElement: c } = React;
const componentMap = {
    VariableEdit,
    TableColumnEdit,
    TableFilterPeaks,
    TableHeaderRowsEdit,
    TablePropertiesEdit,
}

export function EditLayout(props) {
    const { cellData, tabs, contentList } = props
    const [activeTabIndex, setActiveTabIndex] = useState(0);
    const getContentComponent = (content) => componentMap[content.component];
    
    return c(
        'div',
        { className: 'bb-edit-container' },
        c(Tabs, { tabs, activeTabIndex, setActiveTabIndex }),
        c(
            getContentComponent(contentList[activeTabIndex]),
            {
                cellData
            }
        ),
    );
}
