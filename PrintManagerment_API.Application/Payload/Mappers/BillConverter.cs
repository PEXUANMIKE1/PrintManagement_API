using PrintManagerment_API.Application.Payload.ResponseModels.DataBill;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class BillConverter
    {
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<User> _baseUserRepository;

        public BillConverter(IBaseRepository<Project> baseProjectRepository, IBaseRepository<Customer> baseCustomerRepository, IBaseRepository<User> baseUserRepository)
        {
            _baseProjectRepository = baseProjectRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _baseUserRepository = baseUserRepository;
        }
        public async Task<DataResponseBill> EntityDTO(Bill bill)
        {
            var project = await _baseProjectRepository.GetByIdAsync(bill.ProjectId);
            var employee = await _baseUserRepository.GetByIdAsync(bill.EmployeeId);
            var customer = await _baseCustomerRepository.GetByIdAsync(bill.CustomerId);
            return new DataResponseBill
            {
                Id = bill.Id,
                BillName = bill.BillName,
                BillStatus = bill.BillStatus.ToString(),
                CreateTime = bill.CreateTime,
                TotalMoney = bill.TotalMoney,
                ProjectName = project.ProjectName,
                CustomerName = customer.FullName,
                EmployeeName = employee.FullName,
                TradingCode = bill.TradingCode
            };
        }
    }
}
