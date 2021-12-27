using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Contact
{
    public class Get
    {
        public class Query : IRequest<BusinessLogicResponse<Domain.Contact.Contact>>
        {
            public Guid Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(p => p.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, BusinessLogicResponse<Domain.Contact.Contact>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BusinessLogicResponse<Domain.Contact.Contact>> Handle(Query request, CancellationToken cancellationToken)
            {
                var contactEntity = await _context.Contacts.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

                if (contactEntity == null)
                {
                    return BusinessLogicResponse<Domain.Contact.Contact>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                        "There is no contact for specified identifier");
                }

                var contact = _mapper.Map<Domain.Contact.Contact>(contactEntity);
                
                return BusinessLogicResponse<Domain.Contact.Contact>.Success(BusinessLogicResponseResult.Ok, contact);
            }
        }
    }
}