using Ssd.Repository.Entities;

namespace Ssd.Repository.Tests.Builder
{
    public class UserBuilder
    {
        public UserBuilder()
        {
            Name = "defaultuser";
            Email = "default@email.com";
            Password = "defaultpassword";
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            Email = email;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            Password = password;
            return this;
        }

        public User Build()
        {
            return new User
            {
                Name = Name,
                Email = Email,
                Password = Password
            };
        }
    }
}
