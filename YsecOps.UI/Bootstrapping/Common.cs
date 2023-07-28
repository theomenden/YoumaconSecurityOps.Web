using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheOmenDen.Host.Bootstrapping;
public static class Common
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            new EnumerationNameConverter<EventIdIdentifier, Int32>(),
        },
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static readonly String[] MobileProviders = {
        "android",
        "webos",
        "iphone",
        "ipad",
        "ipod",
        "blackberry",
        "mobile",
        "opera mini"
    };
}