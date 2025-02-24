
const { useState } = React;
export function TableHeaderRowsEdit(props) {
    const { content } = props;
    console.log(content);
    return React.createElement(
        'div',
        { className: 'bb-edit-content', style: { display: 'block' } },
        React.createElement('h2', { className: 'bb-edit-title' }, 'Header Rows'),
        React.createElement(
            'div',
            { className: 'show-row-wrapper' },
            React.createElement('h3', null, 'Show Rows'),
            React.createElement(
                'div',
                null,
                React.createElement('input', { type: 'checkbox', name: 'showChannel', id: 'showChannel' }),
                React.createElement('label', { htmlFor: 'showChannel' }, 'Channel')
            ),
            React.createElement(
                'div',
                null,
                React.createElement('input', { type: 'checkbox', name: 'showUnit', id: 'showUnit' }),
                React.createElement('label', { htmlFor: 'showUnit' }, 'Unit')
            )
        )
    );
}

