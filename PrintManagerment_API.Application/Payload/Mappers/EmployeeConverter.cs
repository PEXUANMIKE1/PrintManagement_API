using PrintManagerment_API.Application.Payload.ResponseModels.DataUsers;
using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class EmployeeConverter
    {
        public DataResponseEmployee EntityDTO(User employee)
        {
            return new DataResponseEmployee
            {
                Id = employee.Id,
                EmployeeEmail = employee.Email,
                EmployeeName = employee.FullName,
                EmployeePhone = employee.PhoneNumber
            };
        }
    }
}
