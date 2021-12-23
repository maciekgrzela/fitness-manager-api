using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FitnessManager.BusinessLogic.Membership
{
    public class CurrentUser
    {
        public class Query : IRequest<BusinessLogicResponse<LoggedUser>> { }
        
        public class Handler : IRequestHandler<Query, BusinessLogicResponse<LoggedUser>>
        {
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<UserEntity> _userManager;

            public Handler(IMapper mapper, IUserAccessor userAccessor, UserManager<UserEntity> userManager)
            {
                _mapper = mapper;
                _userAccessor = userAccessor;
                _userManager = userManager;
            }
            
            public async Task<BusinessLogicResponse<LoggedUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetUserName());

                if (user == null)
                {
                    return BusinessLogicResponse<LoggedUser>.Failure(BusinessLogicResponseResult.ResourceDoesntExist, "User not found");
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                
                return BusinessLogicResponse<LoggedUser>.Success(BusinessLogicResponseResult.Ok, new LoggedUser
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = userRoles[0],
                });
            }
        }
    }
}