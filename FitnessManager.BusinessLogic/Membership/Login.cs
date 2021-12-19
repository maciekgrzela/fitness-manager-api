using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
         public class Query : IRequest<BusinessLogicResponse<LoggedUser>>
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
        
        public class Handler : IRequestHandler<Query, BusinessLogicResponse<LoggedUser>>
        {
            private readonly SignInManager<UserEntity> _signInManager;
            private readonly UserManager<UserEntity> _userManager;
            private readonly IWebTokenGenerator _webTokenGenerator;

            public Handler(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IWebTokenGenerator webTokenGenerator)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _webTokenGenerator = webTokenGenerator;
            }
            
            public async Task<BusinessLogicResponse<LoggedUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.Where(p => p.Email == request.Email && !p.UserName.Contains("fb") && !p.UserName.Contains("go")).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (user == null)
                {
                    return BusinessLogicResponse<LoggedUser>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                        "User not found");
                }

                var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Count == 0)
                {
                    return BusinessLogicResponse<LoggedUser>.Failure(BusinessLogicResponseResult.UserIsNotAuthorized,
                        "User is not authorized");
                }

                if (!checkPassword.Succeeded)
                {
                    return BusinessLogicResponse<LoggedUser>.Failure(BusinessLogicResponseResult.UserIsNotAuthorized,
                        "Incorrect user credentials");
                }

                var loggedUser = new LoggedUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Role = userRoles[0],
                    Token = _webTokenGenerator.CreateToken(user, userRoles[0])
                };
                
                return BusinessLogicResponse<LoggedUser>.Success(BusinessLogicResponseResult.Ok, loggedUser);
            }
        }
    }
}