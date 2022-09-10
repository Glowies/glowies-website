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
    if (typeof instanceMap === "undefined") {
        instanceMap = {}
    }

    var buildPath = "unity-builds/" + buildName + "/";
    var loaderPath = buildPath + "Build/" + buildName + ".loader.js";
    LoadCustomJavaScriptFile(loaderPath, function () {
        // Quit old instance if it already exists
        if (instanceMap.hasOwnProperty(containerId)) {
            instanceMap[containerId].Quit();
        }

        instanceMap[containerId] = createUnityInstance(document.querySelector("#" + containerId), {
            dataUrl: buildPath + "Build/seed-renderer.data",
            frameworkUrl: buildPath + "Build/seed-renderer.framework.js",
            codeUrl: buildPath + "Build/seed-renderer.wasm",
            streamingAssetsUrl: buildPath + "StreamingAssets",
            companyName: "DefaultCompany",
            productName: "SeedSharpRenderer",
            productVersion: "0.1",
            matchWebGLToCanvasSize: false, // Uncomment this to separately control WebGL canvas render size and DOM element size.
            // devicePixelRatio: 1, // Uncomment this to override low DPI rendering on high DPI displays.
        });
    });
}

async function UnloadUnityBuild(containerId) {
    if (typeof instanceMap === "undefined") {
        return;
    }

    if (instanceMap.hasOwnProperty(containerId)) {
        await instanceMap[containerId].Quit();
        
    }
}