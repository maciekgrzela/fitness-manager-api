using System;
using System.Collections.Generic;
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
    public class GetAll
    {
        public class Query : IRequest<BusinessLogicResponse<IEnumerable<Domain.Address.Address>>> { }

        public class Handler : IRequestHandler<Query, BusinessLogicResponse<IEnumerable<Domain.Address.Address>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BusinessLogicResponse<IEnumerable<Domain.Address.Address>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var addresses = await _context.Addresses.ToListAsync(cancellationToken: cancellationToken);
                return BusinessLogicResponse<IEnumerable<Domain.Address.Address>>.Success(BusinessLogicResponseResult.Ok, _mapper.Map<IEnumerable<Domain.Address.Address>>(addresses));
            }
        }
    }
}