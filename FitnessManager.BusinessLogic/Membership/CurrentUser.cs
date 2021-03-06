using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;
using FitnessManager.Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Membership
{
    public class CurrentUser
    {
        public class Query : IRequest<BusinessLogicResponse<Domain.User.UserDto>> { }
        
        public class Handler : IRequestHandler<Query, BusinessLogicResponse<Domain.User.UserDto>>
        {
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<UserEntity> _userManager;
            private readonly IWebTokenGenerator _webTokenGenerator;

            public Handler(IMapper mapper, IUserAccessor userAccessor, UserManager<UserEntity> userManager, IWebTokenGenerator webTokenGenerator)
            {
                _mapper = mapper;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _webTokenGenerator = webTokenGenerator;
            }
            
            public async Task<BusinessLogicResponse<Domain.User.UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                    .Include(p => p.Address)
                    .Include(p => p.Contact)
                    .FirstOrDefaultAsync(p => p.Email == _userAccessor.GetUserName(), cancellationToken: cancellationToken);

                if (user == null)
                {
                    return BusinessLogicResponse<Domain.User.UserDto>.Failure(BusinessLogicResponseResult.ResourceDoesntExist, "User not found");
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                
                return BusinessLogicResponse<UserDto>.Success(BusinessLogicResponseResult.Ok, new Domain.User.UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = userRoles[0],
                    Address = _mapper.Map<AddressEntity, AddressDto>(user.Address),
                    Contact = _mapper.Map<ContactEntity, ContactDto>(user.Contact),
                    Token = _webTokenGenerator.CreateToken(user, userRoles[0])
                });
            }
        }
    }
}