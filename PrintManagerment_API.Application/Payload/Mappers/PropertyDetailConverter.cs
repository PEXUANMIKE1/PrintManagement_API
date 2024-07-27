using PrintManagerment_API.Application.Payload.ResponseModels.DataResourcePropertyDetail;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class PropertyDetailConverter
    {
        private readonly IBaseRepository<ResourceProperty> _baseResourcePropertyRepository;
        private readonly IBaseRepository<Resources> _baseResourcesRepository;

        public PropertyDetailConverter(IBaseRepository<ResourceProperty> baseResourcePropertyRepository, IBaseRepository<Resources> baseResourcesRepository)
        {
            _baseResourcePropertyRepository = baseResourcePropertyRepository;
            _baseResourcesRepository = baseResourcesRepository;
        }

        public async Task<DataResResourcePropertyDetail> EntityDTO(ResourcePropertyDetail data)
        {
            var resourceProperty = await _baseResourcePropertyRepository.GetByIdAsync(data.ResourcePropertyId);
            var resource = await _baseResourcesRepository.GetByIdAsync(resourceProperty.ResourcesId);
            return new DataResResourcePropertyDetail
            {
                Id = data.Id,
                PropertyDetailName = data.PropertyDetailName,
                PropertyName = resourceProperty.ResourcePropertyName,
                ResourceName = resource.ResourceName,
                Image = data.Image,
                Price = data.Price,
                Quantity = data.Quantity,
            };
        }
    }
}
