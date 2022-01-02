using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.User;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Membership
{
    public class Login
    {
         public class Query : IRequest<BusinessLogicResponse<UserDto>>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(p => p.Email).NotEmpty().EmailAddress();
                RuleFor(p => p.Password).NotEmpty();
            }
        }
        
        public class Handler : IRequestHandler<Query, BusinessLogicResponse<UserDto>>
        {
            private readonly SignInManager<UserEntity> _signInManager;
            private readonly UserManager<UserEntity> _userManager;
            private readonly IWebTokenGenerator _webTokenGenerator;
            private readonly IMapper _mapper;

            public Handler(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IWebTokenGenerator webTokenGenerator, IMapper mapper)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _webTokenGenerator = webTokenGenerator;
                _mapper = mapper;
            }
            
            public async Task<BusinessLogicResponse<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                    .Include(p => p.Address)
                    .Include(p => p.Contact)
                    .Where(p => p.Email == request.Email)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (user == null)
                {
                    return BusinessLogicResponse<UserDto>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                        "User not found");
                }

                var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Count == 0)
                {
                    return BusinessLogicResponse<UserDto>.Failure(BusinessLogicResponseResult.UserIsNotAuthorized,
                        "User is not authorized");
                }

                if (!checkPassword.Succeeded)
                {
                    return BusinessLogicResponse<UserDto>.Failure(BusinessLogicResponseResult.UserIsNotAuthorized,
                        "Incorrect user credentials");
                }

                var loggedUser = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = userRoles[0],
                    Address = _mapper.Map<AddressEntity, Domain.Address.Address>(user.Address),
                    Contact = _mapper.Map<ContactEntity, Domain.Contact.Contact>(user.Contact),
                    Token = _webTokenGenerator.CreateToken(user, userRoles[0])
                };
                
                return BusinessLogicResponse<UserDto>.Success(BusinessLogicResponseResult.Ok, loggedUser);
            }
        }
    }
}