namespace UserAuthAPI.API.Domain.Dtos.Request
{
    public class OrderListRequestDto
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
