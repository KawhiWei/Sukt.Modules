using System;

namespace Sukt.EntityFrameworkCore.MappingConfiguration
{
    public class EntityManager : IEntityManager
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
