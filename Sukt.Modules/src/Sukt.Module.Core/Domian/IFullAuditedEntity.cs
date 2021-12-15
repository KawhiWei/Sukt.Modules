namespace Sukt.Module.Core.Domian
{
    public interface IFullAuditedEntity<TPrimaryKey> : ICreatedAudited<TPrimaryKey>, IModifyAudited<TPrimaryKey>, ISoftDelete where TPrimaryKey : struct
    {
    }
}