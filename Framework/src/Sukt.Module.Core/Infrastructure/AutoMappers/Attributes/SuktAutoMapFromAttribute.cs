namespace Sukt.Module.Core.Infrastructure.AutoMappers.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SuktAutoMapFromAttribute : SuktAutoMapperAttribute
    {
        public override SuktAutoMapDirection MapDirection
        {
            get { return SuktAutoMapDirection.From; }
        }

        public SuktAutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {
        }
    }
}