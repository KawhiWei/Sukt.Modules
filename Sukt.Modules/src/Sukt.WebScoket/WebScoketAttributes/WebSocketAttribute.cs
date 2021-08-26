using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.WebScoket.WebScoketAttributes
{
    /// <summary>
    /// WebSocket特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WebSocketAttribute : Attribute
    {
        /// <summary>
        /// Mark action use action name
        /// </summary>
        public WebSocketAttribute()
        {
        }

        /// <summary>
        /// Mark action use method value
        /// </summary>
        /// <param name="method"></param>
        public WebSocketAttribute(string method) : this()
        {
            Method = method;
        }

        /// <summary>
        /// Endpoint method name
        /// </summary>
        public string Method { get; set; }
    }
}
