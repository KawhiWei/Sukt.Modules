namespace Sukt.Module.Core.Domian
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}