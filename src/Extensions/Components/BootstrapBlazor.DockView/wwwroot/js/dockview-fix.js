const fixObject = data => {
    if (!data) return null
    data.floatingGroups?.forEach(item => {
        let { width, height } = item.position
        item.position.width = width - 2
        item.position.height = height - 2
    });

    removeInvisibleBranch(data.grid.root)
    return data
}

const removeInvisibleBranch = branch => {
    if (branch.type === 'leaf') {
        if (branch.visible === false) {
            delete branch.visible
        }
    }
    else {
        branch.data.forEach(item => {
            removeInvisibleBranch(item)
        })
    }
}

export { fixObject };
