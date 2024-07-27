using Microsoft.AspNetCore.Http;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Mappers;
using PrintManagerment_API.Application.Payload.Response;
using PrintManagerment_API.Application.Payload.ResponseModels.DataResourcePropertyDetail;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Doman.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class ResourcePropertyDetailService : IResourcePropertyDetailService
    {
        private readonly IBaseRepository<ResourcePropertyDetail> _baseResourcePropertyDetailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PropertyDetailConverter _propertyDetailConverter;

        public ResourcePropertyDetailService(IBaseRepository<ResourcePropertyDetail> baseResourcePropertyDetailRepository, IHttpContextAccessor httpContextAccessor,
            PropertyDetailConverter propertyDetailConverter)
        {
            _baseResourcePropertyDetailRepository = baseResourcePropertyDetailRepository;
            _httpContextAccessor = httpContextAccessor;
            _propertyDetailConverter = propertyDetailConverter;
        }
        public async Task<ResponseObject<IEnumerable<DataResResourcePropertyDetail>>> GetAll()
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<IEnumerable<DataResResourcePropertyDetail>>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                var propertyDetails = await _baseResourcePropertyDetailRepository.GetAllAsync();
                if(propertyDetails == null)
                {
                    return new ResponseObject<IEnumerable<DataResResourcePropertyDetail>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }
                var propertyDetailDTO = new List<DataResResourcePropertyDetail>();
                foreach (var item in propertyDetails)
                {
                    propertyDetailDTO.Add(await _propertyDetailConverter.EntityDTO(item));
                }
                return new ResponseObject<IEnumerable<DataResResourcePropertyDetail>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Lấy danh sách thành công!",
                    Data = propertyDetailDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<DataResResourcePropertyDetail>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Error:" + ex.StackTrace,
                    Data = null
                };
            }
        }
    }
}
