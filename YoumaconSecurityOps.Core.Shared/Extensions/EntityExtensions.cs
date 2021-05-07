using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models;

namespace YoumaconSecurityOps.Core.Shared.Extensions
{
    public static class EntityExtensions
    {
        private static readonly JsonSerializerOptions DefaultSerializerOptions = new()
            {WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve, IgnoreNullValues = true, PropertyNameCaseInsensitive = true};

        public static string ToJson<T>(this T entity) where T : IEntity
        {
            return JsonSerializer.Serialize(entity, DefaultSerializerOptions);
        }

        public static string ToJson<T>(this T entity, JsonSerializerOptions jsonSerializerOptions) where T : IEntity
        {
            return JsonSerializer.Serialize(entity, jsonSerializerOptions);
        }
    }
}
