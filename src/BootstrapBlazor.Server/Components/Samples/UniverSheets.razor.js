const beforeCreateUniver = (sheetName, options) => {
    console.log(sheetName, options);
}

const beforeCreateUniverSheet = (sheetName, workbookData) => {
    console.log(sheetName, workbookData);
}

if (window.BootstrapBlazor === void 0) {
    window.BootstrapBlazor = {};
}

if (window.BootstrapBlazor.Univer === void 0) {
    window.BootstrapBlazor.Univer = {};
}

if (window.BootstrapBlazor.Univer.Sheet === void 0) {
    window.BootstrapBlazor.Univer.Sheet = {
        callbacks: {
            beforeCreateUniver: beforeCreateUniver,
            beforeCreateUniverSheet: beforeCreateUniverSheet
        }
    }
}

export function init(id) {

}

export function dispose(id) {

}
