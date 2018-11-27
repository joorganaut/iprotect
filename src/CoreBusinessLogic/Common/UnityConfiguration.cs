using AXAMansard.Framework.DTO;
using CoreDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CoreBusinessLogic.Common
{
    public abstract class UnityConfiguration<T> 
    {
        internal IUnityContainer Container { get; private set; }
        public T InstantiateService()
        {
            return InstantiateService(new UnityContainer());
        }

        public T InstantiateService(IUnityContainer container)
        {
            if (container == null)
            {
                container = new UnityContainer();
            }
            Container = container;
            RegisterInterfaces();
            return Container.Resolve<T>();
        }
        internal abstract void RegisterInterfaces();
    }
}
