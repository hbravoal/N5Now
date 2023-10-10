using System.ComponentModel;

namespace N5.User.Infrastructure.Persistence.Utils
{
    public static class ReflectionUtils
    {
        public static T? GetDefault<T>(this T type) => default;

        public static bool IsGenericTypeOf(this Type type, Type genericTypeDefinition)
            => type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition;

        public static bool IsImplementationOfGenericType(this Type type, Type genericTypeDefinition)
        {
            if (!genericTypeDefinition.IsGenericTypeDefinition)
                return false;

            // looking for generic interface implementations
            if (genericTypeDefinition.IsInterface)
            {
                foreach (Type i in type.GetInterfaces())
                {
                    if (i.Name == genericTypeDefinition.Name && i.IsGenericTypeOf(genericTypeDefinition))
                        return true;
                }

                return false;
            }

            // looking for generic [base] types
            for (Type t = type; type != null; type = type?.BaseType)
            {
                if (t.Name == genericTypeDefinition.Name && t.IsGenericTypeOf(genericTypeDefinition))
                    return true;
            }

            return false;
        }

        public static object? Convert(this string input, Type type)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(type);
                if (converter != null)
                    return converter.ConvertFromString(input);
                return Activator.CreateInstance(type);
            }
            catch (NotSupportedException)
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}