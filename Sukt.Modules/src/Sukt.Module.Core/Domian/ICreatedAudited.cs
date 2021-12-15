using System;

namespace Sukt.Module.Core.Domian
{
    /// <summary>
    /// 创建人和创建时间
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface ICreatedAudited<TUserKey> where TUserKey : struct
    {
        /// <summary>
        /// 创建人Ｉｄ
        /// </summary>
        TUserKey CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }
    }
}