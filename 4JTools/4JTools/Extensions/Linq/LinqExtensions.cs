using System.Linq.Expressions;
using System.Reflection;

namespace _4JTools.Extensions.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source,
            IDictionary<string, string> orderConditions)
        {
            return source.AsQueryable().OrderBy(orderConditions);
        }

        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source,
            IDictionary<string, string> orderConditions)
        {
            var result = source;

            if (orderConditions?.Any() != true)
            {
                return result;
            }

            var first = true;

            foreach (var item in orderConditions)
            {
                result = result.ApplyOrder(item.Key, item.Value, first);
                first = false;
            }

            return result;
        }

        private static IOrderedQueryable<TSource> ApplyOrder<TSource>(this IQueryable<TSource> source, string property, string direction, bool first)
        {
            var props = property.Split('.');

            var orderMethod = first ? "OrderBy" : "ThenBy";

            if (direction.Equals("DESC", StringComparison.CurrentCultureIgnoreCase))
            {
                orderMethod += "Descending";
            }

            var type = typeof(TSource);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            foreach (var prop in props)
            {
                var pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (pi == null)
                {
                    return (IOrderedQueryable<TSource>)source;
                }

                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(TSource), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods()
                .Single(method => method.Name == orderMethod && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TSource), type)
                .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<TSource>)result;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                return;
            }

            foreach (var item in source)
            {
                action.Invoke(item);
            }
        }
    }
}
