using System.Collections.Generic;

namespace Sukt.Module.Core.DtoBases
{
    /// <summary>
    /// 分页同一返回继承
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageBaseResult<T>: IPageResult<T>
    {
        public PageBaseResult(int total, T[] data)
        {
            Total = total;
            Data = data;
        }

        public int Total { get; set; }
        public T[] Data { get; set; }
    }
}
