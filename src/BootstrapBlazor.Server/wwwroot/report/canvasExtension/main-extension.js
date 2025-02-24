const { DEFAULT_FONTFACE_PLANE, FIX_ONE_PIXEL_BLUR_OFFSET, getColor, MIDDLE_CELL_POS_MAGIC_NUMBER, SheetExtension } = UniverEngineRender

const UNIQUE_KEY = 'MainCustomExtension'
// const customEmojiList = ['🥳']
export class MainCustomExtension extends SheetExtension {
    uKey = UNIQUE_KEY

    // Must be greater than 50
    get zIndex() {
        return 50
    }

    draw(ctx, _parentScale, spreadsheetSkeleton) {
        const { rowColumnSegment } = spreadsheetSkeleton
        const { startRow, endRow, startColumn, endColumn } = rowColumnSegment
        if (!spreadsheetSkeleton) {
            return
        }

        // Only displayed on the specified sheet
        if (spreadsheetSkeleton.worksheet.getSheetId() !== 'sheet1') {
            // return
        }

        const { rowHeightAccumulation, columnTotalWidth, columnWidthAccumulation, rowTotalHeight, _cellData } = spreadsheetSkeleton
        const sheetData = _cellData.getData()
        if (
            !rowHeightAccumulation
            || !columnWidthAccumulation
            || columnTotalWidth === undefined
            || rowTotalHeight === undefined
        ) {
            return
        }

        ctx.fillStyle = getColor([248, 249, 250])
        ctx.textAlign = 'center'
        ctx.textBaseline = 'middle'
        ctx.fillStyle = getColor([0, 0, 0])
        ctx.beginPath()
        ctx.lineWidth = 1

        ctx.translateWithPrecisionRatio(FIX_ONE_PIXEL_BLUR_OFFSET, FIX_ONE_PIXEL_BLUR_OFFSET)

        ctx.strokeStyle = getColor([217, 217, 217])
        ctx.font = `13px ${DEFAULT_FONTFACE_PLANE}`
        let preRowPosition = 0
        const rowHeightAccumulationLength = rowHeightAccumulation.length
        for (let r = startRow - 1; r <= endRow; r++) {
            if (r < 0 || r > rowHeightAccumulationLength - 1) {
                continue
            }
            const rowEndPosition = rowHeightAccumulation[r]
            if (preRowPosition === rowEndPosition) {
                // Skip hidden rows
                continue
            }

            let preColumnPosition = 0
            const columnWidthAccumulationLength = columnWidthAccumulation.length
            for (let c = startColumn - 1; c <= endColumn; c++) {
                if (c < 0 || c > columnWidthAccumulationLength - 1) {
                    continue
                }

                const columnEndPosition = columnWidthAccumulation[c]
                if (preColumnPosition === columnEndPosition) {
                    // Skip hidden columns
                    continue
                }

                // painting cell background
                if (sheetData[r]?.[c]?.custom?.variable) {
                    ctx.fillStyle = '#e4393c'
                    strokeTriangle(ctx, preColumnPosition, preRowPosition, columnEndPosition, rowEndPosition)
                }

                // painting cell text
                // const middleCellPosX = preColumnPosition + (columnEndPosition - preColumnPosition) / 2
                // const middleCellPosY = preRowPosition + (rowEndPosition - preRowPosition) / 2
                // customEmojiList[c] && ctx.fillText(customEmojiList[c], middleCellPosX + 20, middleCellPosY + MIDDLE_CELL_POS_MAGIC_NUMBER) // Magic number 1, because the vertical alignment appears to be off by 1 pixel
                
                preColumnPosition = columnEndPosition
            }

            preRowPosition = rowEndPosition
        }
        ctx.fill()
    }
}

// 画三角形
function strokeTriangle(ctx, str, stc, endr, endc) {
    ctx.moveTo(endr, stc)
    ctx.lineTo(endr - 6, stc)
    ctx.lineTo(endr, stc + 6)
    ctx.lineTo(endr, stc)
}
  