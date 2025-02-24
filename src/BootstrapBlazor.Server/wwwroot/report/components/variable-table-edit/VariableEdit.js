import { insertVariable } from '../../api/reportApis.js'

const { useState, useEffect, createElement: c } = React;
export function VariableEdit(props) {
    const { cellData } = props;
    const hasChannel = cellData.custom?.variable?.hasChannel;

    console.log(cellData, 'cellData666');
    // const [formula, setFormula] = useState(cellData.v);
    // useEffect(() => {
    //     setFormula(cellData.v || '');
    // }, [cellData.v]);
    return c(
        'div',
        { className: 'bb-edit-content' },
        c('h2', { className: 'bb-edit-title' }, 'Formula'),
        c(
            'div',
            null,
            c(
                'div',
                null,
                c('label', { htmlFor: 'variableFormula' }, 'Formula'),
                c('input', {
                    type: 'text',
                    autoComplete: 'off',
                    name: 'variableFormula',
                    id: 'variableFormula',
                    value: cellData.v || '',
                    onFocus: () => insertVariable({cellData}),
                })
            ),
            c(
                'div',
                { style: { display: 'none' } },
                c('label', { htmlFor: 'variableFormat' }, 'Format'),
                c('input', { type: 'text', name: 'variableFormat', id: 'variableFormat' })
            ),
            c(
                'div',
                { style: { display: hasChannel ? 'block' : 'none' } },
                c('label', { htmlFor: 'variableChannel' }, 'Channel'),
                c(
                    'select',
                    { name: 'variableChannel', id: 'variableChannel' },
                    c('option', { value: '<Selected Channel>' }, '<Selected Channel>'),
                    c('option', { value: 'FID(C6+)' }, 'FID(C6+)'),
                    c('option', { value: 'FID(C2-C6)' }, 'FID(C2-C6)')
                )
            ),
            c(
                'div',
                { style: { display: hasChannel ? 'block' : 'none' } },
                c('label', { htmlFor: 'variableCompoment' }, 'Compoment'),
                c(
                    'select',
                    { name: 'variableCompoment', id: 'variableCompoment' },
                    c('option', { value: 'X' }, 'X'),
                    c('option', { value: 'Y' }, 'Y'),
                    c('option', { value: 'Z' }, 'Z')
                )
            )
        )
    );
}
