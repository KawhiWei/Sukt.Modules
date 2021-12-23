using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sukt.Module.Core.Extensions;

namespace Sukt.EntityFrameworkCore.ValueConversion
{
    public class JsonValueConverter<T> : ValueConverter<T, string> where T : class
    {
#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        public JsonValueConverter(ConverterMappingHints? hints = default) :
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
          base(v => v.Serialize(), v => v.Deserialize<T>(), hints)
        { }
    }
}
