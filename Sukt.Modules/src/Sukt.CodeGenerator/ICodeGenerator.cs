namespace Sukt.CodeGenerator
{
    /// <summary>
    /// 生成器
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// 创建代码文件
        /// </summary>
        /// <param name="projectMetadata"></param>

        void GenerateCode(ProjectMetadata projectMetadata);

        /// <summary>
        /// 创建实体代码
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateEntityCode(ProjectMetadata metadata);

        /// <summary>
        /// 生成实体配置代码
        /// </summary>
        /// <param name="metadata">元数据</param>
        /// <returns></returns>
        CodeData GenerateEntityConfigurationCode(ProjectMetadata metadata);

        /// <summary>
        /// 生成契约
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateIApplicationContract(ProjectMetadata metadata);

        /// <summary>
        /// 生成契约实现
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateApplicationContract(ProjectMetadata metadata);
        /// <summary>
        /// 生成输入Dto
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateInputDto(ProjectMetadata metadata);
        /// <summary>
        /// 生成输出Dto
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateOutputDto(ProjectMetadata metadata);
        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateController(ProjectMetadata metadata);
    }
}