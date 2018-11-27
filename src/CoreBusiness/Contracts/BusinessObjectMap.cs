using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CoreBusiness.Contracts
{
    public class BusinessObjectMap<T> : ClassMap<T> where T : IBusinessObject
    {
        public BusinessObjectMap()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var id_property = Expression.Property(parameter, "ID");
            var id_conversion = Expression.Convert(id_property, typeof(object));
            var id_lambda = Expression.Lambda<Func<T, object>>(id_conversion, parameter);
            Id(id_lambda).GeneratedBy.Identity();
            foreach (PropertyInfo info in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var property = Expression.Property(parameter, info);
                var conversion = Expression.Convert(property, typeof(object));
                var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);
                if (info.Name == "ID")
                    continue;
                Map(lambda);//.To(name);
            }
            string table = typeof(T).Name;
            Table($"{table.ToLower()}s");
        }
    }
}
