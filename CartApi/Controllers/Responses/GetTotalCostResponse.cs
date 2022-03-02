namespace CartApi.Controllers.Responses
{
    public class GetTotalCostResponse
    {
        public decimal TotalCost { get; set; } = 0;

        public decimal NetCost { get; set; } = 0;

        public int TotalItem { get; set; } = 0;
    }
}
