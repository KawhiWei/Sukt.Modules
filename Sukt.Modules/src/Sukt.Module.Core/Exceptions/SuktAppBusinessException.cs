using System;
using System.Runtime.Serialization;

namespace Sukt.Module.Core.Exceptions
{
    /// <summary>
    /// 所有业务异常使用该类抛出，中间件会做拦截
    /// </summary>
    public class SuktAppBusinessException : Exception
    {
        public SuktAppBusinessException()
        {
        }

        public SuktAppBusinessException(string message) : base(message)
        {
        }

        public SuktAppBusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
