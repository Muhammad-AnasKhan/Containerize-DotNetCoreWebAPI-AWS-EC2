using UserAuthAPI.API.Domain.Entities;

namespace UserAuthAPI.API.Domain.Dtos.Request
{
    public class OrderListResponeDto
    {
        public bool Status { get; set; }
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public List<Orders> Orders {get; set;}
    }
}
