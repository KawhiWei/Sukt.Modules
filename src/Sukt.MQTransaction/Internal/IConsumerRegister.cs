using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public interface IConsumerRegister: IProcessingServer
    {
        bool Ishealthy();
        void ReStart(bool force = false);
    }
}
