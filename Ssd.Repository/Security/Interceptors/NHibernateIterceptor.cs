using System;
using System.Linq;
using NHibernate;
using NHibernate.Type;
using Ssd.Repository.Security.Attributes;
using Ssd.Repository.Security.Encryptions;

namespace Ssd.Repository.Security.Interceptors
{
    public class NHibernateIterceptor : EmptyInterceptor
    {
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            var entityType = entity.GetType();
            var properties = entityType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(SsdEncryptAttribute)));
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(entity, null);
                propertyInfo.SetValue(entity, StringEncrypter.DoHash((string)value), null);
            }

            return true;
        }
    }
}
