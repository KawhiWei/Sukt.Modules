using System;
using System.ComponentModel;

namespace Sukt.Module.Core.Domian
{
    public class FullAggregateRootWithIdentity : AggregateRootWithIdentity<string>, IFullAudited
    {
        public FullAggregateRootWithIdentity(string id) : base(id)
        {
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTimeOffset CreatedAt { get; private set; } = default!;
        [DisplayName("最后修改时间")]
        public DateTimeOffset? LastModifedAt { get; private set; } = default!;
        public bool IsDeleted { get; set; }

        public void UpdateCreatedAt()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateLastModifedAt()
        {
            LastModifedAt = DateTimeOffset.UtcNow;
        }
    }
}
