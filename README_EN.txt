Wallpaper Changer

A simple wallpaper slideshow application for Windows.

[Features]

- Runs in the system tray
- Automatically changes wallpapers from a specified folder
- Detects and applies changes when images are added or removed
- Supports shuffle order
- Settings managed via config.json
- Very lightweight and low CPU usage

[Supported Image Formats]

jpg,jpeg,png,bmp

[Requirements]

- Windows 10 / Windows 11
- .NET Desktop Runtime 10 (x64)

You can download the runtime from the official Microsoft website
 
https://dotnet.microsoft.com/download/dotnet/10.0

[How to Use]

1. Extract the ZIP file
2. Edit config.json to set the wallpaper folder and interval
3. Run WallpaperChanger.exe
4. The tray icon will appear
5. Right-click the tray icon to exit

Example config.json: (Displays images from "C:\test\Wallpapers"
in shuffle order every 10 seconds.)

{
  "folder": "C:\\test\\Wallpapers",
  "interval": 10,
  "shuffle": true
}

use "\\" instead of "\" when writing folder paths in the JSON file.

[License]

MIT License