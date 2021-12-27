using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Customer
{
    public class GetAll
    {
        public class Query : IRequest<BusinessLogicResponse<IEnumerable<Domain.Customer.Customer>>> { }

        public class Handler : IRequestHandler<Query, BusinessLogicResponse<IEnumerable<Domain.Customer.Customer>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BusinessLogicResponse<IEnumerable<Domain.Customer.Customer>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customers = await _context.Customers
                    .Include(p => p.ActiveSubscriptions)
                    .Include(p => p.Enrolments)
                    .Include(p => p.EquipmentReservations)
                    .ToListAsync(cancellationToken: cancellationToken);
                return BusinessLogicResponse<IEnumerable<Domain.Customer.Customer>>.Success(BusinessLogicResponseResult.Ok, _mapper.Map<IEnumerable<Domain.Customer.Customer>>(customers));
            }
        }
    }
}