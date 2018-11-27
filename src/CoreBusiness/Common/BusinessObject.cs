using AXAMansard.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CoreBusiness.Common
{
    [Serializable]
    public abstract class BusinessObject : DataObject, IBusinessObject
    {
        public virtual DateTime? DateCreated { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual DateTime? DateLastModified { get; set; }
        public virtual string Name { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string LastModifiedBy { get; set; }
    }
    public class Variance
    {
        public string Prop { get; set; }
        public object valA { get; set; }
        public object valB { get; set; }
    }
    public static class BusinessObjectExtension
    {
        public static List<Variance> DetailedCompare<BusinessObject>(this BusinessObject val1, BusinessObject val2)
        {
            List<Variance> variances = new List<Variance>();
            PropertyInfo[] fi = val1.GetType().GetProperties();
            foreach (PropertyInfo f in fi)
            {
                Variance v = new Variance();
                v.Prop = f.Name;
                v.valA = f.GetValue(val1, null);
                v.valB = f.GetValue(val2, null);
                try
                {
                    if (!v.valA.Equals(v.valB))
                        variances.Add(v);
                }
                catch
                {
                    continue;
                }
            }
            return variances;
        }
        public static T Clone<T>(this T obj, T s_obj, Expression<Func<T, object>> selector = null) where T : BusinessObject
        {
            T result = default(T);
            AutoMapper.Mapper.Initialize(cfg =>
            {
                if (selector != null)
                {
                    cfg.CreateMap<T, T>().ForMember(selector, x => x.Ignore());
                }
                else
                {
                    cfg.CreateMap<T, T>();
                }
            });
            result = AutoMapper.Mapper.Map<T>(s_obj);
            return result;
        }
    }
}
