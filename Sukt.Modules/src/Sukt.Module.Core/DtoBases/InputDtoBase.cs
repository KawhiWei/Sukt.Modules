namespace Sukt.Module.Core.Domian
{
    public interface IInputDto
    {

    }
    public class InputDtoBase<Tkey>: IInputDto
    {
        public virtual Tkey Id { get; set; }
    }
}