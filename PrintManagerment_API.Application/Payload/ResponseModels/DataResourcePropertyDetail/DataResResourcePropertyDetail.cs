using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.ResponseModels.DataResourcePropertyDetail
{
    public class DataResResourcePropertyDetail : DataResponseBase
    {
        public string PropertyDetailName { get; set; }
        public string PropertyName { get; set; }
        public string ResourceName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
