

export const setRange = ({value}, setRangeAfter = () => {}) => {
    const sheet = univerAPI.getActiveWorkbook().getActiveSheet();
    const selectRange = sheet.getSelection().getActiveRange();
    console.log(value, 'value');
    selectRange.setValue(value);
    setRangeAfter();
}

export function setRichTextValue() {
    const range = univerAPI.getActiveWorkbook()
        .getActiveSheet()
        .getActiveRange();
    
    // 创建富文本并插入文本
    const richText = univerAPI.newRichText()
        .insertText('Hello\rWorld');
    
    // 设置富文本值
    range.setRichTextValueForCell(richText);
}

// setTimeout(setRichTextValue, 5000)