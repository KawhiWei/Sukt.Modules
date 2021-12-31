using Sukt.Module.Core.Enums;

namespace Sukt.Module.Core.DomainResults
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class DomainResult<TData> 
    {
        public DomainResult() : this(OperationEnumType.Success)
        {
        }

        public DomainResult(OperationEnumType type = OperationEnumType.Success) : this("", default(TData), type)
        {
        }

        public DomainResult(string message, OperationEnumType type) : this(message, default(TData), type)
        {
        }

        public DomainResult(string message, TData data, OperationEnumType type)
        {
            Message = message;
            Type = type;
            Data = data;
        }

        public TData Data { get; set; }

        public OperationEnumType Type { get; set; }

        public bool Success => Type == OperationEnumType.Success;

        public string Message { get; set; }
        public bool Error()
        {
            return Type != OperationEnumType.Success;
        }

        public bool Exp()
        {
            return Type == OperationEnumType.Exp;
        }
    }
}