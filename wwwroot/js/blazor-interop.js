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

function LoadCustomJavaScriptFile(jsPath, callback) {
    var customScript = document.createElement('script');
    customScript.setAttribute('type', 'text/javascript');
    customScript.setAttribute('src', jsPath);

    customScript.onreadystatechange = callback;
    customScript.onload = callback;

    document.head.appendChild(customScript);
}

function LoadUnityBuild(containerId, buildName) {
    var buildPath = "unity-builds/" + buildName;
    LoadCustomJavaScriptFile(buildPath + "/Build/UnityLoader.js", function () {
        var configPath = buildPath + "/Build/" + buildName + ".json";
        console.log(configPath);
        UnityLoader.instantiate(containerId, configPath);
    });
}