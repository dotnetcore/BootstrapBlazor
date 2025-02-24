
const { useState } = React;

export function TablePropertiesEdit(props) {
    const { content } = props;
    console.log(content);
    return React.createElement(
        'div',
        { className: 'bb-edit-content', style: { display: 'block' } },
        React.createElement(
            'h2',
            { className: 'bb-edit-title' },
            'Table Properties'
        ),
        React.createElement(
            'div',
            { className: 'show-row-wrapper' },
            React.createElement(
                'h3',
                null,
                'Channel'
            ),
            React.createElement(
                'select',
                { name: 'channel', id: 'tableChannel' },
                React.createElement('option', { hidden: '', value: '' }),
                React.createElement('option', { value: '<Selected Channel>' }, '<Selected Channel>'),
                React.createElement('option', { value: '<All Channels>' }, '<All Channels>'),
                React.createElement('option', { value: 'TCD1' }, 'TCD1')
            )
        )
    )
}
