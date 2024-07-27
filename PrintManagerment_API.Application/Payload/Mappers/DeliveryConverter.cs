using PrintManagerment_API.Application.Payload.ResponseModels.DataDelivery;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class DeliveryConverter
    {
        private readonly IBaseRepository<ShippingMethod> _baseShippingMethodRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        
        public DeliveryConverter(IBaseRepository<ShippingMethod> baseShippingMethodRepository, IBaseRepository<Project> baseProjectRepository,
            IBaseRepository<Customer> baseCustomerRepository, IBaseRepository<User> baseUserRepository)
        {
            _baseShippingMethodRepository = baseShippingMethodRepository;
            _baseProjectRepository = baseProjectRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _baseUserRepository = baseUserRepository;
        }
        public async Task<DataResponseDelivery> EntityDTO(Delivery delivery)
        {
            var shippingMethod = await _baseShippingMethodRepository.GetByIdAsync(delivery.ShippingMethodId);
            var project = await _baseProjectRepository.GetByIdAsync(delivery.ProjectId);
            var user = await _baseUserRepository.GetByIdAsync(delivery.DeliverId);
            var customer = await _baseCustomerRepository.GetByIdAsync(delivery.CustomerId);
            return new DataResponseDelivery{
                Id = delivery.Id,
                ShippingMethodName = shippingMethod.ShippingMethodName,
                ProjectName = project.ProjectName,
                CustomerName = customer.FullName,
                DeliverName = user.FullName,
                DeliveryAddress = delivery.DeliveryAddress,
                EstimateDeliveryTime = delivery.EstimateDeliveryTime,
                ActualDeliveryTime = delivery.ActualDeliveryTime,
                DeliveryStatus = delivery.DeliveryStatus.ToString(),
            };
        } 
    }
}
