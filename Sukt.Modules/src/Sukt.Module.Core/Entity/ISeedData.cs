namespace Sukt.Module.Core.Entity
{
    public interface ISeedData
    {
        /// <summary>
        ///种子数据初始化
        /// </summary>
        void Initialize();

        int Order { get; }

        bool Disable { get; }
    }
}