using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Customer
{
    public class Get
    {
        public class Query : IRequest<BusinessLogicResponse<Domain.Customer.Customer>>
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

        public class Handler : IRequestHandler<Query, BusinessLogicResponse<Domain.Customer.Customer>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BusinessLogicResponse<Domain.Customer.Customer>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customerEntity = await _context.Customers
                    .Include(p => p.ActiveSubscriptions)
                    .Include(p => p.Enrolments)
                    .Include(p => p.EquipmentReservations)
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

                if (customerEntity == null)
                {
                    return BusinessLogicResponse<Domain.Customer.Customer>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                        "There is no contact for specified identifier");
                }

                var customer = _mapper.Map<Domain.Customer.Customer>(customerEntity);
                
                return BusinessLogicResponse<Domain.Customer.Customer>.Success(BusinessLogicResponseResult.Ok, customer);
            }
        }
    }
}