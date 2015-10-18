namespace Ssd.Repository.Entities
{
    public class UserEntity : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
