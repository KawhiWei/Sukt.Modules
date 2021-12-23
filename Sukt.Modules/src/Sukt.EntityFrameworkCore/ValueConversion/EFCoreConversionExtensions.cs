using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core.Extensions;
using System;

namespace Sukt.EntityFrameworkCore.ValueConversion
{
    public static class EFCoreConversionExtensions
    {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class
        {
            propertyBuilder.HasConversion(new JsonValueConverter<T>())
                .Metadata.SetValueComparer(new JsonValueComparer<T>());

            return propertyBuilder;
        }
        public class JsonValueComparer<T> : ValueComparer<T>
        {
            public JsonValueComparer() : base((t1, t2) => DoEquals(t1, t2), t => DoGetHashCode(t), t => DoGetSnapshot(t))
            {
            }

            private static T DoGetSnapshot(T instance)
            {
                if (instance is ICloneable cloneable)
                    return (T)cloneable.Clone();
                return instance.Serialize().Deserialize<T>();
            }

            private static int DoGetHashCode(T instance)
            {
                if (instance is IEquatable<T>)
                    return instance.GetHashCode();

                return instance.Serialize().GetHashCode();
            }

#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
            private static bool DoEquals(T? left, T? right)
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
            {
                if (left is IEquatable<T> equatable)
                    return equatable.Equals(right);

                var result = left.Serialize().Equals(right.Serialize());
                return result;
            }
        }
    }
}
