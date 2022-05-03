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

**WARNING**: Make sure to use correct transfer type in FTP client so that ASCII characters are transferred correctly.

**WARNING**: .htaccess sometimes doesn't transfer as expected and has invalid characters. You might need to modify .htaccess directly from your hosting service.