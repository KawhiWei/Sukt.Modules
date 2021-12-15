namespace Sukt.Module.Core.DtoBases
{
    public interface IOutputDto
    {
    }

    /// <summary>
    /// 实现DTO接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class OutputDtoBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
