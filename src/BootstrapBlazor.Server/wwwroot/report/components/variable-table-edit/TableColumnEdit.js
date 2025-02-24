
const { useState } = React;
export function TableColumnEdit(props) {
    const { content } = props;
    console.log(content);
    return React.createElement(
        'div',
        { className: 'bb-edit-content', style: { display: 'block' } },
        React.createElement(
            'h2',
            { className: 'bb-edit-title' },
            'Report Column'
        ),
        React.createElement(
            'div',
            { className: 'management' },
            React.createElement(
                'h3',
                null,
                'Column Management'
            ),
            React.createElement(
                'div',
                null,
                React.createElement(
                    'button',
                    { id: 'addColumn', title: 'Insert a new column' },
                    React.createElement(
                        'svg',
                        { t: '1713868541570', className: 'icon', viewBox: '0 0 1024 1024', version: '1.1', xmlns: 'http://www.w3.org/2000/svg', pId: '3010', width: '19', height: '19' },
                        React.createElement(
                            'path',
                            { d: 'M906.6124654 484.96676768H747.99379616V114.87697753h158.61866923v370.08979015M959.52623131 537.79469433V62H695.12908112v475.79469433zM589.38738942 960.85956464h52.86471417V220.61866924H377.89174265V960.85956464h211.49564677zM325.0270276 220.61866924H60.66666667V960.85956464h52.87697754V273.48338427h158.60640674V960.85956464h52.87697665zM854.27504928 960.85956464l115.78488398-115.79714735L932.54818112 807.48935175l-78.77590489 78.77590489v-242.67981973h-52.90150253v242.67981973l-78.77590576-78.77590489-37.54853965 37.53627716L800.34347477 960.85956464l0.52729893-0.52729891v0.52729891h52.87697666v-0.52729891z', fill: '#333333', pId: '3011' },
                            null
                        )
                    )
                ),
                React.createElement(
                    'button',
                    { id: 'deleteColumn', title: 'Remove the selected column' },
                    React.createElement(
                        'svg',
                        { t: '1713868654744', className: 'icon', viewBox: '0 0 1024 1024', version: '1.1', xmlns: 'http://www.w3.org/2000/svg', pId: '3728', width: '20', height: '20' },
                        React.createElement(
                            'path',
                            { d: 'M371.4048 544.88576h286.12096V29.88032H371.4048v515.00544z m228.8896-457.79456v400.56832h-171.6736V87.0912h171.6736zM514.46272 900.2496l102.43072 102.43072h37.77024l2.86208-2.86208V962.048l-102.43072-102.43072 102.43072-102.43584v-37.76512l-2.86208-2.86208h-37.77024l-102.43072 102.43072-102.43072-102.43072h-37.77024l-2.86208 2.86208v37.76512l102.43072 102.43584L371.4048 962.048v37.77024l2.86208 2.86208h37.77024l102.4256-102.43072z m0 0', fill: '#09958D', pId: '3729' },
                            null
                        ),
                        React.createElement(
                            'path',
                            { d: 'M85.28384 258.7648h171.6736v743.91552h57.22112V201.53856H28.0576v801.14176h57.22624zM714.752 1002.68032h57.216V258.7648h171.6736v743.91552h57.216V201.53856H714.752z', fill: '#4A4B4B', pId: '3730' },
                            null
                        ),
                        React.createElement(
                            'path',
                            { d: 'M657.52064 719.42144v-2.86208h-2.85696zM657.52064 1002.68032v-2.86208l-2.85696 2.86208zM371.4048 1002.68032h2.86208l-2.86208-2.86208zM371.4048 719.42144l2.86208-2.86208h-2.86208z', fill: '#444A5C', pId: '3731' },
                            null
                        )
                    )
                )
            )
        ),
        React.createElement(
            'div',
            null,
            React.createElement(
                'h3',
                null,
                'Column Properties'
            ),
            React.createElement(
                'div',
                null,
                React.createElement(
                    'label',
                    { htmlFor: 'variableFormula' },
                    'Formula'
                ),
                React.createElement(
                    'input',
                    { type: 'text', autoComplete: 'off', name: 'variableFormula', id: 'variableFormula' },
                    null
                )
            ),
            React.createElement(
                'div',
                null,
                React.createElement(
                    'label',
                    { htmlFor: 'columnHeader' },
                    'Header'
                ),
                React.createElement(
                    'input',
                    { type: 'text', name: 'Header', id: 'columnHeader' },
                    null
                )
            ),
            React.createElement(
                'div',
                null,
                React.createElement(
                    'label',
                    { htmlFor: 'columnUnit' },
                    'Unit'
                ),
                React.createElement(
                    'input',
                    { type: 'text', name: 'Unit', id: 'columnUnit' },
                    null
                )
            ),
            React.createElement(
                'div',
                { style: { display: 'none' } },
                React.createElement(
                    'label',
                    { htmlFor: 'columnFormat' },
                    'Format'
                ),
                React.createElement(
                    'input',
                    { type: 'text', name: 'Format', id: 'columnFormat' },
                    null
                )
            ),
            React.createElement(
                'div',
                { style: { display: 'block' } },
                React.createElement(
                    'label',
                    { htmlFor: 'variableChannel' },
                    'Channel'
                ),
                React.createElement(
                    'select',
                    { name: 'channel', id: 'variableChannel' },
                    React.createElement(
                        'option',
                        { value: '<Selected Channel>' },
                        '<Selected Channel>'
                    ),
                    React.createElement(
                        'option',
                        { value: '<All Channels>' },
                        '<All Channels>'
                    ),
                    React.createElement(
                        'option',
                        { value: 'TCD1' },
                        'TCD1'
                    )
                )
            )
        )
    );
}
