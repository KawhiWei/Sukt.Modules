using System;

namespace Sukt.Module.Core.Domian
{
    /// <summary>
    /// 修改人和修改时间接口
    /// </summary>
    public interface IModifyAudited
    {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public void UpdateLastModifedAt();
    }
}