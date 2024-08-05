import Data from "../../modules/data.js"
export function init(id, options) {
    options = {
        ...{
            onLoading: null,
        },
        ...options
    }
    var isLoading = false;
    const ob = new IntersectionObserver(
        async (entries) => {
            const entry = entries[0];
            if (entry.isIntersecting) {
                if (isLoading) {
                    return;
                }
                isLoading = true;
                await more();
                isLoading = false;
            }
        },
        {
            root: null,
            threshold: 0,
        }

    );
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }
    ob.observe(el);

    //执行C#中的方法加载数据
    async function more() {
      options.invoke.invokeMethodAsync(options.onLoading);
    }
    Data.set(id, { el, options });
}

export function dispose(id) {
    const loadMore = Data.get(id)
    if (loadMore) {
       //这里是否要移除obServer订阅
    }
}
