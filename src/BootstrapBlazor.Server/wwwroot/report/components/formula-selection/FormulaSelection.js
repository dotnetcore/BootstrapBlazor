import { getCategoryList, getVariableList } from '../../api/getData.js';
import { setRange } from '../../api/sheetApis.js';
import { editVariableAndTable } from '../../api/reportApis.js'

const { useState, useEffect, createElement: c } = React;
export function FormulaSelection(props) {
    const { categoryList, closeDialog, cellData } = props;
    const { category, value: variable } = cellData.custom?.variable || {};
    const [ formula, setFormula ] = useState('');
    const [currentCategoryIndex, setCurrentCategoryIndex] = useState(findIndex(categoryList, item => item.value == category));
    const [variableList, setVariableList] = useState(getVariableList(categoryList[currentCategoryIndex]));
    const [ currentVariableIndex, setCurrentVariableIndex ] = useState(findIndex(variableList, item => item.value == variable));
    
    const handleCategoryClick = (category, index) => {
        return e => {
            if (currentCategoryIndex == index) return;
            setVariableList(getVariableList(category));
            setCurrentCategoryIndex(index);
            setCurrentVariableIndex(0);
        }   
    }
    const handleVariableClick = (variable, index) => {
        return e => {
            setCurrentVariableIndex(index);
        }
    }
    const handleInsertFormula = () => {
        const cc = categoryList[currentCategoryIndex]
        const cellData = {
            v: formula,
            custom: {
                variable: {
                    formula,
                    category: cc.value,
                    categoryDesc: cc.desc,
                    hasChannel: cc.hasChannel,
                    ...variableList[currentVariableIndex]
                },
            }
        }
        setRange({
            value: cellData,
            range: null,
            sheet: null,
        }, () => {
            closeDialog()
            const editEle = document.querySelector('.bb-edit-container')
            if (editEle) {
                editVariableAndTable({cellData})
            }
        })
    }

    useEffect(() => {
        const categoryIndex = findIndex(categoryList, item => item.value == category);
        setCurrentCategoryIndex(categoryIndex);

        const varList = getVariableList(categoryList[categoryIndex]);
        const varIndex = findIndex(varList, item => item.value == variable);
        setVariableList(varList);
        setCurrentVariableIndex(varIndex);
    }, [cellData.custom?.variable])

    useEffect(() => {
        setFormula(categoryList[currentCategoryIndex].value + '.' + variableList[currentVariableIndex]?.value)
    }, [currentCategoryIndex, currentVariableIndex])

    return c(
        'div',
        { className: 'bb-formula-selection' },
        c(
            'div',
            { },
            c(
                'ul',
                { className: 'bb-category-list' },
                ...categoryList.map((category, index) => {
                    return c(
                        'li',
                        { 
                            key: index,
                            className: currentCategoryIndex == index ? 'active' : '',
                            title: category.value,
                            onClick: handleCategoryClick(category, index),
                        },
                        category.value,
                        c('span', {}, category.desc)
                    )
                })
            ),
            c(
                'ul',
                { className: 'bb-variable-list' },
                ...variableList.map((variable, index) => {
                    return React.createElement(
                        'li',
                        {
                            key: index,
                            className: currentVariableIndex == index ? 'active' : '',
                            title: variable.value,
                            onClick: handleVariableClick(variable, index),
                            onDoubleClick: e => {
                                handleInsertFormula()
                            }
                        },
                        variable.value,
                        c('span', {}, variable.desc)
                    )
                })
            ),
        ),
        c(
            'input',
            { type: 'text', disabled: true, value: formula },
        ),
        c(
            'button',
            { onClick: handleInsertFormula },
            `${(cellData.custom?.variable || cellData.custom?.table) ? 'OK' : 'Insert'}`
        )
    )
           
}

function findIndex(arr, p) {
    const index = arr.findIndex(p);
    return index === -1 ? 0 : index;
}
