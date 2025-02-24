
const { useState } = React;
export function Tabs(props) {
    // const [activeTabIndex, setActiveTabIndex] = useState(0);
    const { activeTabIndex, setActiveTabIndex } = props;
    const tabs = props.tabs || [];

    return React.createElement(
        'ul',
        { className: 'bb-edit-tabs' },
        ...tabs.map((tab, index) => React.createElement(
            'li',
            {
                className: activeTabIndex === index? 'active' : '',
                key: index,
                onClick: () => {
                    setActiveTabIndex(index);
                },
            },
            tab.title
        ))
        
    );
}
