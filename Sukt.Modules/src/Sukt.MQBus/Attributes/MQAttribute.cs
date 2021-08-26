using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class MQAttribute : Attribute
    {

    }
}
