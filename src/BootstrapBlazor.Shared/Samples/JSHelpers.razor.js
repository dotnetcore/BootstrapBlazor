import Data from '../../../../../_content/BootstrapBlazor/modules/data.js'

export function test1() {
    Data.set("test", "BootstrapBlazor")
}

export function test2() {
    const res = Data.get("test")
    return res;
}
