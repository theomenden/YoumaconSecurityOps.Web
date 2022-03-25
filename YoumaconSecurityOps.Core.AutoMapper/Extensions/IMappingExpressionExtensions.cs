

namespace YoumaconSecurityOps.Core.AutoMapper.Extensions
{
    public static class IMappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> MapThisPropertyTo<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression, Expression<Func<TSource>> source,
            Expression<Func<TDestination>> destination)
            where TSource : IEntity
            where TDestination: IEntity
        {
            return mappingExpression;
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }

    }
}
