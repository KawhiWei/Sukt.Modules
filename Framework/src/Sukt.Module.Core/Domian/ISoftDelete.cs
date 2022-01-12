namespace Sukt.Module.Core.Domian
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public interface ISoftDelete
    {
        public void UpdateDeletion();
    }
}