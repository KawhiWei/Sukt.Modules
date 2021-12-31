namespace Sukt.Module.Core.DtoBases
{
    public interface IPageResult<T>
    {
        int Total { get; set; }
        T[] Data { get; set; }
    }
}
