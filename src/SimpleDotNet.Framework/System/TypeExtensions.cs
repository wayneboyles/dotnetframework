using SimpleDotNet.Framework;

namespace System
{
    public static class TypeExtensions
    {
        public static string GetFullNameWithAssemblyName(this Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        public static bool IsAssignableTo<TTarget>(this Type type)
        {
            Check.NotNull(type, nameof(type));

            return type.IsAssignableTo(typeof(TTarget));
        }

        public static bool IsAssignableTo(this Type type, Type targetType)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(targetType, nameof(targetType));

            return targetType.IsAssignableFrom(type);
        }

        public static Type[] GetBaseClasses(this Type type, bool includeObject = true)
        {
            Check.NotNull(type, nameof(type));

            var types = new List<Type>();
            AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject);
            return types.ToArray();
        }

        public static Type[] GetBaseClasses(this Type type, Type stoppingType, bool includeObject = true)
        {
            Check.NotNull(type, nameof(type));

            var types = new List<Type>();
            AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject, stoppingType);
            return types.ToArray();
        }

        private static void AddTypeAndBaseTypesRecursively(List<Type> types, Type type, bool includeObject, Type stoppingType = null)
        {
            if (type == null || type == stoppingType)
            {
                return;
            }

            if (!includeObject && type == typeof(object))
            {
                return;
            }

            AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject, stoppingType);
            types.Add(type);
        }
    }
}
