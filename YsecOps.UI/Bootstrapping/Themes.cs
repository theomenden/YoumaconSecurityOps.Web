using Blazorise;

namespace TheOmenDen.Host.Bootstrapping;

public static class Themes
{
    public static readonly Theme Default = new()
    {
        ColorOptions = new()
        {
            Primary = "#461dd3",
            Secondary = "#7a6fff",
            Success = "#00b300",
            Danger = "#ff3333",
            Warning = "#ff9933",
            Info = "#00cccc",
            Link = "#0000cc"
        },
        BackgroundOptions = new()
        {
            Body = "#cccccc",
            Light = "#fffff",
            Dark = "#7f7f7f"
        }
    };

    public static readonly Theme DarkMode = new()
    {
        ColorOptions = new()
        {
            Primary = "#461dd3",
            Secondary = "#7a6fff",
            Success = "#00b300",
            Danger = "#ff3333",
            Warning = "#ff9933",
            Info = "#00cccc",
            Link = "#9999ff"
        },
        BackgroundOptions = new()
        {
            Body = "#222222",
            Light = "#4e4e4e",
            Dark = "#171717"
        }
    };

    public static readonly Theme LightMode = new()
    {
        ColorOptions = new()
        {
            Primary = "#461dd3",
            Secondary = "#7a6fff",
            Success = "#00b300",
            Danger = "#ff3333",
            Warning = "#ff9933",
            Info = "#00cccc",
            Link = "#0000cc"
        },
        BackgroundOptions = new()
        {
            Body = "#cccccc",
            Light = "#fffff",
            Dark = "#7f7f7f"
        }
    };
}
