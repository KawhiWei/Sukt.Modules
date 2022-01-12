namespace Sukt.Module.Core.Infrastructure.AutoMappers.Attributes
{
    public class SuktAutoMapToAttribute : SuktAutoMapperAttribute
    {
        public override SuktAutoMapDirection MapDirection
        {
            get { return SuktAutoMapDirection.From; }
        }

        public SuktAutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {
        }
    }
}