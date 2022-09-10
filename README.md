# glowies website
A Blazor app that serves as Oktay Comu's personal website.

## development
```bash
dotnet watch
```

## deployment
- Publish into desired directory.
```bash
dotnet publish -c Release -o dist
```

- Update sitemap.xml if you created a new page.

- Serve on a webserver.

local webserver example:
```bash
dotnet tool install --global dotnet-serve
dotnet serve -od dist/wwwroot
```

- Make sure all caching is disabled while testing deployment. (Set Cloudfare to dev mode)

**WARNING**: Make sure to set transfer type to **binary** in your FTP client so that characters are transferred correctly. Not setting this can lead to the `Failed to find a valid digest in the 'integrity' attribute` error.  

**WARNING**: .htaccess sometimes doesn't transfer as expected and has invalid characters. You might need to modify .htaccess directly from your hosting service.

## adding unity builds
- Copy over the WebGLTemplates folder into the Assets directory
- Use the Better2020 template
- Uncheck the "Show Splash Screen" checkbox
- Build the project to WebGL
- Edit the background color in `BUILD_DIR/TemplateData/style.css` to be #161616 