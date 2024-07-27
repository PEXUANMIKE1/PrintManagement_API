using PrintManagerment_API.Application.Payload.ResponseModels.DataDesign;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class DesignConverter
    {
        private IBaseRepository<Project> _baseProjectRepository;
        private IBaseRepository<User> _baseUserRepository;

        public DesignConverter(IBaseRepository<Project> baseProjectRepository, IBaseRepository<User> baseUserRepository)
        {
            _baseProjectRepository = baseProjectRepository;
            _baseUserRepository = baseUserRepository;
        }
        public async Task<DataResponseDesign> EntityDTO(Design design)
        {
            var project = await _baseProjectRepository.GetByIdAsync(design.ProjectId);
            var designer = await _baseUserRepository.GetByIdAsync(design.DesginerId);
            var aprrover = await _baseUserRepository.GetByIdAsync(design.ApproverId);
            return new DataResponseDesign
            {
                Id = design.Id,
                ProjectName = project.ProjectName,
                DesignerName = designer.FullName,
                FilePath = design.FilePath,
                DesignTime = design.DesignTime,
                DesignStatus = design.DesignStatus.ToString(),
                ApproverName = aprrover.FullName
            };
        }
    }
}
