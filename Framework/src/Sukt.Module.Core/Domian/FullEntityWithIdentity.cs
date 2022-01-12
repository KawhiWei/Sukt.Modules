using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Domian
{
    public class FullEntityWithIdentity : EntityWithIdentity<string>, IFullAudited
    {
        public FullEntityWithIdentity(string id) : base(id)
        {
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTimeOffset CreatedAt { get; private set; } = default!;
        [DisplayName("最后修改时间")]
        public DateTimeOffset? LastModifedAt { get; private set; } = default!;
        /// <summary>
        /// 删除时间
        /// </summary>
        [DisplayName("删除时间")]
        public DateTimeOffset? DeletionTime { get; private set; }
        public void UpdateCreatedAt()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateDeletion()
        {
            DeletionTime = DateTimeOffset.UtcNow;
        }

        public void UpdateLastModifedAt()
        {
            LastModifedAt = DateTimeOffset.UtcNow;
        }
    }
}
