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
            {WriteIndented = true,  IgnoreNullValues = true, PropertyNameCaseInsensitive = true};

        /// <summary>
        /// Converts the entity supplied from <typeparamref name="T"/> to a serialized JSON <see cref="string"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns><c>JSON</c> if the <paramref name="entity"/> contains data, <c>String.Empty</c> if it contains no data</returns>
        /// <remarks><para>Uses default <see cref="JsonSerializerOptions"/></para>
        /// <para><typeparamref name="T" /> must implement <see cref="IEntity"/></para>
        /// </remarks>
        public static string ToJson<T>(this T entity) where T : IEntity
        {
            return JsonSerializer.Serialize(entity, DefaultSerializerOptions);
        }

        /// <summary>
        /// Converts the entity supplied from <typeparamref name="T"/> to a serialized JSON <see cref="string"/> using supplied <paramref name="jsonSerializerOptions"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="jsonSerializerOptions"/>
        /// <returns><c>JSON</c> if the <paramref name="entity"/> contains data, <c>String.Empty</c> if it contains no data</returns>
        /// <remarks>
        /// <para>Review: <see cref="JsonSerializerOptions"/></para>
        /// <para><typeparamref name="T" /> must implement <see cref="IEntity"/></para>
        /// </remarks>
        public static string ToJson<T>(this T entity, JsonSerializerOptions jsonSerializerOptions) where T : IEntity
        {
            return JsonSerializer.Serialize(entity, jsonSerializerOptions);
        }
    }
}
