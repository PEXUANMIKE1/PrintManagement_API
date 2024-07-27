using PrintManagerment_API.Application.Payload.ResponseModels.DataNotification;
using PrintManagerment_API.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Mappers
{
    public class NotifyConverter
    {
        public DataResponseNotify EntityDTO(Notification notification)
        {
            return new DataResponseNotify
            {
                Id = notification.Id,
                Content = notification.Content,
                Link = notification.Link,
                CreateTime = notification.CreateTime,
                IsSeen = notification.IsSeen,
            };
        }
    }
}
