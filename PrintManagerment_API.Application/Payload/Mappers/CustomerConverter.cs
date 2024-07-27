using PrintManagerment_API.Application.Payload.ResponseModels.DataCustomer;
using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class CustomerConverter
    {
        public DataResponseCustomer EntityDTO(Customer customer)
        {
            return new DataResponseCustomer
            {
                Id = customer.Id,
                CustomerName = customer.FullName,
                CustomerAddress = customer.Address,
                CustomerEmail = customer.Email,
                CustomerPhone = customer.PhoneNumber
            };
        }
    }
}
