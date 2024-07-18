using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataProject;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class ProjectConverter
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        public ProjectConverter(IBaseRepository<User> baseUserRepository, IBaseRepository<Customer> baseCustomerRepository)
        {
            _baseUserRepository = baseUserRepository;
            _baseCustomerRepository = baseCustomerRepository;
        }
        public async Task<DataResponseProject> EntityDTO(Project project)
        {
            var user = await _baseUserRepository.GetByIdAsync(project.EmployeeId.Value);
            var customer = await _baseCustomerRepository.GetByIdAsync(project.CustomerId);
            return new DataResponseProject
            {
                ProjectName = project.ProjectName,
                RequestDescriptionFromCustomer = project.RequestDescriptionFromCustomer,
                StartDate = project.StartDate,
                ExpectedEndDate = project.ExpectedEndDate,
                EmployeeName = user.FullName,
                CustomerName = customer.FullName,
                ProjectStatus = project.ProjectStatus.ToString(),
            };
        }
    }
}
