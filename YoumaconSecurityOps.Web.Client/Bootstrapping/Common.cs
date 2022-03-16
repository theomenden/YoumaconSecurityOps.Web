using System.Text.Json;
using System.Text.Json.Serialization;

namespace YoumaconSecurityOps.Web.Client.Bootstrapping;

public class Common
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}

