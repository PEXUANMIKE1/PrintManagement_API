using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataProject;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.Enumerates;
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
        private readonly IBaseRepository<Design> _baseDesignRepository;
        public ProjectConverter(IBaseRepository<User> baseUserRepository, IBaseRepository<Customer> baseCustomerRepository, IBaseRepository<Design> baseDesignRepository)
        {
            _baseUserRepository = baseUserRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _baseDesignRepository = baseDesignRepository;
        }
        public async Task<DataResponseProject> EntityDTO(Project project)
        {
            var imgProject = "img-default.png";
            var Design = await _baseDesignRepository.GetAsync(x => x.ProjectId == project.Id && x.DesignStatus==ConstantEnums.DesignStatus.Approved);
            if(Design != null)
            {
                imgProject = Design.FilePath;
            }
            var user = await _baseUserRepository.GetByIdAsync(project.EmployeeId);
            var customer = await _baseCustomerRepository.GetByIdAsync(project.CustomerId);
            return new DataResponseProject
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                RequestDescriptionFromCustomer = project.RequestDescriptionFromCustomer,
                StartDate = project.StartDate,
                ExpectedEndDate = project.ExpectedEndDate,
                EmployeeName = user.FullName,
                CustomerName = customer.FullName,
                ProjectStatus = project.ProjectStatus.ToString(),
                ProjectImg = imgProject,
            };
        }
    }
}
