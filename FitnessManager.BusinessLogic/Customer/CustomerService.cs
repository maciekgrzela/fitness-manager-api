using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.Customer.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;
using FitnessManager.Domain.Customer;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseRepository<CustomerEntity> _baseCustomerRepository;
        private readonly IBaseRepository<AddressEntity> _baseAddressRepository;
        private readonly IBaseRepository<ContactEntity> _baseContactRepository;
        private readonly IBaseRepository<SubscriptionEntity> _baseSubscriptionRepository;
        private readonly IBaseRepository<CustomerFitnessClassEnrolmentEntity> _baseEnrolmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IBaseRepository<CustomerEntity> baseCustomerRepository,
            IBaseRepository<AddressEntity> baseAddressRepository, 
            IBaseRepository<ContactEntity> baseContactRepository, 
            IBaseRepository<SubscriptionEntity> baseSubscriptionRepository,
            IBaseRepository<CustomerFitnessClassEnrolmentEntity> baseEnrolmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _baseCustomerRepository = baseCustomerRepository;
            _baseAddressRepository = baseAddressRepository;
            _baseContactRepository = baseContactRepository;
            _baseSubscriptionRepository = baseSubscriptionRepository;
            _baseEnrolmentRepository = baseEnrolmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _baseCustomerRepository.GetAll()
                .Include(p => p.Address)
                .Include(p => p.Contact)
                .Include(p => p.Enrolments)
                .Include(p => p.ActiveSubscriptions)
                .Include(p => p.EquipmentReservations)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<CustomerEntity>> GetByIdAsync(Guid id)
        {
            var customer = await _baseCustomerRepository.GetById(id);

            if (customer == null)
            {
                return BusinessLogicResponse<CustomerEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "There is no customer for specified identifier");
            }
            
            return BusinessLogicResponse<CustomerEntity>.Success(BusinessLogicResponseResult.Ok, customer);
        }

        public async Task<BusinessLogicResponse<CustomerEntity>> SaveAsync(SaveCustomerDto dto)
        {
            var customerId = Guid.NewGuid();
            var customer = new CustomerEntity
            {
                Id = customerId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EquipmentReservations = new List<EquipmentReservationEntity>()
            };
            
            if (dto.Address != null)
            {
                var address = _mapper.Map<SaveAddressDto, AddressEntity>(dto.Address);
                address.Id = Guid.NewGuid();
                var addressCreated = await _baseAddressRepository.Add(address);
                customer.Address = addressCreated;
            }
            
            if (dto.Contact != null)
            {
                var contact = _mapper.Map<SaveContactDto, ContactEntity>(dto.Contact);
                contact.Id = Guid.NewGuid();
                var contactCreated = await _baseContactRepository.Add(contact);
                customer.Contact = contactCreated;
            }

            var activeSubscriptions = new List<CustomerSubscriptionsEntity>();

            foreach (var subscriptionId in dto.ActiveSubscriptions)
            {
                var existingSubscription = await _baseSubscriptionRepository.GetById(subscriptionId);

                if (existingSubscription == null)
                {
                    return BusinessLogicResponse<CustomerEntity>.Failure(BusinessLogicResponseResult.ConflictOccured,
                        "There is no subscription for specified identifier");
                }
                
                activeSubscriptions.Add(new CustomerSubscriptionsEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    SubscriptionId = existingSubscription.Id,
                    PaymentStatus = "init",
                    StartedAt = DateTime.UtcNow,
                    EndedAt = null
                });
            }
            
            var enrolments = new List<CustomerFitnessClassEnrolmentEntity>();

            foreach (var enrolmentId in dto.Enrolments)
            {
                var existingEnrolment = await _baseEnrolmentRepository.GetById(enrolmentId);

                if (existingEnrolment == null)
                {
                    return BusinessLogicResponse<CustomerEntity>.Failure(BusinessLogicResponseResult.ConflictOccured,
                        "There is no enrolment for specified identifier");
                }
                
                enrolments.Add(new CustomerFitnessClassEnrolmentEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    EnrolmentId = enrolmentId,
                });
            }

            customer.ActiveSubscriptions = activeSubscriptions;
            customer.Enrolments = enrolments;

            var customerSaved = await _baseCustomerRepository.Add(customer);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<CustomerEntity>.Success(BusinessLogicResponseResult.Created, customerSaved);
        }

        public async Task<BusinessLogicResponse<CustomerEntity>> UpdateAsync(Guid id, UpdateCustomerDto dto)
        {
            var existingCustomer = await _baseCustomerRepository.GetById(id);

            if (existingCustomer == null)
            {
                return BusinessLogicResponse<CustomerEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "There is no customer for specified identifier");
            }

            existingCustomer.FirstName = dto.FirstName;
            existingCustomer.LastName = dto.LastName;

            if (dto.Contact != null)
            {
                var contact = new ContactEntity
                {
                    Id = Guid.NewGuid(),
                    Email = dto.Contact.Email,
                    Website = dto.Contact.Website,
                    FacebookUrl = dto.Contact.FacebookUrl,
                    InstagramUrl = dto.Contact.InstagramUrl,
                    PhoneNumber = dto.Contact.PhoneNumber,
                    CustomerId = existingCustomer.Id
                };

                existingCustomer.Contact = contact;
            }

            if (dto.Address != null)
            {
                var address = new AddressEntity
                {
                    Id = Guid.NewGuid(),
                    Street = dto.Address.Street,
                    Number = dto.Address.Number,
                    Country = dto.Address.Country,
                    City = dto.Address.City,
                    PostalCode = dto.Address.PostalCode,
                    CustomerId = existingCustomer.Id
                };

                existingCustomer.Address = address;
            }

            _baseCustomerRepository.Update(existingCustomer);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<CustomerEntity>.Success(BusinessLogicResponseResult.Updated, existingCustomer);
        }

        public async Task<BusinessLogicResponse<CustomerEntity>> DeleteAsync(Guid id)
        {
            var existingCustomer = await _baseCustomerRepository.GetById(id);

            if (existingCustomer == null)
            {
                return BusinessLogicResponse<CustomerEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "There is no customer for specified identifier");
            }

            await _baseCustomerRepository.Delete(existingCustomer);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<CustomerEntity>.Success(BusinessLogicResponseResult.Deleted, new CustomerEntity());
        }
    }
}