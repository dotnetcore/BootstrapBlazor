
const { useState } = React;

export function TableFilterPeaks(props) {
    const { content } = props;
    console.log(content);
    return React.createElement(
        'div',
        { class: 'bb-edit-content filter-peaks', style: { display: 'block' } },
        React.createElement('h2', { className: 'bb-edit-title' }, 'Filter Peaks'),
        React.createElement(
            'div',
            { class: 'include-peaks' },
            React.createElement('h3', {}, 'Include Peaks'),
            React.createElement(
                'div',
                {},
                React.createElement('input', { type: 'checkbox', name: 'IdentifiedPeaks', id: 'IdentifiedPeaks' }),
                React.createElement('label', { htmlFor: 'IdentifiedPeaks' }, 'Identified Peaks')
            ),
            React.createElement(
                'div',
                {},
                React.createElement('input', { type: 'checkbox', name: 'UnidentifiedPeaks', id: 'UnidentifiedPeaks' }),
                React.createElement('label', { htmlFor: 'UnidentifiedPeaks' }, 'Unidentified Peaks')
            ),
            React.createElement(
                'div',
                {},
                React.createElement('input', { type: 'checkbox', name: 'UndetectedComponent', id: 'UndetectedComponent' }),
                React.createElement('label', { htmlFor: 'UndetectedComponent' }, 'Undetected Component')
            )
        ),
        React.createElement(
            'div',
            { class: 'custom-conditions mask' },
            React.createElement('h3', {}, 'Custom Conditions'),
            React.createElement(
                'div',
                {},
                React.createElement('input', { type: 'checkbox', name: 'CustomConditionsCheck', id: 'CustomConditionsCheck' }),
                React.createElement('label', { htmlFor: 'CustomConditionsCheck' }, 'Only include peaks that match the following conditions')
            ),
            React.createElement(
                'div',
                {},
                'Match ',
                React.createElement(
                    'select',
                    { className: 'match-type',  name: 'anyOrAll', id: 'anyOrAll' },
                    React.createElement('option', { value: 'ALL' }, 'ALL'),
                    React.createElement('option', { value: 'ANY' }, 'ANY')
                ),
                ' of the following rules:'
            ),
            React.createElement(
                'div',
                { id: 'validWrapper', class: 'bb-valid-wrapper' },
                React.createElement(
                    'ul',
                    {},
                    React.createElement(
                        'li',
                        {},
                        React.createElement('input', { type: 'text', class: 'variable', onfocus: () => fpFocusHandle(this) }),
                        React.createElement('select', { class: 'action', onchange: () => fpChangeHandle(this) }),
                        React.createElement('input', { type: 'text', class: 'valid-value', onblur: () => fpBlurHandle(this) }),
                        React.createElement('button', { class: 'addValid' }, '+'),
                        React.createElement('button', { class: 'deleteValid' }, '-')
                    )
                )
            ),
            React.createElement(
                'div',
                {},
                React.createElement('button', { id: 'addGroupBtn' }, 'and'),
                React.createElement('button', { style: { float: 'right', display: 'none' }, id: 'filterBtn' }, ' Submit filtering rules ')
            )
        )
    );
}

