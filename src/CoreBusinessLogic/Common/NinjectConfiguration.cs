using AXAMansard.Framework.DTO;
using CoreDataAccess;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBusinessLogic.Common
{
    public class NinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(INHBusinessObjectDAO<>)).To(typeof(NHBusinessObjectDAO<>));
            Bind(typeof(INPBusinessObjectDAO<>)).To(typeof(NPBusinessObjectDAO<>));
            Bind(typeof(IDIDAOImplementation<>)).To(typeof(DIDAOImplementation<>));
        }

    }
}
