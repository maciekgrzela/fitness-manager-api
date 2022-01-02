using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.Validators;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Common.Enums;
using FitnessManager.Domain.Contact;
using FitnessManager.Domain.User;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Membership
{
    public class Register
    {
        public class Query : IRequest<BusinessLogicResponse<UserDto>>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public SaveAddressDto Address { get; set; }
            public SaveContactDto Contact { get; set; }
            public string Role { get; set; }
        }
        
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(p => p.FirstName).NotEmpty().MaximumLength(150);
                RuleFor(p => p.LastName).NotEmpty().MaximumLength(200);
                RuleFor(p => p.Email).NotEmpty().EmailAddress();
                RuleFor(p => p.Password).Password();
                RuleFor(p => p.Role).IsInUsersRole();
            }
        }
        
        public class Handler : IRequestHandler<Query, BusinessLogicResponse<UserDto>>
        {
            private readonly UserManager<UserEntity> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IWebTokenGenerator _webTokenGenerator;
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IWebTokenGenerator webTokenGenerator, IUserAccessor userAccessor, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _webTokenGenerator = webTokenGenerator;
                _userAccessor = userAccessor;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            
            
            public async Task<BusinessLogicResponse<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var existingEmail = await _userManager.Users.Where(p => p.Email == request.Email).ToListAsync(cancellationToken: cancellationToken);

                if (existingEmail.Count > 0)
                {
                    return BusinessLogicResponse<UserDto>.Failure(BusinessLogicResponseResult.UserIsNotAuthorized,
                        "User with this e-mail already exists");
                }

                var userRole = (EUserRole) Enum.Parse(typeof(EUserRole), _userAccessor.GetUserRole());
                var requestedRole = (EUserRole) Enum.Parse(typeof(EUserRole), request.Role);

                if (userRole == EUserRole.RegularUser && requestedRole == EUserRole.Admin)
                {
                    return BusinessLogicResponse<UserDto>.Failure(BusinessLogicResponseResult.AccessDenied,
                        "You don't have sufficient priviledges to create Admin's account");
                }

                var address = _mapper.Map<SaveAddressDto, Domain.Address.Address>(request.Address);
                var contact = _mapper.Map<SaveContactDto, Domain.Contact.Contact>(request.Contact);
                
                address.Id = Guid.NewGuid();
                contact.Id = Guid.NewGuid();

                var user = new UserEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.Email,
                    Email = request.Email,
                    Role = request.Role,
                    Address = _mapper.Map<Domain.Address.Address, AddressEntity>(address),
                    AddressId = address.Id,
                    Contact = _mapper.Map<Domain.Contact.Contact, ContactEntity>(contact),
                    ContactId = contact.Id
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return BusinessLogicResponse<UserDto>.Failure(BusinessLogicResponseResult.ConflictOccured,
                        "Unable to create new user's account");
                }

                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, request.Role));
                await _userManager.AddToRoleAsync(user, request.Role);

                await _unitOfWork.CommitTransactionsAsync();

                var loggedUser = _mapper.Map<UserEntity, UserDto>(user);
                loggedUser.Token = _webTokenGenerator.CreateToken(user, request.Role);

                return BusinessLogicResponse<UserDto>.Success(BusinessLogicResponseResult.Ok, loggedUser);
            }
        }
    }
}