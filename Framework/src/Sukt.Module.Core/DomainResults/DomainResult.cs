using Sukt.Module.Core.Enums;

namespace Sukt.Module.Core.DomainResults
{
    public class DomainResult : DomainResult<object>
    {
        public DomainResult() : base(OperationEnumType.Success)
        {
        }
        public DomainResult(string message)
        {
            Message = message;  
        }

        public DomainResult(OperationEnumType type = OperationEnumType.Success) : base("", null, type)
        {
        }

        public DomainResult(string message, OperationEnumType type) : base(message, null, type)
        {
        }

        public DomainResult(string message, object data, OperationEnumType type) : base(message, data, type)
        {
        }

        /// <summary>
        /// 成功
        /// </summary>
        public static DomainResult Ok()
        {
            return Ok(string.Empty, null);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">提示消息</param>
        public static DomainResult Ok(string message)
        {
            return Ok(message, null);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回成功数据</param>
        /// <returns></returns>
        public static DomainResult Ok(object data)
        {
            return Ok(string.Empty, data);
        }
        public  void SetData(object data)
        {
            Data = data.ToString();
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="data">返回成功数据</param>
        /// <returns></returns>
        public static DomainResult Ok(string message, object data)
        {
            return new DomainResult(message, data, OperationEnumType.Success);
        }

        public static DomainResult Error(string message)
        {
            return Error(message, null);
        }

        public static DomainResult Error(object data)
        {
            return Error(string.Empty, data);
        }

        public static DomainResult Error(string message, object data)
        {
            return new DomainResult(message, data, OperationEnumType.Error);
        }
    }
}