using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Address.Address Address { get; set; }
        public Contact.Contact Contact { get; set; }
        public string Token { get; set; }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, LoggedUserDto>();
        }
    }
}