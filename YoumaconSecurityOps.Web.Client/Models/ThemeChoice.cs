namespace YoumaconSecurityOps.Web.Client.Models;
public class ThemeChoice : IEquatable<ThemeChoice>
{
    protected ThemeChoice(String name, Theme theme)
    {
        Name = name;
        Theme = theme;
    }

    private static readonly ThemeColorOptions DefaultColorOptions = new()
    {
        Primary = "#0d6efd",
        Secondary = "#6c757d",
        Success = "#198754",
        Info = "#0dcaf0",
        Link = "#0dcaf0",
        Warning = "#ffc107",
        Danger = "#dc3545",
        Light = "#f8f9fa",
        Dark = "#212529"
    };

    public String Name { get; }

    public Theme Theme { get; }

    public static ThemeChoice Wong { get; } = new(nameof(Wong), new()
    {
        ColorOptions = new()
        {
            Primary = "#332288",
            Secondary = "#555555",
            Info = "#44AA99",
            Success = "#117733",
            Warning = "#DDCC77",
            Danger = "#882255",
            Dark = "#000000",
            Light = "#ffffff",
            Link = "#56B4E9"
        },
        IsGradient = false,
        IsRounded = true,
        BackgroundOptions = new()
        {
            Primary = "#000"
        }
    });

    public static ThemeChoice Ibm { get; } = new(nameof(Ibm), new()
    {
        ColorOptions = new()
        {
            Primary = "#648FFF",
            Secondary = "#785EF0",
            Warning = "#FFB000",
            Danger = "#DC267F",
            Success = "#FE6100",
            Info = "#0dcaf0",
            Light = "#fff",
            Dark = "#000"
        },
        IsGradient = false,
        IsRounded = true,
        BackgroundOptions = new()
        {
            Primary = "#000"
        }
    });

    public static ThemeChoice Tol { get; } = new(nameof(Tol), new()
    {
        ColorOptions = new()
        {
            Primary = "#332288",
            Secondary = "#44AA99",
            Warning = "#DDCC77",
            Info = "#88CCEE",
            Danger = "#AA4499",
            Success = "#117733",
            Light = "#fff",
            Dark = "#000"
        },
        IsGradient = false,
        IsRounded = true,
        BackgroundOptions = new()
        {
            Primary = "#882255"
        }
    });

    public static ThemeChoice Light { get; } = new(nameof(Light), new()
    {
        ColorOptions = DefaultColorOptions,
        IsGradient = false,
        IsRounded = true,
        BackgroundOptions = new()
        {
            Primary = "#000"
        }
    });

    public static ThemeChoice Dark { get; } = new(nameof(Dark), new()
    {
        ColorOptions = DefaultColorOptions,
        IsGradient = false,
        IsRounded = true,
        BackgroundOptions = new()
        {
            Primary = "#999999"
        }
    });

    public static IEnumerable<ThemeChoice> GetAll()
    {
        return new List<ThemeChoice> { Wong, Ibm, Tol, Light, Dark };
    }

    public override string ToString() => Name;

    public bool Equals(ThemeChoice other)
    {
        return other is not null
            && (ReferenceEquals(this, other)
               || String.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase));
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj is ThemeChoice other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0;
    }

    public static bool operator ==(ThemeChoice left, ThemeChoice right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ThemeChoice left, ThemeChoice right)
    {
        return !Equals(left, right);
    }
}
