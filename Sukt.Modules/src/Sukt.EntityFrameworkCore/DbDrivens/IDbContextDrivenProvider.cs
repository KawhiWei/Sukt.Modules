using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Entity;

namespace Sukt.EntityFrameworkCore
{
    /// <summary>
    /// 上下文驱动提供者
    /// </summary>
    public interface IDbContextDrivenProvider
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        DBType DatabaseType { get; }

        /// <summary>
        /// 构建数据库驱动
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>

        DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder);

    }
}
