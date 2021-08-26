using Sukt.CodeGenerator;
using Microsoft.Extensions.DependencyInjection;
using Sukt.TestBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sukt.Tests
{
    public class CodeGeneratorTests: IntegratedTest<CodeGeneratorModeule>
    {
        [Fact]
        public void CodeGenerate_Test()
        {
            try
            {
                ProjectMetadata projectMetadata = new ProjectMetadata();
                projectMetadata.Company = "Sukt.Core";
                projectMetadata.SiteUrl = "http://admin.destinycore.club";
                projectMetadata.Creator = "Sukt.Admin";
                projectMetadata.Copyright = "Sukt.Admin";
                projectMetadata.Namespace = "Sukt.Order";
                projectMetadata.SaveFilePath = @"F:\Github\test\testCodeGenerator";
                List<PropertyMetadata> propertyMetadatas = new List<PropertyMetadata>();
                propertyMetadatas.Add(new PropertyMetadata()
                {
                    IsNullable = false,
                    IsPrimaryKey = false,
                    CSharpType = "string",
                    DisplayName = "名字",
                    PropertyName = "Name",
                    IsPageDto = true,
                    IsInputDto=true
                });
                propertyMetadatas.Add(new PropertyMetadata()
                {
                    IsNullable = false,
                    IsPrimaryKey = false,
                    CSharpType = "string",
                    DisplayName = "名字1",
                    PropertyName = "Name1",
                    IsInputDto = true

                });
                propertyMetadatas.Add(new PropertyMetadata()
                {
                    IsNullable = false,
                    IsPrimaryKey = false,
                    CSharpType = "int",
                    DisplayName = "价格",
                    PropertyName = "Price",
                    IsPageDto = false,
                    IsInputDto = true

                });
                projectMetadata.EntityMetadata = new EntityMetadata()
                {
                    EntityName = "Product",
                    DisplayName = "商品",
                    PrimaryKeyType = "Guid",
                    PrimaryKeyName = "Id",
                    IsAggregate = true,
                    Properties = propertyMetadatas,
                    IsCreation = true,
                    IsModification = true,
                    IsSoftDelete = true,
                    AuditedUserKeyType = "Guid",
                    IsAutoMap = true,

                };

                ICodeGenerator codeGenerator = ServiceProvider.GetService<ICodeGenerator>();

                codeGenerator.GenerateCode(projectMetadata);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
    }
}
