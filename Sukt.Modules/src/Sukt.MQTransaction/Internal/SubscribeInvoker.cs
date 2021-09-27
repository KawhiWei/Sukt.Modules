using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Internal
{
    public class SubscribeInvoker : ISubscribeInvoker
    {
        private readonly IServiceProvider _serviceProvider;
        public SubscribeInvoker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public virtual async Task InvokeAsync(Message message, ConsumerExecutorDescriptor descriptor)
        {
            var instance = GetInstance(descriptor);
            var parameterDescriptors = descriptor.ParameterDescriptors;
            var executeParameters = new object[parameterDescriptors.Count];
            for (int i = 0; i < parameterDescriptors.Count; i++)
            {
                var parameterDescriptor = parameterDescriptors[i];
                if (message.MessageContent != null)
                {
                    var converter = TypeDescriptor.GetConverter(parameterDescriptor.ParameterType);
                    if (converter.CanConvertFrom(message.MessageContent.GetType()))
                    {
                        executeParameters[i] = converter.ConvertFrom(message.MessageContent);
                    }
                    else
                    {
                        if (parameterDescriptor.ParameterType.IsInstanceOfType(message.MessageContent))
                        {
                            executeParameters[i] = message.MessageContent;
                        }
                        else
                        {
                            executeParameters[i] = Convert.ChangeType(message.MessageContent, parameterDescriptor.ParameterType);
                        }
                    }
                }
            }
            await Task.CompletedTask;
            ExecuteWithParameter(descriptor, instance, executeParameters);
        }

        private object ExecuteWithParameter(ConsumerExecutorDescriptor descriptor,object instance,  object[] parameters)
        {
            object result = null;
            if (parameters.Length<=0)
            {
               result= descriptor.MethodInfo.Invoke(instance, null);
            }
            else
            {
               result= descriptor.MethodInfo.Invoke(instance, parameters);
            }
            return result;
        }
        protected virtual object GetInstance(ConsumerExecutorDescriptor descriptor)
        {
            using var scope = _serviceProvider.CreateScope();
            var provider = scope.ServiceProvider;
            var srvType = descriptor.ServiceTypeInfo?.AsType();
            var implType = descriptor.ImplementationTypeInfo.AsType();
            object obj = null;
            if (srvType != null)
            {
                var list = provider.GetServices(srvType);
                obj = provider.GetServices(srvType).FirstOrDefault(o => o.GetType() == implType);
            }
            if (obj == null)
            {
                obj = ActivatorUtilities.GetServiceOrCreateInstance(provider, implType);
            }
            return obj;
        }

        public void Invoke(Message message, ConsumerExecutorDescriptor descriptor)
        {
            var instance = GetInstance(descriptor);
            var parameterDescriptors = descriptor.ParameterDescriptors;
            var executeParameters = new object[parameterDescriptors.Count];
            for (int i = 0; i < parameterDescriptors.Count; i++)
            {
                var parameterDescriptor = parameterDescriptors[i];
                if (message.MessageContent != null)
                {
                    var converter = TypeDescriptor.GetConverter(parameterDescriptor.ParameterType);
                    if (converter.CanConvertFrom(message.MessageContent.GetType()))
                    {
                        executeParameters[i] = converter.ConvertFrom(message.MessageContent);
                    }
                    else
                    {
                        if (parameterDescriptor.ParameterType.IsInstanceOfType(message.MessageContent))
                        {
                            executeParameters[i] = message.MessageContent;
                        }
                        else
                        {
                            executeParameters[i] = Convert.ChangeType(message.MessageContent, parameterDescriptor.ParameterType);
                        }
                    }
                }
            }
            ExecuteWithParameter(descriptor, instance, executeParameters);
        }
    }
}
