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
    public class BillDeliveryConverter
    {
        private readonly IBaseRepository<ShippingMethod> _baseShippingMethodRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<Bill> _baseBillRepository;
        private readonly IBaseRepository<User> _baseUserRepository;

        public BillDeliveryConverter(IBaseRepository<ShippingMethod> baseShippingMethodRepository, IBaseRepository<Project> baseProjectRepository,
            IBaseRepository<Customer> baseCustomerRepository, IBaseRepository<User> baseUserRepository, IBaseRepository<Bill> baseBillRepository)
        {
            _baseShippingMethodRepository = baseShippingMethodRepository;
            _baseProjectRepository = baseProjectRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _baseUserRepository = baseUserRepository;
            _baseBillRepository = baseBillRepository;
        }
        public async Task<DataResponseBillDelivery> EntityDTO(Delivery delivery)
        {
            var shippingMethod = await _baseShippingMethodRepository.GetByIdAsync(delivery.ShippingMethodId);
            var project = await _baseProjectRepository.GetByIdAsync(delivery.ProjectId);
            var user = await _baseUserRepository.GetByIdAsync(delivery.DeliverId);
            var customer = await _baseCustomerRepository.GetByIdAsync(delivery.CustomerId);
            var bill = await _baseBillRepository.GetAsync(x => x.ProjectId == project.Id);
            return new DataResponseBillDelivery
            {
                BillId = bill.Id,
                BillName = bill.BillName,
                TradingCode = bill.TradingCode,
                TotalMoney = bill.TotalMoney,
                CustomerName = customer.FullName,
                ProjectName = project.ProjectName,
                SDT = customer.PhoneNumber,
                //
                DeliveryId = delivery.Id,
                ShippingMethodName = shippingMethod.ShippingMethodName,
                DeliverName = user.FullName,
                DeliveryAddress = delivery.DeliveryAddress,
                EstimateDeliveryTime = delivery.EstimateDeliveryTime,
                ActualDeliveryTime = delivery.ActualDeliveryTime,
                DeliveryStatus = delivery.DeliveryStatus.ToString(),
            };
        }
    }
}
