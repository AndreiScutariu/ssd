using Ssd.Repository.Security.Attributes;

namespace Ssd.Repository.Entities
{
    public class User : Base<int>
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        [SsdEncrypt]
        public virtual string Password { get; set; }
    }
}