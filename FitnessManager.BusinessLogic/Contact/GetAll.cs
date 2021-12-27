using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Contact
{
    public class GetAll
    {
        public class Query : IRequest<BusinessLogicResponse<IEnumerable<Domain.Contact.Contact>>> { }

        public class Handler : IRequestHandler<Query, BusinessLogicResponse<IEnumerable<Domain.Contact.Contact>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BusinessLogicResponse<IEnumerable<Domain.Contact.Contact>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var contacts = await _context.Contacts.ToListAsync(cancellationToken: cancellationToken);
                return BusinessLogicResponse<IEnumerable<Domain.Contact.Contact>>.Success(BusinessLogicResponseResult.Ok, _mapper.Map<IEnumerable<Domain.Contact.Contact>>(contacts));
            }
        }
    }
}