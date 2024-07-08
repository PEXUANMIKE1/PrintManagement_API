using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class CustomerFeedBack : BaseEnities
    {
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string FeedBackContent { get; set; } = string.Empty;
        public string ResponseByCompany { get; set; } = string.Empty;
        public int UserFeedBackId { get; set; }
        public User? UserFeedBack { get; set; }
        public DateTime FeedBackTime { get; set; }
        public DateTime ResponseTime { get; set; }

    }
}
