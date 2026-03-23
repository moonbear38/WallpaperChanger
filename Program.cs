using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

internal static class Program
{
    static NotifyIcon? tray;

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SystemParametersInfo(
        uint uiAction, uint uiParam, string pvParam, uint fWinIni);

    const uint SPI_SETDESKWALLPAPER = 20;
    const uint SPIF_UPDATEINIFILE = 0x01;
    const uint SPIF_SENDWININICHANGE = 0x02;

    class Config
    {
        public string folder { get; set; } = "";
        public int interval { get; set; } = 10;
        public bool shuffle { get; set; } = true;
    }

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var config = LoadConfig();
        if (!Directory.Exists(config.folder))
            return;

        config.folder = config.folder.TrimEnd('\\');

        int interval = Math.Max(1, config.interval);

        string[] images = LoadImages(config.folder);
        if (images.Length == 0)
            return;

        FileSystemWatcher watcher = new FileSystemWatcher(config.folder);
        watcher.NotifyFilter = NotifyFilters.FileName;
        watcher.Created += (_, __) => images = LoadImages(config.folder);
        watcher.Deleted += (_, __) => images = LoadImages(config.folder);
        watcher.EnableRaisingEvents = true;

        tray = new NotifyIcon();
        tray.Icon = new Icon("icon.ico");
        tray.Visible = true;
        tray.Text = "WallpaperChanger.exe";

        ContextMenuStrip menu = new ContextMenuStrip();
        menu.Items.Add("Exit", null, (s, e) =>
        {
            tray.Visible = false;
            Application.Exit();
        });
        tray.ContextMenuStrip = menu;

        Thread thread = new Thread(() =>
        {
            Random rand = new Random();
            int index = 0;

            List<string> shuffle = new List<string>();

            while (true)
            {
                string img;

                if (config.shuffle)
                {
                    if (shuffle.Count == 0)
                        shuffle = images.OrderBy(_ => rand.Next()).ToList();

                    img = shuffle[0];
                    shuffle.RemoveAt(0);
                }
                else
                {
                    if (images.Length == 1)
                        index = 0;

                    img = images[index++ % images.Length];
                }

                try
                {
                    SystemParametersInfo(
                        SPI_SETDESKWALLPAPER, 0, img,
                        SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
                }
                catch { }

                Thread.Sleep(interval * 1000);
            }
        });

        thread.IsBackground = true;
        thread.Start();

        Application.Run();
    }

    static string[] LoadImages(string folder)
    {
        return new[] { "*.jpg", "*.jpeg", "*.png", "*.bmp" }
            .SelectMany(p => Directory.GetFiles(folder, p))
            .OrderBy(f => f)
            .ToArray();
    }

    static Config LoadConfig()
    {
        const string file = "config.json";

        if (!File.Exists(file))
        {
            var defaultConfig = new Config
            {
                folder = "C:\\test\\Wallpapers",
                interval = 10,
                shuffle = true
            };

            File.WriteAllText(file,
                JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true }));

            return defaultConfig;
        }

        string json = File.ReadAllText(file);
        return JsonSerializer.Deserialize<Config>(json)!;
    }
}
