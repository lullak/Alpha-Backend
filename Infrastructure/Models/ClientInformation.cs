namespace Infrastructure.Models
{
    public class ClientInformation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ClientBillingCity { get; set; } = null!;
        public string? ClientBillingAddress { get; set; }
        public string? ClientBillingPostalCode { get; set; }
        public string? ClientBillingReference { get; set; }
    }
}
