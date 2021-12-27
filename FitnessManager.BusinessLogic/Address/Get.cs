using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Address
{
    public class Get
    {
        public class Query : IRequest<BusinessLogicResponse<Domain.Address.Address>>
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

        public class Handler : IRequestHandler<Query, BusinessLogicResponse<Domain.Address.Address>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BusinessLogicResponse<Domain.Address.Address>> Handle(Query request, CancellationToken cancellationToken)
            {
                var addressEntity = await _context.Addresses.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

                if (addressEntity == null)
                {
                    return BusinessLogicResponse<Domain.Address.Address>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                        "There is no address for specified identifier");
                }

                var address = _mapper.Map<Domain.Address.Address>(addressEntity);
                
                return BusinessLogicResponse<Domain.Address.Address>.Success(BusinessLogicResponseResult.Ok, address);
            }
        }
    }
}