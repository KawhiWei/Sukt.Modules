using Sukt.Module.Core.Infrastructure;
using System;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Sukt.Module.Core.Extensions
{
    public static class ObjectExtension
    {
        public static string Serialize<T>(this T obj)
        {
            //https://q.cnblogs.com/q/115234/
            //https://github.com/dotnet/runtime/issues/28567
            var jsonOption = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            if (obj == null)
                return string.Empty;
            return JsonSerializer.Serialize(obj, jsonOption);
        }
        public static string Serialize<T>(this T obj, JsonSerializerOptions options)
        {
            if (obj == null)
                return string.Empty;
            return JsonSerializer.Serialize(obj, options);
        }

#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        public static T? Deserialize<T>(this string text)
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        {
            if (string.IsNullOrEmpty(text))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(text);
        }
#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        public static T? Deserialize<T>(this string text, JsonSerializerOptions options)
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        {
            if (string.IsNullOrEmpty(text))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(text, options);
        }

        public static T GetPropertyValue<T>(this object obj, string name)
        {
            var property = obj.GetType()
                              .GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var methodInfo = property?.GetGetMethod(true);

            if (methodInfo is null)
            {
                throw new Exception($"the '{name}' is not the property of {obj.GetType()}");
            }

            return (T)FastInvoke.GetMethodInvoker(methodInfo)(obj, null);
        }
    }
}
