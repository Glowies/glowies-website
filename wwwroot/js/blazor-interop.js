function AddResizeListener(elementId, dotnetRef, callbackMethodName) {
    const resizeObserver = new ResizeObserver(entries => {
        for (let entry of entries) {
            var width = entry.contentRect.width;
            var height = entry.contentRect.height;
            dotnetRef.invokeMethodAsync(callbackMethodName, width, height);
        }
    });
    resizeObserver.observe(document.getElementById(elementId));
}