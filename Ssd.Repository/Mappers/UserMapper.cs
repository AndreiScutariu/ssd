using FluentNHibernate.Mapping;
using Ssd.Repository.Entities;

namespace Ssd.Repository.Mappers
{
    public class UserMapper : ClassMap<User>
    {
        public UserMapper()
        {
            Id(m => m.Id);
            Map(m => m.Name);
            Map(m => m.Password);
            Map(m => m.Email);
        }
    }
}