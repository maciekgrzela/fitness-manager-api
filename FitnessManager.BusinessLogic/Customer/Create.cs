using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.DataAccess.Context;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;
using FluentValidation;
using MediatR;

namespace FitnessManager.BusinessLogic.Customer
{
    public class Create
    {
        public class Command : IRequest<BusinessLogicResponse<Domain.Customer.Customer>>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public SaveAddressDto Address { get; set; }
            public SaveContactDto Contact { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(p => p.FirstName).NotEmpty().NotNull();
                RuleFor(p => p.LastName).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, BusinessLogicResponse<Domain.Customer.Customer>>
        {

            private readonly DataContext _context;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _context = context;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<BusinessLogicResponse<Domain.Customer.Customer>> Handle(Command request, CancellationToken cancellationToken)
            {
                var address = _mapper.Map<Domain.Address.Address>(request.Address);
                var contact = _mapper.Map<Domain.Contact.Contact>(request.Contact);
                
                address.Id = Guid.NewGuid();
                contact.Id = Guid.NewGuid();

                var customer = new CustomerEntity
                {
                    Id = Guid.NewGuid(),
                    AddressId = address.Id,
                    Address = _mapper.Map<AddressEntity>(address),
                    ContactId = contact.Id,
                    Contact = _mapper.Map<ContactEntity>(contact),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Enrolments = new List<CustomerFitnessClassEnrolmentEntity>(),
                    ActiveSubscriptions = new List<CustomerSubscriptionsEntity>(),
                    EquipmentReservations = new List<EquipmentReservationEntity>()
                };

                await _context.Customers.AddAsync(customer, cancellationToken);
                
                return BusinessLogicResponse<Domain.Customer.Customer>.Success(BusinessLogicResponseResult.Created, new Domain.Customer.Customer());
            }
        }
    }
}